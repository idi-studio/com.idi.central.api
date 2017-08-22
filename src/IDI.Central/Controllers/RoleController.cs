using IDI.Central.Domain.Modules.Administration.Commands;
using IDI.Central.Domain.Modules.Administration.Queries;
using IDI.Central.Models.Administration;
using IDI.Central.Providers;
using IDI.Core.Common;
using IDI.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/role"), ApplicationAuthorize]
    public class RoleController : Controller
    {
        // POST: api/role
        [HttpPost]
        public Result Post([FromBody]RoleCreationInput input)
        {
            return ServiceLocator.CommandBus.Send(new RoleCreationCommand(input.RoleName));
        }

        // GET: api/role/list
        [HttpGet("list")]
        public Result<Set<RoleModel>> List()
        {
            return ServiceLocator.QueryProcessor.Execute<QueryRolesCondition, Set<RoleModel>>();
        }
    }
}
