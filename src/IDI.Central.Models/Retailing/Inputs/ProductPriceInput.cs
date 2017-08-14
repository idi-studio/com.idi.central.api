using System;
using System.Collections.Generic;
using IDI.Core.Common;
using Newtonsoft.Json;

namespace IDI.Central.Models.Retailing
{
    public class ProductPriceInput : IInput
    {
        [JsonProperty("product-id")]
        public Guid ProductId { get; set; }

        [JsonProperty("category")]
        public PriceCategory Category { get; set; }

        [JsonProperty("tags")]
        public List<Tag> Tags { get; set; } = new List<Tag>();

        [JsonProperty("active")]
        public bool Enabled { get; set; }
    }
}
