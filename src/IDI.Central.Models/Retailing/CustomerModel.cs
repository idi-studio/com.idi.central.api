using System;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Queries;
using Newtonsoft.Json;

namespace IDI.Central.Models.Retailing
{
    public class CustomerModel : IQueryResult
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("gender")]
        public Gender Gender { get; set; } = Gender.Unknown;

        [JsonProperty("grade")]
        public int Grade { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("date")]
        public string date { get; set; }
    }
}
