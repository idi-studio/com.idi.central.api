using System;
using IDI.Central.Common.JsonTypes;
using IDI.Core.Infrastructure.Queries;
using Newtonsoft.Json;

namespace IDI.Central.Models.Sales
{
    public class PromotionModel: IModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("start")]
        public DateTime StartTime { get; set; }

        [JsonProperty("end")]
        public DateTime EndTime { get; set; }

        [JsonProperty("price")]
        public PromotionPrice Price { get; set; } = new PromotionPrice();

        [JsonProperty("pid")]
        public Guid ProductId { get; set; }

        [JsonProperty("pname")]
        public string ProductName { get; set; }

        [JsonProperty("enabled")]
        public bool Enabled { get; set; }
    }
}
