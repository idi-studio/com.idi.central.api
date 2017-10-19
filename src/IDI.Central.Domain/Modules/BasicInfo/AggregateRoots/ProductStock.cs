using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IDI.Central.Common;
using IDI.Central.Domain.Modules.Inventory.AggregateRoots;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.BasicInfo.AggregateRoots
{
    [Table("ProductStock")]
    public class ProductStock : AggregateRoot
    {
        public Guid ProductId { get; set; }

        /// <summary>
        /// Stock Keeping Unit
        /// </summary>
        [Range(0, int.MaxValue)]
        public decimal SKU { get; set; } = 1M;

        [Range(0, int.MaxValue)]
        public decimal SafetyStock { get; set; } = 0M;

        [Required]
        [StringLength(10)]
        public string Uint { get; set; } = "PCS";

        public Guid? StoreId { get; set; }

        [ForeignKey("StoreId")]
        public Store Store { get; set; }

        [Required]
        [StringLength(10)]
        public string BinCode { get; set; } = Configuration.Inventory.DefaultBinCode;
    }
}
