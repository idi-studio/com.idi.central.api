using System;
using IDI.Central.Common.Enums;
using IDI.Core.Common;
using Newtonsoft.Json;

namespace IDI.Central.Models.Retailing
{
    public class VoucherInput : IInput
    {
        [JsonProperty("vid")]
        public Guid Id { get; set; }

        [JsonProperty("paymethod")]
        public PayMethod PayMethod { get; set; }

        [JsonProperty("amount")]
        public decimal PayAmount { get; set; }

        [JsonProperty("remark")]
        public string Remark { get; set; }

        [JsonProperty("oid")]
        public Guid OrderId { get; set; }
    }
}
