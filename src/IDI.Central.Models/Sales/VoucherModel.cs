using System;
using IDI.Central.Common.Enums;
using IDI.Core.Infrastructure.Queries;
using Newtonsoft.Json;

namespace IDI.Central.Models.Sales
{
    public class VoucherModel : IModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("tn")]
        public string TN { get; set; }

        [JsonProperty("status")]
        public TradeStatus Status { get; set; }

        [JsonProperty("sn")]
        public string SN { get; set; }

        [JsonProperty("paymethod")]
        public PayMethod PayMethod { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("payment")]
        public decimal PaymentAmount { get; set; }

        [JsonProperty("payable")]
        public decimal PayableAmount { get; set; }

        [JsonProperty("doc")]
        public string Document { get; set; }

        [JsonProperty("remark")]
        public string Remark { get; set; }

        [JsonProperty("oid")]
        public Guid OrderId { get; set; }
    }
}
