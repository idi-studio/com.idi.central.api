﻿using System.Collections.Generic;
using IDI.Core.Common;
using Newtonsoft.Json;

namespace IDI.Central.Models.Retailing
{
    public class ProductPictureBatchInput : IInput
    {
        [JsonProperty("images")]
        public List<ProductPictureInput> Pictures { get; set; } = new List<ProductPictureInput>();
    }
}
