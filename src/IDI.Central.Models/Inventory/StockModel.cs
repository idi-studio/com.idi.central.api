using System;
using IDI.Core.Infrastructure.Queries;
using Newtonsoft.Json;

namespace IDI.Central.Models.Inventory
{
    public  class StockModel: IModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("bin")]
        public string BinCode { get; set; }

        [JsonProperty("pid")]
        public Guid ProductId { get; set; }

        [JsonProperty("product")]
        public string ProductName { get; set; }

        [JsonProperty("sid")]
        public Guid StoreId { get; set; }

        [JsonProperty("store")]
        public string StoreName { get; set; }

        [JsonProperty("qty")]
        public decimal Quantity { get; set; }

        [JsonProperty("frz")]
        public decimal Reserve { get; set; }

        [JsonProperty("avl")]
        public decimal Available { get; set; }
    }
}
