using System;
using System.Collections.Generic;
using IDI.Central.Models.Common;
using IDI.Central.Models.Material;
using IDI.Core.Infrastructure.Queries;
using Newtonsoft.Json;

namespace IDI.Central.Models.Sales
{
    public class SellModel : IModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string QRCode { get; set; }

        [JsonProperty("desc")]
        public string Description { get; set; }

        [JsonProperty("tags")]
        public List<TagModel> Tags { get; set; }

        [JsonProperty("price")]
        public PriceModel Price { get; set; }
    }
}
