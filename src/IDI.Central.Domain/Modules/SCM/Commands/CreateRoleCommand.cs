using System;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.Verification.Attributes;

namespace IDI.Central.Domain.Modules.SCM.Commands
{
    public class CreateRoleCommand : Command
    {
        [RequiredField("角色")]
        [StringLength("角色", MaxLength = 20)]
        public string RoleName { get; private set; }

        public CreateRoleCommand(string rolename)
        {
            this.RoleName = rolename;
        }
    }
}
