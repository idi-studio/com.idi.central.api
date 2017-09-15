using IDI.Core.Common;
using Newtonsoft.Json;

namespace IDI.Central.Models.Common
{
    public class CategoryInput : IInput
    {
        [JsonProperty("name")]
        public string EnumType { get; set; }
    }
}
