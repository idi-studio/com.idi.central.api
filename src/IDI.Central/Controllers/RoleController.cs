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
    [Route("api/role"), ApplicationAuthorize]
    [Module(Configuration.Modules.Administration)]
    public class RoleController : Controller, IAuthorizable
    {
        private readonly ICommandBus bus;
        private readonly IQuerier querier;

        public RoleController(ICommandBus bus, IQuerier querier)
        {
            this.bus = bus;
            this.querier = querier;
        }

        [HttpPost]
        [Permission("role", PermissionType.Add)]
        public Result Post([FromBody]RoleCreationInput input)
        {
            return bus.Send(new RoleCreationCommand(input.RoleName));
        }

        [HttpGet("list")]
        [Permission("role", PermissionType.Query)]
        public Result<Set<RoleModel>> List()
        {
            return querier.Execute<QueryRoleSetCondition, Set<RoleModel>>();
        }

        [HttpGet("permission/{name}")]
        [Permission("role-permission", PermissionType.Query)]
        public Result<RolePermissionModel> Permission(string name)
        {
            var condition = new QueryRolePermissionCondition { Name = name };

            return querier.Execute<QueryRolePermissionCondition, RolePermissionModel>(condition);
        }

        [HttpPut("authorize")]
        [Permission("role-authorize", PermissionType.Modify)]
        public Result Put([FromBody]RoleAuthorizeInput input)
        {
            var command = new RoleAuthorizeCommand
            {
                Role = input.Role,
                Permissions = input.Permissions,
                Mode = CommandMode.Update,
                Group = VerificationGroup.Update,
            };

            return bus.Send(command);
        }
    }
}
