﻿using System;
using IDI.Central.Common;
using IDI.Core.Common;
using Newtonsoft.Json;

namespace IDI.Central.Models.Retailing
{
    public class ProductPriceInput : IInput
    {
        [JsonProperty("product-id")]
        public Guid ProductId { get; set; }

        [JsonProperty("category")]
        public PriceCategory Category { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("grade")]
        public int Grade { get; set; } = 0;

        [JsonProperty("start-date")]
        public DateTime StartDate { get; set; }

        [JsonProperty("due-date")]
        public DateTime DueDate { get; set; }

        [JsonProperty("active")]
        public bool Enabled { get; set; }
    }
}