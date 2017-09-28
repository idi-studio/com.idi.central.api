using System.Collections.Generic;
using IDI.Core.Infrastructure.Queries;
using Newtonsoft.Json;

namespace IDI.Central.Models.Administration
{
    public class RolePermissionModel: IModel
    {
        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("modules")]
        public List<ModulePermissionModel> Modules { get; set; } = new List<ModulePermissionModel>();
    }
}
