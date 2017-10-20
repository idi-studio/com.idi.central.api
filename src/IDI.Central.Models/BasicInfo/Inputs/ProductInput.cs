using System;
using System.Collections.Generic;
using IDI.Central.Common;
using IDI.Core.Common;
using Newtonsoft.Json;

namespace IDI.Central.Models.BasicInfo
{
    public class ProductInput : IInput
    {
        [JsonProperty("name")]
        public string Name { get; set; } = "";

        [JsonProperty("skid")]
        public Guid StoreId { get; set; }

        [JsonProperty("sku")]
        public decimal SKU { get; set; } = 1M;

        [JsonProperty("ss")]
        public decimal SafetyStock { get; set; } = 0M;

        [JsonProperty("uint")]
        public string Uint { get; set; } = "PCS";

        [JsonProperty("bin")]
        public string BinCode { get; set; } = Configuration.Inventory.DefaultBinCode;

        [JsonProperty("tags")]
        public List<TagModel> Tags { get; set; } = new List<TagModel>();

        [JsonProperty("active")]
        public bool Enabled { get; set; }

        [JsonProperty("onshelf")]
        public bool OnShelf { get; set; } = false;
    }
}
