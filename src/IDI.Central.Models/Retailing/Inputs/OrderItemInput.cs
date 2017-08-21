using System;
using IDI.Core.Common;

namespace IDI.Central.Models.Retailing
{
    public class OrderItemInput : IInput
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal? ReadjustUnitPrice { get; set; }

        public decimal Quantity { get; set; }
    }
}
