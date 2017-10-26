using System;
using IDI.Core.Common;
using Newtonsoft.Json;

namespace IDI.Central.Models.Administration
{
    public class UserModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("gender")]
        public Gender Gender { get; set; }

        [JsonProperty("birthday")]
        public string Birthday { get; set; }

        [JsonProperty("photo")]
        public string Photo { get; set; }

        [JsonProperty("active")]
        public bool IsActive { get; set; }

        [JsonProperty("locked")]
        public bool IsLocked { get; set; }

        [JsonProperty("locktime")]
        public string LockTime { get; set; }

        [JsonProperty("latest")]
        public string LatestLoginTime { get; set; }
    }
}
