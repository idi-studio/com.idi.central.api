using IDI.Core.Common;
using Newtonsoft.Json;

namespace IDI.Central.Models.Administration.Inputs
{
    public class RoleAuthorizeInput : IInput
    {
        [JsonProperty("role")]
        public string Role { get; private set; }

        [JsonProperty("permissions")]
        public string[] Permissions { get; private set; }
    }
}
