using System;
using IDI.Core.Common;
using Newtonsoft.Json;

namespace IDI.Central.Models.Sales.Inputs
{
    public class ShippingAddressInput : IInput
    {
        [JsonProperty("aid")]
        public Guid Id { get; set; }

        [JsonProperty("cid")]
        public Guid CustomerId { get; set; }

        [JsonProperty("receiver")]
        public string Receiver { get; set; }

        [JsonProperty("contactno")]
        public string ContactNo { get; set; }

        [JsonProperty("province")]
        public string Province { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("area")]
        public string Area { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("detail")]
        public string Detail { get; set; }

        [JsonProperty("postcode")]
        public string Postcode { get; set; }

        [JsonProperty("default")]
        public bool Default { get; set; }
    }
}
