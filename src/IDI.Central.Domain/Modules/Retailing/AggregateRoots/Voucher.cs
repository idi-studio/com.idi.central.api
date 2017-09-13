using System;
using System.ComponentModel.DataAnnotations;
using IDI.Central.Common;
using IDI.Central.Common.Enums;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.Retailing.AggregateRoots
{
    public class Voucher : AggregateRoot
    {
        public string TN { get; set; }

        public PayMethod PayMethod { get; set; }

        public DateTime Date { get; set; }

        public decimal PayAmount { get; set; }

        public decimal OrderAmount { get; set; }

        public byte[] Document { get; set; }

        [StringLength(50)]
        public string ContentType { get; set; }

        [StringLength(200)]
        public string Remark { get; set; }

        public Guid OrderId { get; set; }

        public Order Order { get; set; }
    }
}
