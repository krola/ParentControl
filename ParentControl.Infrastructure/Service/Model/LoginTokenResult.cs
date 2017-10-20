using Newtonsoft.Json;

namespace ParentControl.Infrastructure.Service.Model
{
    public class LoginTokenResult
    {
        public override string ToString()
        {
            return AccessToken;
        }

        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }
    }
}
