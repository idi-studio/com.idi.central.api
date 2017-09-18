using IDI.Core.Infrastructure.Queries;
using Newtonsoft.Json;

namespace IDI.Central.Models.Common
{
    public class TagModel : IModel
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
