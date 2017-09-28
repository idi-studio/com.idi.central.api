using System.Collections.Generic;
using IDI.Core.Infrastructure.Queries;
using Newtonsoft.Json;

namespace IDI.Central.Models.Administration
{
    public class ModulePermissionModel : IModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("permissions")]
        public List<PermissionModel> Permissions { get; set; } = new List<PermissionModel>();
    }
}
