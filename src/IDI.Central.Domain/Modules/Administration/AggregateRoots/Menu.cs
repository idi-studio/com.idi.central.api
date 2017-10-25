using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.Administration.AggregateRoots
{
    [Table("Menus")]
    public class Menu : AggregateRoot
    {
        public int SN { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Route { get; set; }

        public bool Actived { get; set; } = true;

        public Guid ModuleId { get; set; }

        public Module Module { get; set; }
    }
}
