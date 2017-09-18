using System;
using System.Collections.Generic;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Queries;
using Newtonsoft.Json;

namespace IDI.Central.Models.Sales
{
    public class CustomerModel : IModel
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
        public string PhoneNum { get; set; }

        [JsonProperty("verified")]
        public bool Verified { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("shippings")]
        public List<ShippingAddressModel> Shippings { get; set; } = new List<ShippingAddressModel>();
    }
}
