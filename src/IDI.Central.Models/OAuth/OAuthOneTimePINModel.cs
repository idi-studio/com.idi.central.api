using IDI.Core.Infrastructure.Queries;
using Newtonsoft.Json;

namespace IDI.Central.Models.OAuth
{
    public class OAuthOneTimePINModel : IModel
    {
        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("pin")]
        public string PIN { get; set; }
    }
}
