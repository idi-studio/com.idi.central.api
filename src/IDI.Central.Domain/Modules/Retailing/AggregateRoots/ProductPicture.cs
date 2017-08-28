using System;
using System.ComponentModel.DataAnnotations;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.Retailing.AggregateRoots
{
    public class ProductPicture : AggregateRoot
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string FileName { get; set; }

        [Required]
        [StringLength(10)]
        public string Extension { get; set; }

        public byte[] Image { get; set; }

        public Guid ProductId { get; set; }

        public Product Product { get; set; }
    }
}
