using IDI.Central.Domain.Modules.Administration.Commands;
using IDI.Central.Domain.Modules.Administration.Queries;
using IDI.Central.Models.Administration;
using IDI.Central.Providers;
using IDI.Core.Common;
using IDI.Core.Common.Basetypes;
using IDI.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/roles"), ApplicationAuthorize]
    public class RolesController : Controller
    {
        // POST: api/roles
        [HttpPost]
        public Result Post([FromBody]RoleCreationInput input)
        {
            return ServiceLocator.CommandBus.Send(new RoleCreationCommand(input.RoleName));
        }

        // GET: api/roles
        [HttpGet]
        public Result<Collection<RoleModel>> Get()
        {
            return ServiceLocator.QueryProcessor.Execute<QueryRolesCondition, Collection<RoleModel>>();
        }

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
