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
    [Route("api/role"), ApplicationAuthorize]
    public class RoleController : Controller
    {
        private readonly ICommandBus commandBus;
        private readonly IQueryProcessor queryProcessor;

        public RoleController(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            this.commandBus = commandBus;
            this.queryProcessor = queryProcessor;
        }

        // POST: api/role
        [HttpPost]
        public Result Post([FromBody]RoleCreationInput input)
        {
            return commandBus.Send(new RoleCreationCommand(input.RoleName));
        }

        // GET: api/role/list
        [HttpGet("list")]
        public Result<Set<RoleModel>> List()
        {
            return queryProcessor.Execute<QueryRolesCondition, Set<RoleModel>>();
        }
    }
}
