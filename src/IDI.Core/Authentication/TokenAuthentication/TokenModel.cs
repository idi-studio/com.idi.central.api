using Newtonsoft.Json;

namespace IDI.Core.Authentication.TokenAuthentication
{
    public class TokenModel
    {
        [JsonProperty("access_token")]
        public string Token { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; } = "Bearer";
    }
}
