using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.Inventory.AggregateRoots
{
    public class Store : AggregateRoot
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public bool Inactive { get; set; } = false;

        public List<Stock> Stocks { get; set; } = new List<Stock>();
    }
}
