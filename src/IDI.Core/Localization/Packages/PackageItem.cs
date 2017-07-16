using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace IDI.Core.Localization
{
    public class PackageItem
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
