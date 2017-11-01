using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IDI.Central.Common.JsonTypes;
using IDI.Central.Domain.Modules.Inventory.AggregateRoots;
using IDI.Core.Common;
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

        [JsonData(typeof(List<Tag>))]
        public string Tags { get; set; }

        public bool Enabled { get; set; } = true;

        public bool OnShelf  { get; set; } = false;

        public List<ProductPrice> Prices { get; set; } = new List<ProductPrice>();

        public List<ProductPicture> Pictures { get; set; } = new List<ProductPicture>();

        public List<Stock> Stocks { get; set; } = new List<Stock>();

        public ProductStock Stock { get; set; }

        public ProductArticle Article { get; set; }
    }
}
