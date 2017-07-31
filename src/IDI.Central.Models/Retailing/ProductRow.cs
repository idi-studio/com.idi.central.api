using System;
using Newtonsoft.Json;

namespace IDI.Central.Models.Retailing
{
    public class ProductRow
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("spec")]
        public string Specifications { get; set; }

        [JsonProperty("active")]
        public bool Enabled { get; set; } = true;
    }
}
