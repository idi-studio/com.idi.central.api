using IDI.Central.Domain.Modules.SCM.Commands;
using IDI.Central.Models.SCM;
using IDI.Central.Common;
using IDI.Core.Common;
using IDI.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.APIs
{
    [Route("api/role")]
    public class RoleController : ApiController
    {
        [HttpPost]
        public Result Post([FromBody]CreateRoleForm form)
        {
            return ServiceLocator.CommandBus.Send(new CreateRoleCommand(form.RoleName));
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
