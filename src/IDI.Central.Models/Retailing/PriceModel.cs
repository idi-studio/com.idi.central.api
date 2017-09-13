using System;
using IDI.Central.Common;
using IDI.Core.Infrastructure.Queries;
using Newtonsoft.Json;

namespace IDI.Central.Models.Retailing
{
    public class PriceModel : IModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("category")]
        public PriceCategory Category { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("grade")]
        public int Grade { get; set; }
    }
}
