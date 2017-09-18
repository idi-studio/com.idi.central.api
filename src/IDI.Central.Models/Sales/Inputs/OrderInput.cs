using System;
using IDI.Central.Common.Enums;
using IDI.Core.Common;
using Newtonsoft.Json;

namespace IDI.Central.Models.Sales
{
    public class OrderInput : IInput
    {
        [JsonProperty("status")]
        public OrderStatus Status { get; set; }

        [JsonProperty("remark")]
        public string Remark { get; set; } = string.Empty;

        [JsonProperty("custid")]
        public Guid? CustomerId { get; set; }
    }
}
