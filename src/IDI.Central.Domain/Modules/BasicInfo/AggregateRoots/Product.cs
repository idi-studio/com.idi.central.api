using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IDI.Central.Domain.Modules.Inventory.AggregateRoots;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.BasicInfo.AggregateRoots
{
    [Table("Products")]
    public class Product : AggregateRoot
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string QRCode { get; set; } = Guid.NewGuid().AsCode();

        /// <summary>
        /// JsonType: List of TagModel
        /// </summary>
        public string Tags { get; set; }

        public bool Enabled { get; set; } = true;

        public bool OnShelf  { get; set; } = false;

        public List<ProductPrice> Prices { get; set; } = new List<ProductPrice>();

        public List<ProductPicture> Pictures { get; set; } = new List<ProductPicture>();

        public List<Stock> Stocks { get; set; } = new List<Stock>();
    }
}
