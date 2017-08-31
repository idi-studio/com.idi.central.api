using System;
using IDI.Central.Domain.Modules.Retailing.Commands;
using IDI.Central.Domain.Modules.Retailing.Queries;
using IDI.Central.Models.Retailing;
using IDI.Central.Providers;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Common.Extensions;
using IDI.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace IDI.Central.Controllers
{
    [Route("api/product"), ApplicationAuthorize]
    public class ProductController : Controller
    {
        public readonly ApplicationOptions options;

        public ProductController(IOptionsSnapshot<ApplicationOptions> options)
        {
            this.options = options.Value;
        }

        //POST: api/product
        [HttpPost]
        public Result Post([FromBody]ProductInput input)
        {
            var command = new ProductCommand
            {
                Name = input.Name,
                Code = input.QRCode,
                Tags = input.Tags.ToJson(),
                Enabled = input.Enabled,
                OnShelf = input.OnShelf,
                Mode = CommandMode.Create,
                Group = VerificationGroup.Create,
            };

            return ServiceLocator.CommandBus.Send(command);
        }

        // GET: api/product/list
        [HttpGet("list")]
        public Result<Set<ProductModel>> List()
        {
            return ServiceLocator.QueryProcessor.Execute<QueryProductSetCondition, Set<ProductModel>>();
        }

        // GET: api/product/selling
        [HttpGet("selling")]
        public Result<Set<ProductSellModel>> Selling()
        {
            return ServiceLocator.QueryProcessor.Execute<QueryProductSellSetCondition, Set<ProductSellModel>>();
        }

        // Put: api/product
        [HttpPut("{id}")]
        public Result Put(Guid id, [FromBody]ProductInput input)
        {
            var command = new ProductCommand
            {
                Id = id,
                Name = input.Name,
                Code = input.QRCode,
                Tags = input.Tags.ToJson(),
                Enabled = input.Enabled,
                OnShelf = input.OnShelf,
                Mode = CommandMode.Update,
                Group = VerificationGroup.Update,
            };

            return ServiceLocator.CommandBus.Send(command);
        }

        // GET api/product/{id}
        [HttpGet("{id}")]
        public Result<ProductModel> Get(Guid id)
        {
            var condition = new QueryProductCondition
            {
                Id = id,
                Domain = this.options.Domain
            };

            return ServiceLocator.QueryProcessor.Execute<QueryProductCondition, ProductModel>(condition);
        }

        // DELETE api/product/{id}
        [HttpDelete("{id}")]
        public Result Delete(Guid id)
        {
            var command = new ProductCommand { Id = id, Mode = CommandMode.Delete, Group = VerificationGroup.Delete };

            return ServiceLocator.CommandBus.Send(command);
        }
    }
}
