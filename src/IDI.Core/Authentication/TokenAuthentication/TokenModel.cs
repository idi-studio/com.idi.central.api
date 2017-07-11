using Newtonsoft.Json;

namespace IDI.Core.Authentication.TokenAuthentication
{
    internal class TokenModel
    {
        [JsonProperty("access_token")]
        public string Token { get; set; }

        [JsonProperty("expires_in")]
        public int Expiration { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; } = "Bearer";
    }
}
