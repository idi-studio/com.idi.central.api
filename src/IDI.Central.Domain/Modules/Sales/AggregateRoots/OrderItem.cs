using System;
using System.ComponentModel.DataAnnotations.Schema;
using IDI.Central.Domain.Modules.BasicInfo.AggregateRoots;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.Sales.AggregateRoots
{
    [Table("OrderItems")]
    public class OrderItem : AggregateRoot
    {
        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Quantity { get; set; }

        public Order Order { get; set; }

        public Product Product { get; set; }
    }
}
