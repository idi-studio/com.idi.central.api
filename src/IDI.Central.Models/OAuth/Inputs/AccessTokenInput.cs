using IDI.Central.Common.Enums;
using IDI.Core.Common;
using Newtonsoft.Json;

namespace IDI.Central.Models.OAuth.Inputs
{
    public class AccessTokenInput: IInput
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("type")]
        public OAuthType Type { get; set; }
    }
}
