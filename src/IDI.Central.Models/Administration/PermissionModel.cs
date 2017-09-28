using IDI.Core.Authentication;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure.Queries;
using Newtonsoft.Json;

namespace IDI.Central.Models.Administration
{
    public class PermissionModel: IModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("type")]
        public PermissionType Type { get; set; }

        [JsonProperty("module")]
        public string Module { get; set; }

        [JsonProperty("checked")]
        public bool Checked { get; set; }
    }
}
