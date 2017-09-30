using IDI.Core.Infrastructure.Queries;
using Newtonsoft.Json;

namespace IDI.Central.Models.OAuth
{
    public abstract class OAuthUserModel : IModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("login")]
        public string Login { get; set; }
    }

    public class GitHubUserModel : OAuthUserModel
    {
        [JsonProperty("company")]
        public string Company { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("bio")]
        public string Profession { get; set; }

        [JsonProperty("followers")]
        public string Followers { get; set; }
    }
}
