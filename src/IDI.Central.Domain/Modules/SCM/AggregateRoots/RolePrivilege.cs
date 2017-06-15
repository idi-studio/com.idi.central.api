using System;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.SCM.AggregateRoots
{
    public class RolePrivilege : AggregateRoot
    {
        public Guid RoleId { get; set; }

        public Role Role { get; set; }

        public Guid PrivilegeId { get; set; }

        public Privilege Privilege { get; set; }
    }
}
