using System;
using IDI.Core.Common;
using Newtonsoft.Json;

namespace IDI.Central.Models.Administration
{
    public class UserRow
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("gender")]
        public Gender Gender { get; set; }

        [JsonProperty("birthday")]
        public DateTime Birthday { get; set; }

        [JsonProperty("photo")]
        public string Photo { get; set; }

        [JsonProperty("active")]
        public bool IsActive { get; set; }
    }
}
