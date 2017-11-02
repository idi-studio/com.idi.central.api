using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IDI.Central.Common.JsonTypes;
using IDI.Central.Domain.Modules.BasicInfo.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.Sales.AggregateRoots
{
    [Table("Promotions")]
    public class Promotion : AggregateRoot
    {
        [Required]
        [StringLength(30)]
        public string Subject { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        [Required]
        [JsonData(typeof(PromotionPrice))]
        public string Price { get; set; }

        public bool Enabled { get; set; }

        public Guid ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
