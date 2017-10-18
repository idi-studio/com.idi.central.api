using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IDI.Central.Common.Enums;
using IDI.Central.Domain.Modules.BasicInfo.AggregateRoots;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.Inventory.AggregateRoots
{
    [Table("StoreTrans")]
    public class StoreTrans : AggregateRoot
    {
        public Guid StoreId { get; set; }

        [ForeignKey("StoreId")]
        public Store Store { get; set; }

        [Required]
        [StringLength(10)]
        public string BinCode { get; set; }

        public Guid ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public decimal Quantity { get; set; }

        public StoreTransType TransType { get; set; }
    }
}
