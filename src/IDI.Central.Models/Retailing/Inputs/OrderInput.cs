using System;
using IDI.Core.Common;
using Newtonsoft.Json;

namespace IDI.Central.Models.Retailing
{
    public class OrderInput : IInput
    {
        [JsonProperty("remark")]
        public string Remark { get; set; } = string.Empty;

        [JsonProperty("custid")]
        public Guid? CustomerId { get; set; }
    }
}
