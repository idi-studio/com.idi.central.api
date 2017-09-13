using System;
using IDI.Central.Common.Enums;
using IDI.Core.Infrastructure.Queries;
using Newtonsoft.Json;

namespace IDI.Central.Models.Retailing
{
    public class ProductPictureModel : IModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("pid")]
        public Guid ProductId { get; set; }

        [JsonProperty("category")]
        public ImageCategory Category { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("sn")]
        public int Sequence { get; set; }

        [JsonProperty("filename")]
        public string FileName { get; set; }

        [JsonProperty("url")]
        public string URL { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }
    }
}
