using Newtonsoft.Json;

namespace IDI.Core.Localization
{
    public class PackageItem
    {
        [JsonProperty("prefix")]
        public string Prefix { get; set; } = string.Empty;

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
