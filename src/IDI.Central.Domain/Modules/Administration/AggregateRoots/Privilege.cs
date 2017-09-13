using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IDI.Central.Common.Enums;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.Administration.AggregateRoots
{
    public class Privilege : AggregateRoot
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        public PrivilegeType PrivilegeType { get; set; }

        public Guid ModuleId { get; set; }

        public Module Module { get; set; }

        public List<RolePrivilege> RolePrivileges { get; set; } = new List<RolePrivilege>();
    }
}
