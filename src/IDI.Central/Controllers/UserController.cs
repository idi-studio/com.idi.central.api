using IDI.Central.Domain.Modules.Administration.Commands;
using IDI.Central.Domain.Modules.Administration.Queries;
using IDI.Central.Models.Administration;
using IDI.Central.Providers;
using IDI.Core.Common;
using IDI.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/user"), ApplicationAuthorize]
    public class UserController : Controller
    {
        // POST: api/user
        [HttpPost]
        public Result Post([FromBody]UserRegistrationInput input)
        {
            return ServiceLocator.CommandBus.Send(new UserRegistrationCommand(input.UserName, input.Password, input.Confirm));
        }

        // GET: api/user/list
        [HttpGet("list")]
        public Result<Set<UserModel>> List()
        {
            return ServiceLocator.QueryProcessor.Execute<QueryUsersCondition, Set<UserModel>>();
        }
    }
}
