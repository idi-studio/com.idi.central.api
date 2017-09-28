using System.Collections.Generic;
using IDI.Core.Infrastructure.Queries;
using Newtonsoft.Json;

namespace IDI.Central.Models.Administration
{
    public class UserRoleModel : IModel
    {
        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("roles")]
        public List<RoleSelectionModel> Roles { get; set; } = new List<RoleSelectionModel>();
    }
}
