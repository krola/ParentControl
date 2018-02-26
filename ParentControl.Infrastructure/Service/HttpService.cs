using System.Net;
using Newtonsoft.Json;
using ParentControl.Infrastructure.Contracts;
using ParentControl.Infrastructure.Contracts.Services;
using ParentControl.Infrastructure.Service.Model;
using RestSharp;
using Authorization = ParentControl.DTO.Authorization;

namespace ParentControl.Infrastructure.Service
{
    public class HttpService : IHttpService
    {
        private RestClient _client;
        private Authorization _authorization;

        public HttpService(IConfiguration config)
        {
            if (!string.IsNullOrEmpty(config.ApiAddress)) { _client = new RestClient(config.ApiAddress); }
            
        }
        public Authorization Authenticate(string username, string password)
        {
            var loginDTO = new LogInDTO()
            {
                UserName = username,
                Password = password
            };

            var request = new RestRequest("api/Authorization", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(loginDTO);

            IRestResponse response = _client.Execute<Authorization>(request);
            var content = response.Content;
            _authorization = JsonConvert.DeserializeObject<Authorization>(content);
            return _authorization;
        }

        public string GetRequest(string url, params RequestParameter[] parameters)
        {
            var request = RestRequest(url);
            foreach (var requestParameter in parameters)
            {
                request.AddParameter(requestParameter.Key, requestParameter.Value);
            }
            IRestResponse response = _client.Execute(request);
            if (response.ResponseStatus != ResponseStatus.Completed)
                return string.Empty;
            return response.Content; // raw content as 
        }

        private RestRequest RestRequest(string url)
        {
            var request = new RestRequest(url);
            request.AddHeader("Authorization", string.Format("bearer {0}", _authorization.Token));
            return request;
        }

        public Response PostRequest(string url, params RequestParameter[] parameters)
        {
            var request = RestRequest(url);
            request.Method = Method.POST;
            request.AddHeader("Content-type", "application/json; charset=utf-8");
            foreach (var requestParameter in parameters)
            {
                request.AddParameter(requestParameter.Key, requestParameter.Value);
            }

            IRestResponse response = _client.Execute(request);
            return new Response()
            {
                Success = response.StatusCode == HttpStatusCode.OK,
                Message = response.StatusCode != HttpStatusCode.OK ? response.ErrorMessage : string.Empty
            };
        }

        public Response PostRequest(string url, object body)
        {
            var request = RestRequest(url);
            request.Method = Method.POST;
            request.RequestFormat = DataFormat.Json;
            request.AddBody(body);

            IRestResponse response = _client.Execute(request);
            return new Response()
            {
                Success = response.StatusCode == HttpStatusCode.OK,
                Message = response.StatusCode != HttpStatusCode.OK ? response.ErrorMessage : string.Empty
            };
        }

        public Response PutRequest(string url, object body)
        {
            var request = RestRequest(url);
            request.Method = Method.PUT;
            request.RequestFormat = DataFormat.Json;
            request.AddBody(body);

            IRestResponse response = _client.Execute(request);
            return new Response()
            {
                Success = response.StatusCode == HttpStatusCode.OK,
                Message = response.StatusCode != HttpStatusCode.OK ? response.ErrorMessage : string.Empty
            };
        }

        public string Token
        {
            get { return _authorization.Token; }
        }
        public bool IsConnected
        {
            get
            {
                return _authorization != null && !string.IsNullOrEmpty(_authorization.Token);
            }
        }
    }
}
