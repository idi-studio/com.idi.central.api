using System;
using IDI.Central.Domain.Modules.Retailing.Commands;
using IDI.Central.Domain.Modules.Retailing.Queries;
using IDI.Central.Models.Retailing;
using IDI.Central.Providers;
using IDI.Core.Common;
using IDI.Core.Common.Basetypes;
using IDI.Core.Common.Enums;
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
        public Result Post([FromBody]ProductInput input)
        {
            var command = new ProductCommand
            {
                Name = input.Name,
                Code = input.Code,
                Tags = input.Tags.ToJson(),
                Enabled = input.Enabled,
                OnShelf = input.OnShelf,
                Mode = CommandMode.Create,
                Group = Constants.VerificationGroup.Create,
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
        public Result Put(Guid id, [FromBody]ProductInput input)
        {
            var command = new ProductCommand
            {
                Id = id,
                Name = input.Name,
                Code = input.Code,
                Tags = input.Tags.ToJson(),
                Enabled = input.Enabled,
                OnShelf = input.OnShelf,
                Mode = CommandMode.Update,
                Group = Constants.VerificationGroup.Update,
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

        // DELETE api/products/{id}
        [HttpDelete("{id}")]
        public Result Delete(Guid id)
        {
            var command = new ProductCommand { Id = id, Mode = CommandMode.Delete, Group = Constants.VerificationGroup.Delete };

            return ServiceLocator.CommandBus.Send(command);
        }
    }
}
