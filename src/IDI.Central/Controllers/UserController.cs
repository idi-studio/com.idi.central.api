using IDI.Central.Core;
using IDI.Central.Domain.Modules.Administration.Commands;
using IDI.Central.Domain.Modules.Administration.Queries;
using IDI.Central.Models.Administration;
using IDI.Core.Common;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/user"), ApplicationAuthorize]
    public class UserController : Controller
    {
        private readonly ICommandBus commandBus;
        private readonly IQueryProcessor queryProcessor;

        public UserController(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            this.commandBus = commandBus;
            this.queryProcessor = queryProcessor;
        }

        // POST: api/user
        [HttpPost]
        public Result Post([FromBody]UserRegistrationInput input)
        {
            return commandBus.Send(new UserRegistrationCommand(input.UserName, input.Password, input.Confirm));
        }

        // GET: api/user/list
        [HttpGet("list")]
        public Result<Set<UserModel>> List()
        {
            return queryProcessor.Execute<QueryUsersCondition, Set<UserModel>>();
        }
    }
}
