using System;
using IDI.Central.Common.Enums;
using IDI.Core.Infrastructure.Queries;
using Newtonsoft.Json;

namespace IDI.Central.Models.Retailing
{
    public class VoucherModel : IModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("transno")]
        public string TransNo { get; set; }

        [JsonProperty("oid")]
        public Guid OrderId { get; set; }

        [JsonProperty("orderno")]
        public string OrderNo { get; set; }

        [JsonProperty("paymethod")]
        public PayMethod PayMethod { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("payamount")]
        public decimal PayAmount { get; set; }

        [JsonProperty("orderamount")]
        public decimal OrderAmount { get; set; }

        [JsonProperty("document")]
        public string Document { get; set; }

        [JsonProperty("remark")]
        public string Remark { get; set; }
    }
}
