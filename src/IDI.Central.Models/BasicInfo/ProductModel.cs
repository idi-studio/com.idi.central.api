using System;
using System.Collections.Generic;
using IDI.Central.Common.JsonTypes;
using IDI.Core.Infrastructure.Queries;
using Newtonsoft.Json;

namespace IDI.Central.Models.BasicInfo
{
    public class ProductModel: IModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string QRCode { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("skid")]
        public Guid StoreId { get; set; }

        [JsonProperty("bin")]
        public string BinCode { get; set; }

        [JsonProperty("sku")]
        public decimal SKU { get; set; }

        [JsonProperty("ss")]
        public decimal SafetyStock { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }

        [JsonProperty("tags")]
        public List<Tag> Tags { get; set; } = new List<Tag>();

        [JsonProperty("images")]
        public List<ProductPictureModel> Pictures { get; set; } = new List<ProductPictureModel>();

        [JsonProperty("active")]
        public bool Enabled { get; set; } = true;

        [JsonProperty("onshelf")]
        public bool OnShelf { get; set; } = false;
    }
}
