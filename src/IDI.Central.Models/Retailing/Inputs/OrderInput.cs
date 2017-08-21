using System;
using IDI.Central.Common.Enums;
using IDI.Core.Common;

namespace IDI.Central.Models.Retailing
{
    public class OrderInput : IInput
    {
        public Guid Id { get; set; }

        public OrderCategory Category { get; set; }

        public string Remark { get; set; }

        public Guid CustomerId { get; set; }
    }
}
