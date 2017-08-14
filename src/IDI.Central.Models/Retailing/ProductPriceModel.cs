using System;
using IDI.Central.Common;
using IDI.Core.Infrastructure.Queries;
using Newtonsoft.Json;

namespace IDI.Central.Models.Retailing
{
    public class ProductPriceModel : IQueryResult
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("category")]
        public PriceCategory Category { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("grade")]
        public int Grade { get; set; }

        [JsonProperty("start-date")]
        public DateTime StartDate { get; set; }

        [JsonProperty("due-date")]
        public DateTime DueDate { get; set; }

        [JsonProperty("product-id")]
        public Guid ProductId { get; set; }

        [JsonProperty("active")]
        public bool Enabled { get; set; } = true;
    }
}
