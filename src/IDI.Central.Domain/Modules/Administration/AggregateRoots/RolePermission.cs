using System;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.Administration.AggregateRoots
{
    public class RolePermission : AggregateRoot
    {
        public Guid RoleId { get; set; }

        public Role Role { get; set; }

        public Guid PermissionId { get; set; }

        public Permission Permission { get; set; }
    }
}
