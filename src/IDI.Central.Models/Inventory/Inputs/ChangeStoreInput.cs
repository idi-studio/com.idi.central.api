using System.Collections.Generic;
using IDI.Central.Domain.Modules.Inventory;
using IDI.Core.Common;
using Newtonsoft.Json;

namespace IDI.Central.Models.Inventory.Inputs
{
    public class ChangeStoreInput : IInput
    {
        [JsonProperty("items")]
        public List<StockItemModel> Items { get; set; } = new List<StockItemModel>();
    }
}
