using System;
using System.Collections.Generic;
using IDI.Core.Common.Basetypes;
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

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("tags")]
        public List<Tag> Tags { get; set; }

        [JsonProperty("active")]
        public bool Enabled { get; set; } = true;
    }
}
