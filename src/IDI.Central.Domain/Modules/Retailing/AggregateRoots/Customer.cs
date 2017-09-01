using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.Retailing.AggregateRoots
{
    public class Customer : AggregateRoot
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public int Grade { get; set; }

        public List<ShippingAddress> Shippings { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();

        public Guid? UserId { get; set; }

        public User User { get; set; }
    }
}
