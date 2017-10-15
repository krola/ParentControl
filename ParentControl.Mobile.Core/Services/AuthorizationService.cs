using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ParentControl.Mobile.Core.Services.Contracts;

namespace ParentControl.Mobile.Core.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly string _tokenEndpoint = "http://parentcontrolapi.azurewebsites.net/";
        public async Task<bool> Login(string username, string password)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(_tokenEndpoint);
            var request = new HttpRequestMessage(HttpMethod.Post, "Token");

            var byteArray = new UTF8Encoding().GetBytes("<clientid>:<clientsecret>");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var formData = new List<KeyValuePair<string, string>>();
            formData.Add(new KeyValuePair<string, string>("grant_type", "password"));
            formData.Add(new KeyValuePair<string, string>("username", username));
            formData.Add(new KeyValuePair<string, string>("password", password));

            request.Content = new FormUrlEncodedContent(formData);
            var response = await client.SendAsync(request);

            return true;
        }
    }
}
