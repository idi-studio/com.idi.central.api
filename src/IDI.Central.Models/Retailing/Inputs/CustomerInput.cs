using System;
using IDI.Core.Common;
using Newtonsoft.Json;

namespace IDI.Central.Models.Retailing.Inputs
{
    public class CustomerInput : IInput
    {
        [JsonProperty("cid")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("phone")]
        public string PhoneNum { get; set; }

        [JsonProperty("grade")]
        public int Grade { get; set; }

        [JsonProperty("gender")]
        public Gender Gender { get; set; }
    }
}
