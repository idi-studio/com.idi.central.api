using System;
using IDI.Central.Common.Enums;
using IDI.Core.Infrastructure.Queries;
using Newtonsoft.Json;

namespace IDI.Central.Models.Material
{
    public class ProductPriceModel : IModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("category")]
        public PriceCategory Category { get; set; }

        [JsonProperty("categoryname")]
        public string CategoryName { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("grade")]
        public int GradeFrom { get; set; }

        [JsonProperty("gradeto")]
        public int GradeTo { get; set; }

        [JsonProperty("startdate")]
        public string PeriodStart { get; set; }

        [JsonProperty("duedate")]
        public string PeriodEnd { get; set; }

        [JsonProperty("pid")]
        public Guid ProductId { get; set; }

        [JsonProperty("active")]
        public bool Enabled { get; set; } = true;
    }
}
