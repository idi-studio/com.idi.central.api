using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.Identity.AggregateRoots
{
    public class Module : AggregateRoot
    {
        public int SN { get; set; }

        [Required]
        [StringLength(10)]
        public string Code { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Icon { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public List<Privilege> Privileges { get; set; } = new List<Privilege>();

        public List<Menu> Menus { get; set; } = new List<Menu>();
    }
}
