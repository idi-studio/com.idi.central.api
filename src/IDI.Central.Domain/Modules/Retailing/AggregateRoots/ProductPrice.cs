using System;
using IDI.Central.Domain.Common;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.Retailing.AggregateRoots
{
    public class ProductPrice : AggregateRoot
    {
        public PriceCategory Category { get; set; }

        public decimal Amount { get; set; } = 0.00M;

        public DateTime? EffectiveDate { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public bool IsValid { get; set; } = true;

        public Guid ProductId { get; set; }

        public Product Product { get; set; }
    }
}
