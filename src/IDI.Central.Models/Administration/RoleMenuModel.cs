using System.Collections.Generic;
using IDI.Core.Infrastructure.Queries;
using Newtonsoft.Json;

namespace IDI.Central.Models.Administration
{
    public class RoleMenuModel : IModel
    {
        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("menus")]
        public List<MenuItem> Menus { get; set; } = new List<MenuItem>();
    }
}
