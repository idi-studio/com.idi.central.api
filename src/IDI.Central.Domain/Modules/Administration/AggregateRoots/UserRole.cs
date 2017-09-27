﻿using System;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.Administration.AggregateRoots
{
    public class UserRole : AggregateRoot
    {
        public Guid UserId { get; set; }

        public string Roles { get; set; }

        //public User User { get; set; }

        //public Guid RoleId { get; set; }

        //public Role Role { get; set; }
    }
}
