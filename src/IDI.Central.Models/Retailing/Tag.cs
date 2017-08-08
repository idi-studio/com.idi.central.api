using Newtonsoft.Json;

namespace IDI.Central.Models.Retailing
{
    public class Tag
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
