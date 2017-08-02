using System;
using IDI.Central.Domain.Localization;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.Verification.Attributes;

namespace IDI.Central.Domain.Modules.Administration.Commands
{
    public class RoleAuthorizationCommand : Command
    {
        [RequiredField(Resources.Key.DisplayName.Role)]
        public string RoleName { get; private set; }

        public Guid[] Privileges { get; private set; }

        public RoleAuthorizationCommand(string rolename, Guid[] privileges)
        {
            this.RoleName = rolename;
            this.Privileges = privileges;
        }
    }
}
