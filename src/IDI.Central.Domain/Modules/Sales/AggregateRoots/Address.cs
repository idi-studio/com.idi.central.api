using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.Sales.AggregateRoots
{
    [Table("Addresses")]
    public class ShippingAddress : Address
    {
        [Required]
        [StringLength(30)]
        public string Receiver { get; set; }

        [StringLength(20)]
        public string ContactNo { get; set; }

        public Guid CustomerId { get; set; }

        public Customer Customer { get; set; }
    }

    public class Address : AggregateRoot
    {
        [Required]
        [StringLength(100)]
        public string Province { get; set; }

        [Required]
        [StringLength(100)]
        public string City { get; set; }

        [Required]
        [StringLength(100)]
        public string Area { get; set; }

        [Required]
        [StringLength(200)]
        public string Street { get; set; }

        [Required]
        [StringLength(200)]
        public string Detail { get; set; }

        [Required]
        [StringLength(20)]
        public string Postcode { get; set; }
    }
}
