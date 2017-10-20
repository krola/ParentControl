using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ParentControl.Core.Contracts;
using ParentControl.Core.Contracts.Services;
using RestSharp.Portable.HttpClient;
using ParentControl.Core.Services.Model;
using RestSharp.Portable;

namespace ParentControl.Core.Service
{
    public class HttpService : IHttpService
    {
        private RestClient _client;
        private AuthorizationResult _token;

        public HttpService(IConfiguration config)
        {
            if (!string.IsNullOrEmpty(config.ApiAddress)) { _client = new RestClient(config.ApiAddress); }
            
        }
        public async Task<AuthorizationResult> AuthenticateAsync(string username, string password)
        {
            var request = new RestRequest("Token", Method.POST);
            request.AddParameter("grant_type", "password");
            request.AddParameter("username", username);
            request.AddParameter("password", password);

            // execute the request
            IRestResponse response = await _client.Execute(request);
            var content = response.Content; // raw content as 
            var result = JsonConvert.DeserializeObject<AuthorizationResult>(content);
            _token = result;
            return result;
        }

        public async Task<string> GetRequestAsync(string url, params RequestParameter[] parameters)
        {
            var request = RestRequest(url);
            foreach (var requestParameter in parameters)
            {
                request.AddParameter(requestParameter.Key, requestParameter.Value);
            }
            var response = await _client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
                return string.Empty;
            return response.Content; // raw content as 
        }

        private RestRequest RestRequest(string url)
        {
            var request = new RestRequest(url);
            request.AddHeader("Authorization", string.Format("Bearer {0}", _token.AccessToken));
            return request;
        }

        public async Task<Response> PostRequestAsync(string url, params RequestParameter[] parameters)
        {
            var request = RestRequest(url);
            request.Method = Method.POST;
            request.AddHeader("Content-type", "application/json; charset=utf-8");
            foreach (var requestParameter in parameters)
            {
                request.AddParameter(requestParameter.Key, requestParameter.Value);
            }

            IRestResponse response = await _client.Execute(request);
            return new Response()
            {
                Success = response.StatusCode == HttpStatusCode.OK,
                Message = response.StatusCode != HttpStatusCode.OK ? response.Content : string.Empty
            };
        }

        public async Task<Response> PostRequestAsync(string url, object body)
        {
            var request = RestRequest(url);
            request.Method = Method.POST;
            request.AddBody(body);

            IRestResponse response = await _client.Execute(request);
            return new Response()
            {
                Success = response.StatusCode == HttpStatusCode.OK,
                Message = response.StatusCode != HttpStatusCode.OK ? response.Content : string.Empty
            };
        }

        public string Token
        {
            get { return _token?.AccessToken; }
        }
        public bool IsConnected
        {
            get
            {
                return _token != null ? !string.IsNullOrEmpty(_token.AccessToken) : false;
            }
        }
    }
}
