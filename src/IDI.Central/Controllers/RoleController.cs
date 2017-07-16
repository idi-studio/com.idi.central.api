using IDI.Central.Common;
using IDI.Central.Domain.Modules.Administration.Commands;
using IDI.Central.Models.Administration;
using IDI.Core.Common;
using IDI.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.APIs
{
    [Route("api/role")]
    public class RoleController : Controller
    {
        [HttpPost]
        public Result Post([FromBody]RoleCreationInput input)
        {
            return ServiceLocator.CommandBus.Send(new RoleCreationCommand(input.RoleName));
        }

        //// GET: api/values
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

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
