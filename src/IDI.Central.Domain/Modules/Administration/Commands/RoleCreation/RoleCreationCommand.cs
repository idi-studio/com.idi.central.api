using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.Verification.Attributes;

namespace IDI.Central.Domain.Modules.Administration.Commands
{
    public class RoleCreationCommand : Command
    {
        [RequiredField("角色")]
        [StringLength("角色", MaxLength = 20)]
        public string RoleName { get; private set; }

        public RoleCreationCommand(string rolename)
        {
            this.RoleName = rolename;
        }
    }
}
