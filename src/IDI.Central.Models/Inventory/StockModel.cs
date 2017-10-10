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

        [JsonProperty("pname")]
        public string ProductName { get; set; }

        [JsonProperty("storeid")]
        public Guid StoreId { get; set; }

        [JsonProperty("storename")]
        public string StoreName { get; set; }

        [JsonProperty("qty")]
        public decimal Quantity { get; set; }

        [JsonProperty("frozen")]
        public decimal Frozen { get; set; }

        [JsonProperty("available")]
        public decimal Available { get; set; }
    }
}
