using System;
using IDI.Core.Common;
using Newtonsoft.Json;

namespace IDI.Central.Models.Retailing
{
    public class ProductPictureInput : IInput
    {
        [JsonProperty("pid")]
        public Guid ProductId { get; set; }
    }
}
