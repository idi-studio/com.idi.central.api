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
    [Route("api/product/prices"), ApplicationAuthorize]
    public class ProductPricesController : Controller
    {
        //POST: api/product/prices
        [HttpPost]
        public Result Post([FromBody]ProductPriceInput input)
        {
            var command = new ProductPriceCommand
            {
                ProductId = input.ProductId,
                Category = input.Category,
                StartDate = input.StartDate,
                DueDate = input.DueDate,
                Amount = input.Amount,
                Grade = input.Grade,
                Enabled = input.Enabled,
                Mode = CommandMode.Create,
                Group = Constants.VerificationGroup.Create,
            };

            return ServiceLocator.CommandBus.Send(command);
        }

        // GET: api/product/prices
        [HttpGet]
        public Result<Collection<ProductPriceModel>> Get()
        {
            return ServiceLocator.QueryProcessor.Execute<QueryProductPricesCondition, Collection<ProductPriceModel>>();
        }

        // Put: api/product/prices
        [HttpPut("{id}")]
        public Result Put(Guid id, [FromBody]ProductPriceInput input)
        {
            var command = new ProductPriceCommand
            {
                Id = id,
                ProductId = input.ProductId,
                Category = input.Category,
                StartDate = input.StartDate,
                DueDate = input.DueDate,
                Amount = input.Amount,
                Grade = input.Grade,
                Enabled = input.Enabled,
                Mode = CommandMode.Update,
                Group = Constants.VerificationGroup.Update,
            };

            return ServiceLocator.CommandBus.Send(command);
        }

        // GET api/product/prices/{id}
        [HttpGet("{id}")]
        public Result<ProductPriceModel> Get(Guid id)
        {
            var condition = new QueryProductPriceCondition { Id = id };

            return ServiceLocator.QueryProcessor.Execute<QueryProductPriceCondition, ProductPriceModel>(condition);
        }

        // DELETE api/product/prices/{id}
        [HttpDelete("{id}")]
        public Result Delete(Guid id)
        {
            var command = new ProductPriceCommand { Id = id, Mode = CommandMode.Delete, Group = Constants.VerificationGroup.Delete };

            return ServiceLocator.CommandBus.Send(command);
        }
    }
}
