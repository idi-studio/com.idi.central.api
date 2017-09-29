using IDI.Core.Common;
using Newtonsoft.Json;

namespace IDI.Central.Models.BasicInfo
{
    public class CategoryInput : IInput
    {
        [JsonProperty("name")]
        public string EnumType { get; set; }
    }
}
