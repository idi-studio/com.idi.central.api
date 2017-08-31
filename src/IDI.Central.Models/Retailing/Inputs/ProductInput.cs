using System.Collections.Generic;
using IDI.Core.Common;
using Newtonsoft.Json;

namespace IDI.Central.Models.Retailing
{
    public class ProductInput : IInput
    {
        [JsonProperty("name")]
        public string Name { get; set; } = "";

        [JsonProperty("code")]
        public string QRCode { get; set; } = "";

        [JsonProperty("tags")]
        public List<TagModel> Tags { get; set; } = new List<TagModel>();

        [JsonProperty("active")]
        public bool Enabled { get; set; }

        [JsonProperty("onshelf")]
        public bool OnShelf { get; set; } = false;
    }
}
