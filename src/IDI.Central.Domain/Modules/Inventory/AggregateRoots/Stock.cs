using System;
using System.ComponentModel.DataAnnotations;
using IDI.Central.Domain.Modules.BasicInfo.AggregateRoots;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.Inventory.AggregateRoots
{
    public class Stock : AggregateRoot
    {
        [Required]
        [StringLength(10)]
        public string BinCode { get; set; } = "MAIN";

        public Guid ProductId { get; set; }

        public Product Product { get; set; }

        public Guid StoreId { get; set; }

        public Store Store { get; set; }

        public decimal Quantity { get; set; }

        public decimal Frozen { get; set; } = 0.00M;
    }
}
