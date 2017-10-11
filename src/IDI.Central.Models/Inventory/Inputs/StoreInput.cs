using IDI.Core.Common;
using Newtonsoft.Json;

namespace IDI.Central.Models.Inventory.Inputs
{
    public class StoreInput: IInput
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }
    }
}
