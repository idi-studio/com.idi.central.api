using IDI.Central.Common;
using IDI.Central.Core;
using IDI.Central.Domain.Modules.Administration.Commands;
using IDI.Central.Domain.Modules.Administration.Queries;
using IDI.Central.Models.Administration;
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
        private readonly ICommandBus commandBus;
        private readonly IQuerier queryProcessor;

        public UserController(ICommandBus commandBus, IQuerier queryProcessor)
        {
            this.commandBus = commandBus;
            this.queryProcessor = queryProcessor;
        }

        [HttpPost]
        [Permission("user", PermissionType.Add)]
        public Result Post([FromBody]UserRegistrationInput input)
        {
            return commandBus.Send(new UserRegistrationCommand(input.UserName, input.Password, input.Confirm));
        }

        [HttpGet("list")]
        [Permission("user", PermissionType.Query)]
        public Result<Set<UserModel>> List()
        {
            return queryProcessor.Execute<QueryUsersCondition, Set<UserModel>>();
        }
    }
}
