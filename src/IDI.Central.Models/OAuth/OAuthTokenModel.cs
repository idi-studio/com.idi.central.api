using IDI.Core.Infrastructure.Queries;
using Newtonsoft.Json;

namespace IDI.Central.Models.OAuth
{
    public abstract class OAuthTokenModel : IModel
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("error_description")]
        public string ErrorDesc { get; set; }
    }

    public class GitHubTokenModel : OAuthTokenModel
    {
        [JsonProperty("scope")]
        public string scope { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }
    }
}
