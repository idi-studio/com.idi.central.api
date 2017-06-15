using System;
using System.ComponentModel.DataAnnotations;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.SCM.AggregateRoots
{
    public class Menu : AggregateRoot
    {
        public int SN { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        [Required]
        [StringLength(50)]
        public string Controller { get; set; }

        [Required]
        [StringLength(50)]
        public string Action { get; set; }

        public bool Display { get; set; }

        public Guid ParentId { get; set; }

        public Guid ModuleId { get; set; }

        public Module Module { get; set; }
    }
}
