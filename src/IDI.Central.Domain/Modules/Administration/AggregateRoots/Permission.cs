using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IDI.Core.Authentication;
using IDI.Core.Common.Enums;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.Administration.AggregateRoots
{
    [Table("Permissions")]
    public class Permission : AggregateRoot, IPermission
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        public PermissionType Type { get; set; }

        [Required]
        [StringLength(20)]
        public string Module { get; set; }
    }
}
