using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IDI.Core.Common.Enums;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.Administration.AggregateRoots
{
    public class Permission : AggregateRoot
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        public PermissionType Type { get; set; }

        public Guid ModuleId { get; set; }

        public Module Module { get; set; }

        public List<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
