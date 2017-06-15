using System;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.SCM.AggregateRoots
{
    public class UserRole : AggregateRoot
    {
        public Guid UserId { get; set; }

        public User User { get; set; }

        public Guid RoleId { get; set; }

        public Role Role { get; set; }
    }
}
