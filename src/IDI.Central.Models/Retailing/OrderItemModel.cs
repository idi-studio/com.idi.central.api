using System;
using IDI.Core.Infrastructure.Queries;
using Newtonsoft.Json;

namespace IDI.Central.Models.Retailing
{
    public class OrderItemModel : IQueryResult
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("orderid")]
        public Guid OrderId { get; set; }

        [JsonProperty("productid")]
        public Guid ProductId { get; set; }

        [JsonProperty("name")]
        public string ProductName { get; set; }

        [JsonProperty("unitprice")]
        public decimal UnitPrice { get; set; }

        [JsonProperty("readjustunitprice")]
        public decimal? ReadjustUnitPrice { get; set; }

        [JsonProperty("qty")]
        public decimal Quantity { get; set; }

    }
}
