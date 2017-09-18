using System;
using IDI.Core.Infrastructure.Queries;
using Newtonsoft.Json;

namespace IDI.Central.Models.Sales
{
    public class OrderItemModel : IModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("oid")]
        public Guid OrderId { get; set; }

        [JsonProperty("pid")]
        public Guid ProductId { get; set; }

        [JsonProperty("name")]
        public string ProductName { get; set; }

        [JsonProperty("unitprice")]
        public decimal UnitPrice { get; set; }

        [JsonProperty("qty")]
        public decimal Quantity { get; set; }

    }
}
