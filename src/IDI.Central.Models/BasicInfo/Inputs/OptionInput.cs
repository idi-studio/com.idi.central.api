using IDI.Core.Common;
using Newtonsoft.Json;

namespace IDI.Central.Models.BasicInfo
{
    public class OptionInput : IInput
    {
        [JsonProperty("name")]
        public string Category { get; set; }
    }
}
