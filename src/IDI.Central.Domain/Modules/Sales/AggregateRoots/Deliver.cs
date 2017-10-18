using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IDI.Central.Common.Enums;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.Sales.AggregateRoots
{
    [Table("Delivers")]
    public class Deliver : AggregateRoot
    {
        [Required]
        [StringLength(50)]
        public string CourierNo { get; set; }

        public DeliverStatus Status { get; set; }

        public string Details { get; set; }

        public Guid OrderId { get; set; }

        public Order Order { get; set; }
    }
}
