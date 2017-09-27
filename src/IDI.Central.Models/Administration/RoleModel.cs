using System;
using IDI.Core.Authentication;
using IDI.Core.Infrastructure.Queries;
using Newtonsoft.Json;

namespace IDI.Central.Models.Administration
{
    public class RoleModel : IModel, IRole
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; } = "";

        [JsonProperty("descrition")]
        public string Descrition { get; set; } = "";

        [JsonProperty("active")]
        public bool IsActive { get; set; }

        [JsonProperty("permissions")]
        public string Permissions { get; set; }
    }
}
