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
            return ServiceLocator.QueryProcessor.Execute<QueryProductsCondition, Collection<ProductModel>>();
        }

        // Put: api/products
        [HttpPut("{id}")]
        public Result Put(Guid id, [FromBody]ProductModificationInput input)
        {
            var command = new ProductModificationCommand
            {
                Id = id,
                Name = input.Name,
                Code = input.Code,
                Tags = input.Tags.ToJson(),
                Enabled = input.Enabled
            };

            return ServiceLocator.CommandBus.Send(command);
        }

        // GET api/products/{id}
        [HttpGet("{id}")]
        public Result<ProductModel> Get(Guid id)
        {
            var condition = new QueryProductCondition { Id = id };

            return ServiceLocator.QueryProcessor.Execute<QueryProductCondition, ProductModel>(condition);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public Result Delete(Guid id)
        {
            var command = new ProductDeletionCommand { Id = id };

            return ServiceLocator.CommandBus.Send(command);
        }
    }
}
