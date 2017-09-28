using IDI.Core.Infrastructure.Queries;
using Newtonsoft.Json;

namespace IDI.Central.Models.Administration
{
    public class RoleSelectionModel : IModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("checked")]
        public bool Checked { get; set; }
    }
}
