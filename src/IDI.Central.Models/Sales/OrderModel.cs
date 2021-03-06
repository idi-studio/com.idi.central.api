﻿using System;
using System.Collections.Generic;
using IDI.Central.Common.Enums;
using IDI.Core.Infrastructure.Queries;
using Newtonsoft.Json;

namespace IDI.Central.Models.Sales
{
    public class OrderModel : IModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("sn")]
        public string SN { get; set; }

        [JsonProperty("category")]
        public DocumentCategory Category { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("status")]
        public SaleStatus Status { get; set; }

        [JsonProperty("statusdesc")]
        public string StatusDesc { get; set; }

        [JsonProperty("remark")]
        public string Remark { get; set; }

        [JsonProperty("custid")]
        public Guid CustomerId { get; set; }

        [JsonProperty("custname")]
        public string CustomerName { get; set; }

        [JsonProperty("items")]
        public List<OrderItemModel> Items { get; set; }
    }
}
