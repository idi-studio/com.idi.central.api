using IDI.Central.Common.Enums;
using IDI.Core.Infrastructure.Queries;
using Newtonsoft.Json;

namespace IDI.Central.Models.Administration
{
    public interface IOAuthUserModel : IModel
    {
        string Name { get; set; }

        string Email { get; set; }

        string Login { get; set; }

        OAuthType Type { get; set; }
    }

    public class GitHubUserModel : IOAuthUserModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("otype")]
        public OAuthType Type { get; set; } = OAuthType.GitHub;

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
