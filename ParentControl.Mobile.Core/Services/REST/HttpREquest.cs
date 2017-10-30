using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ParentControl.Core.Services.REST
{
    class HttpRequest
    {
        private HttpClient _httpClient;
        private HttpResponseMessage _response;

        public HttpRequest()
        {
            _httpClient = new HttpClient();
        }

        public HttpClient Request
        {
            get
            {
                return _httpClient;
            }
        }

        public HttpResponseMessage Response{
            get
            {
                return _response;
            }
        }
    }
}
