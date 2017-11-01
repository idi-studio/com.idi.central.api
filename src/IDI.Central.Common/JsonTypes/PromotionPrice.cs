using System.Collections.Generic;
using IDI.Core.Infrastructure.Queries;
using Newtonsoft.Json;

namespace IDI.Central.Common.JsonTypes
{
    public class PromotionPrice : IModel
    {
        /// <summary>
        /// 原价
        /// </summary>
        [JsonProperty("original")]
        public decimal Original { get; set; }

        /// <summary>
        /// 现价
        /// </summary>
        [JsonProperty("current")]
        public decimal Current { get; set; }

        /// <summary>
        /// 会员价格
        /// </summary>
        [JsonProperty("vip")]
        public List<decimal> VIP { get; set; } = new List<decimal>();
    }
}
