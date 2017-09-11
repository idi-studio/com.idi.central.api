using System;
using IDI.Core.Common;
using Newtonsoft.Json;

namespace IDI.Central.Models.Retailing
{
    public class OrderItemInput : IInput
    {
        [JsonProperty("oid")]
        public Guid OrderId { get; set; }

        [JsonProperty("pid")]
        public Guid ProductId { get; set; }

        [JsonProperty("priceid")]
        public Guid PriceId { get; set; }

        [JsonProperty("qty")]
        public decimal Quantity { get; set; }
    }
}
