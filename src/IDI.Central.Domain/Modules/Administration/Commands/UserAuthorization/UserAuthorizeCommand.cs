using System;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.Verification.Attributes;

namespace IDI.Central.Domain.Modules.Administration.Commands
{
    public class UserAuthorizeCommand : Command
    {
        [RequiredField("用户")]
        public string UserName { get; private set; }

        public Guid[] Roles { get; private set; }

        public UserAuthorizeCommand(string userName, Guid[] roles)
        {
            this.UserName = userName;
            this.Roles = roles;
        }
    }
}
