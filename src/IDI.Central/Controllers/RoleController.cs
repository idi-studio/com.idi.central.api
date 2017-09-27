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
    [Route("api/role"), ApplicationAuthorize]
    [Module(Configuration.Modules.Administration)]
    public class RoleController : Controller, IAuthorizable
    {
        private readonly ICommandBus commandBus;
        private readonly IQueryProcessor queryProcessor;

        public RoleController(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            this.commandBus = commandBus;
            this.queryProcessor = queryProcessor;
        }

        [HttpPost]
        [Permission("role", PermissionType.Add)]
        public Result Post([FromBody]RoleCreationInput input)
        {
            return commandBus.Send(new RoleCreationCommand(input.RoleName));
        }

        [HttpGet("list")]
        [Permission("role", PermissionType.Query)]
        public Result<Set<RoleModel>> List()
        {
            return queryProcessor.Execute<QueryRolesCondition, Set<RoleModel>>();
        }
    }
}
