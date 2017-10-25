using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.Administration.AggregateRoots
{
    [Table("Modules")]
    public class Module : AggregateRoot
    {
        public int SN { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Icon { get; set; }

        [Required]
        [StringLength(50)]
        public string Route { get; set; } = string.Empty;

        public List<Menu> Menus { get; set; } = new List<Menu>();
    }
}
