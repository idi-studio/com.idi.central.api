using System.ComponentModel.DataAnnotations;
using IDI.Core.Authentication;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.Administration.AggregateRoots
{
    public class Role : AggregateRoot, IRole
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Descrition { get; set; }

        public bool IsActive { get; set; } = true;

        public string Permissions { get; set; }
    }
}
