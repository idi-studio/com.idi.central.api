using System;
using IDI.Core.Infrastructure.Queries;
using Newtonsoft.Json;

namespace IDI.Central.Domain.Modules.Inventory
{
    public class StockItemModel : IModel
    {
        [JsonProperty("pid")]
        public Guid ProductId { get; set; }

        [JsonProperty("bin")]
        public string BinCode { get; set; }

        [JsonProperty("qty")]
        public decimal Quantity { get; set; }
    }
}
