using System;
using System.ComponentModel.DataAnnotations;
using IDI.Central.Common.Enums;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.Material.AggregateRoots
{
    public class ProductPicture : AggregateRoot
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int Sequence { get; set; }

        public ImageCategory Category { get; set; }

        [Required]
        [StringLength(50)]
        public string FileName { get; set; }

        [Required]
        [StringLength(10)]
        public string Extension { get; set; }

        [Required]
        [StringLength(50)]
        public string ContentType { get; set; }

        public byte[] Data { get; set; }

        public Guid ProductId { get; set; }

        public Product Product { get; set; }
    }
}
