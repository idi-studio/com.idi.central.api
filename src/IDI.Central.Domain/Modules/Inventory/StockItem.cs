using System;
using IDI.Central.Common;
using IDI.Core.Infrastructure.Verification.Attributes;

namespace IDI.Central.Domain.Modules.Inventory
{
    public class StockItem
    {
        public Guid ProductId { get; set; }

        public string BinCode { get; set; } = Configuration.Inventory.DefaultBinCode;

        [DecimalRange(Minimum = 0.01)]
        public decimal Quantity { get; set; }
    }
}
