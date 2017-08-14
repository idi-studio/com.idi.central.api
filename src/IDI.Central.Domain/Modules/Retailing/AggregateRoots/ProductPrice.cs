using System;
using IDI.Central.Common;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.Retailing.AggregateRoots
{
    public class ProductPrice : AggregateRoot
    {
        public PriceCategory Category { get; set; }

        public decimal Amount { get; set; }

        public int Grade { get; set; } = 0;

        public DateTime StartDate { get; set; }

        public DateTime DueDate { get; set; }

        public Guid ProductId { get; set; }

        public Product Product { get; set; }
    }
}
