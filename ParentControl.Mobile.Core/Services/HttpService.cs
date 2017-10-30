using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ParentControl.Core.Contracts;
using ParentControl.Core.Contracts.Services;
using ParentControl.Core.Services.Model;
using System;
using System.Net.Http;
using PortableRest;
using System.Text;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Linq;

namespace ParentControl.Core.Service
{
    public class HttpService : IHttpService
    {
        private AuthorizationResult _token;
        private IConfiguration _configuration;
        private RestClient _client;
        private Uri _baseAddress;

        public HttpService(IConfiguration config)
        {
            _configuration = config;
            _client = new RestClient();
            _baseAddress = new Uri(config.ApiAddress);
        }

        public async Task<string> GetRequestAsync(string url, params RequestParameter[] parameters)
        {
            var request = RestRequest(url);
            foreach (var requestParameter in parameters)
            {
                request.AddParameter(requestParameter.Key, requestParameter.Value);
            }
            var response = await _client.ExecuteAsync<string>(request);
            return response; // raw content as 
        }

        private RestRequest RestRequest(string url)
        {
            var request = new RestRequest(url);
            if(_token != null)
            {
                request.AddHeader("Authorization", string.Format("Bearer {0}", _token.AccessToken));
            }
            return request;
        }

        public async Task<Response> PostRequestAsync(string url, params RequestParameter[] parameters)
        {
           

            //var request = RestRequest(new Uri(_baseAddress, url).AbsoluteUri);
            //request.Method = HttpMethod.Post;
            //request.ReturnRawString = true;
            //foreach(var para in parameters)
            //{
            //    request.AddParameter(para.Key, para.Value);
            //}

            try
            {
                //_client.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                //var response = await _client.ExecuteAsync<string>(request);
                using (var client = new HttpClient())
                {
                    client.BaseAddress = _baseAddress;
                    var content = parameters.Select(p => new KeyValuePair<string, string>(p.Key, p.Value));
                    var contentEncoded = new FormUrlEncodedContent(content);
                    var result = await client.PostAsync("/token", contentEncoded);
                    var resultContent = await result.Content.ReadAsStringAsync();
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<Response> PostRequestAsync(string url, object body)
        {
            var request = RestRequest(new Uri(_baseAddress, url).AbsoluteUri);
            request.ContentType = ContentTypes.FormUrlEncoded;
            request.Method = HttpMethod.Post;
         

            try
            {
                var response = await _client.ExecuteAsync<Response>(request);
                return response;
            }
            catch (HttpRequestException ex)
            {
                return null;
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return _token != null ? !string.IsNullOrEmpty(_token.AccessToken) : false;
            }
        }

        public AuthorizationResult AuthenticationData { get => _token; set => _token = value; }
    }
}
