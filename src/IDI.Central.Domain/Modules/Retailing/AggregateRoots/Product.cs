using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.Retailing.AggregateRoots
{
    public class Product : AggregateRoot
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string QRCode { get; set; }

        public string Tags { get; set; }

        public bool Enabled { get; set; } = true;

        public bool OnShelf  { get; set; } = false;

        public List<ProductPrice> Prices { get; set; }
    }
}
