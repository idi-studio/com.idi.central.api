using IDI.Central.Common;
using IDI.Central.Core;
using IDI.Central.Domain.Modules.Administration.Commands;
using IDI.Central.Domain.Modules.Administration.Queries;
using IDI.Central.Models.Administration;
using IDI.Central.Models.Administration.Inputs;
using IDI.Core.Authentication;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/user"), ApplicationAuthorize]
    [Module(Configuration.Modules.Administration)]
    public class UserController : Controller, IAuthorizable
    {
        private readonly ICommandBus bus;
        private readonly IQuerier querier;

        public UserController(ICommandBus bus, IQuerier querier)
        {
            this.bus = bus;
            this.querier = querier;
        }

        [HttpPost]
        [Permission("user", PermissionType.Add)]
        public Result Post([FromBody]UserRegistrationInput input)
        {
            return bus.Send(new UserRegistrationCommand(input.UserName, input.Password, input.Confirm));
        }

        [HttpPost("lock")]
        [Permission("user-lock", PermissionType.Modify)]
        public Result Lock([FromBody]UserLockInput input)
        {
            return bus.Send(new UserLockCommand(input.UserName));
        }

        [HttpGet("list")]
        [Permission("user", PermissionType.Query)]
        public Result<Set<UserModel>> List()
        {
            return querier.Execute<QueryUserSetCondition, Set<UserModel>>();
        }

        [HttpGet("role/{username}")]
        [Permission("user-role", PermissionType.Query)]
        public Result<UserRoleModel> GetUserRole(string username)
        {
            var condition = new QueryUserRoleCondition { UserName = username };

            return querier.Execute<QueryUserRoleCondition, UserRoleModel>(condition);
        }

        [HttpPut("authorize")]
        [Permission("user-authorize", PermissionType.Modify)]
        public Result Put([FromBody]UserAuthorizeInput input)
        {
            var command = new UserAuthorizeCommand
            {
                UserName = input.UserName,
                Roles = input.Roles,
                Mode = CommandMode.Update,
                Group = ValidationGroup.Update,
            };

            return bus.Send(command);
        }
    }
}
