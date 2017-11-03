using System;
using IDI.Central.Common.JsonTypes;
using IDI.Core.Common;
using Newtonsoft.Json;

namespace IDI.Central.Models.Sales
{
    public class PromotionInput : IInput
    {
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

        [JsonProperty("active")]
        public bool Enabled { get; set; }
    }
}
