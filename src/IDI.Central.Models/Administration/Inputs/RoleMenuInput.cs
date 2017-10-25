using System.Collections.Generic;
using IDI.Core.Common;
using Newtonsoft.Json;

namespace IDI.Central.Models.Administration.Inputs
{
    public class RoleMenuInput : IInput
    {
        [JsonProperty("role")]
        public string Role { get; private set; }

        [JsonProperty("menus")]
        public List<int> Menus { get; private set; } = new List<int>();
    }
}
