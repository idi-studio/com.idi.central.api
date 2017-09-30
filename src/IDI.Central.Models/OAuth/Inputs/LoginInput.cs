using IDI.Central.Common.Enums;
using IDI.Core.Common;
using Newtonsoft.Json;

namespace IDI.Central.Models.OAuth.Inputs
{
    public class LoginInput : IInput
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("redirect_uri")]
        public string RedirectUri { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("type")]
        public OAuthType Type { get; set; }
    }
}
