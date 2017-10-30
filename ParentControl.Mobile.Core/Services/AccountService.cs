using ParentControl.Core.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParentControl.Core.Services.Model;
using Newtonsoft.Json;

namespace ParentControl.Core.Services
{
    public class AccountService : IAccountService
    {
        IHttpService _httpService;
        public AccountService(IHttpService httpService)
        {
            _httpService = httpService;
        }
        public async Task<bool> AuthenticateAsync(string username, string password)
        {
            var response = await _httpService.PostRequestAsync("token", new RequestParameter[]
            {
                new RequestParameter()
                {
                    Key= "grant_type",
                    Value = "password"
                },
                new RequestParameter()
                {
                    Key = "username",
                    Value = username
                },
                new RequestParameter()
                {
                    Key = "password",
                    Value = password
                }
            });

            var result = JsonConvert.DeserializeObject<AuthorizationResult>(response.Data);
            _httpService.AuthenticationData = result;
            return _httpService.IsAuthenticated;
        }
    }
}
