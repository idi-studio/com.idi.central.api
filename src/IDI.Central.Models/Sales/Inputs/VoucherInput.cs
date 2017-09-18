using System;
using IDI.Central.Common.Enums;
using IDI.Core.Common;
using Newtonsoft.Json;

namespace IDI.Central.Models.Sales
{
    public class VoucherInput : IInput
    {
        [JsonProperty("vid")]
        public Guid Id { get; set; }

        [JsonProperty("status")]
        public TradeStatus Status { get; set; }

        [JsonProperty("paymethod")]
        public PayMethod PayMethod { get; set; }

        [JsonProperty("payment")]
        public decimal Payment { get; set; }

        [JsonProperty("remark")]
        public string Remark { get; set; }

        [JsonProperty("oid")]
        public Guid OrderId { get; set; }
    }
}
