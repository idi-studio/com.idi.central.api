using System;
using System.Collections.Generic;
using IDI.Core.Infrastructure.Queries;
using Newtonsoft.Json;

namespace IDI.Central.Models.Retailing
{
    public class ProductSellModel : IQueryResult
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

        [JsonProperty("prices")]
        public List<PriceModel> Prices { get; set; }
    }
}
