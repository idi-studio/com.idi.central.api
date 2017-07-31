﻿using System;
using Newtonsoft.Json;

namespace IDI.Central.Models.Retailing
{
    public class ProductRow
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("profile")]
        public string Profile { get; set; }

        [JsonProperty("active")]
        public bool Enabled { get; set; } = true;
    }
}
