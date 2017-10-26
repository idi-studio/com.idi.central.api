using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IDI.Central.Common.Enums;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.Sales.AggregateRoots
{
    [Table("Orders")]
    public class Order : AggregateRoot
    {
        [Required]
        [StringLength(30)]
        public string SN { get; set; }

        public DocumentCategory Category { get; set; }

        public DateTime Date { get; set; }

        public SaleStatus Status { get; set; }

        [StringLength(200)]
        public string Remark { get; set; }

        public Guid? CustomerId { get; set; }

        public Customer Customer { get; set; }

        public ShippingAddress Shipping { get; set; }

        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
