using System.Collections.Generic;
using IDI.Core.Infrastructure.Queries;
using Newtonsoft.Json;

namespace IDI.Central.Models.Administration
{
    public class MenuItem : IModel
    {
        [JsonProperty("sn")]
        public int SN { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; } = string.Empty;

        [JsonProperty("action")]
        public string Route { get; set; }

        [JsonProperty("checked")]
        public bool Checked { get; set; }

        [JsonProperty("sub")]
        public List<MenuItem> Sub { get; set; } = new List<MenuItem>();
    }
}
