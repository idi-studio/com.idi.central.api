using System;
using System.ComponentModel.DataAnnotations.Schema;
using IDI.Central.Common.Enums;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.BasicInfo.AggregateRoots
{
    [Table("ProductPrices")]
    public class ProductPrice : AggregateRoot
    {
        public PriceCategory Category { get; set; }

        public decimal Amount { get; set; }

        public int GradeFrom { get; set; }

        public int GradeTo { get; set; }

        public DateTime PeriodStart  { get; set; }

        public DateTime PeriodEnd { get; set; }

        public bool Enabled { get; set; }

        public Guid ProductId { get; set; }

        public Product Product { get; set; }
    }
}
