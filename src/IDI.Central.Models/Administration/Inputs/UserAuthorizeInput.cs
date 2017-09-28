using IDI.Core.Common;
using Newtonsoft.Json;

namespace IDI.Central.Models.Administration.Inputs
{
    public class UserAuthorizeInput : IInput
    {
        [JsonProperty("username")]
        public string UserName { get; private set; }

        [JsonProperty("roles")]
        public string[] Roles { get; private set; }
    }
}
