using System;
using IDI.Central.Domain.Modules.Retailing.Commands;
using IDI.Central.Domain.Modules.Retailing.Queries;
using IDI.Central.Models.Retailing;
using IDI.Central.Providers;
using IDI.Core.Common;
using IDI.Core.Common.Basetypes;
using IDI.Core.Common.Extensions;
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
                Tags = input.Tags.ToJson(),
            };

            return ServiceLocator.CommandBus.Send(command);
        }

        // GET: api/products
        [HttpGet]
        public Result<Collection<ProductModel>> Get()
        {
            return ServiceLocator.QueryProcessor.Execute<QueryProductCondition, Collection<ProductModel>>();
        }

        // Put: api/products
        [HttpPut("{id}")]
        public Result Put(Guid id, [FromBody]string value)
        {
            throw new NotImplementedException();
        }

        // GET api/products/{id}
        [HttpGet("{id}")]
        public Result<ProductModel> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public Result Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
