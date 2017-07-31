using IDI.Central.Domain.Modules.Retailing.Commands;
using IDI.Central.Domain.Modules.Retailing.Queries;
using IDI.Central.Models.Common;
using IDI.Central.Models.Retailing;
using IDI.Central.Providers;
using IDI.Core.Common;
using IDI.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/products"), ApplicationAuthorize]
    public class ProductsController : Controller
    {
        //POST: api/products
        [HttpPost]
        public Result Post([FromBody]ProductCreationInput input)
        {
            var command = new ProductCreationCommand
            {
                Name = input.Name,
                Code = input.Code,
                Profile = input.Profile,
            };

            return ServiceLocator.CommandBus.Send(command);
        }

        // GET: api/products
        [HttpGet]
        public Result<Table<ProductRow>> Get()
        {
            return ServiceLocator.QueryProcessor.Execute<QueryProductCondition, Table<ProductRow>>();
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
