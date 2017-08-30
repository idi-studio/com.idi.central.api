using System;
using System.Collections.Generic;
using IDI.Core.Infrastructure.Queries;
using Newtonsoft.Json;

namespace IDI.Central.Models.Retailing
{
    public class ProductModel: IQueryResult
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string QRCode { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

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
