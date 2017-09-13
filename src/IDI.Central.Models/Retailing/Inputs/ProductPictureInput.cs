using System;
using IDI.Central.Common.Enums;
using IDI.Core.Common;
using Newtonsoft.Json;

namespace IDI.Central.Models.Retailing
{
    public class ProductPictureInput : IInput
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("pid")]
        public Guid ProductId { get; set; }

        [JsonProperty("sn")]
        public int Sequence { get; set; }

        [JsonProperty("category")]
        public ImageCategory Category { get; set; } = ImageCategory.Picture;
    }
}
