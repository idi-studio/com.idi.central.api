using System;
using Newtonsoft.Json;

namespace IDI.Central.Models.Administration
{
    public class RoleModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; } = "";

        [JsonProperty("descrition")]
        public string Descrition { get; set; } = "";

        [JsonProperty("active")]
        public bool IsActive { get; set; }
    }
}
