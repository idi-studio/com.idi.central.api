using System;
using Newtonsoft.Json;

namespace IDI.Central.Models.Administration
{
    public class UserRow
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("active")]
        public bool IsActive { get; set; }
    }
}
