using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ParentControl.Infrastructure.Contracts;
using ParentControl.Infrastructure.Owin.Model;
using RestSharp;

namespace ParentControl.Infrastructure.Owin
{
    public class HttpService : IHttpService
    {
        private RestClient _client;
        private LoginTokenResult _token;

        public HttpService(IConfiguration config)
        {
            if (!string.IsNullOrEmpty(config.ApiAddress)) { _client = new RestClient(config.ApiAddress); }
            
        }
        public LoginTokenResult Authenticate(string username, string password)
        {
            var request = new RestRequest("Token", Method.POST);
            request.AddParameter("grant_type", "password");
            request.AddParameter("username", username);
            request.AddParameter("password", password);

            // execute the request
            IRestResponse response = _client.Execute(request);
            var content = response.Content; // raw content as 
            var result = JsonConvert.DeserializeObject<LoginTokenResult>(content);
            _token = result;
            return result;
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
            request.AddHeader("Authorization", string.Format("Bearer {0}", _token.AccessToken));
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
