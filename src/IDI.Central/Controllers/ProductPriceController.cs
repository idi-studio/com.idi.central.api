using System;
using IDI.Central.Domain.Modules.Retailing.Commands;
using IDI.Central.Domain.Modules.Retailing.Queries;
using IDI.Central.Models.Retailing;
using IDI.Central.Core;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Common.Extensions;
using IDI.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/product/price"), ApplicationAuthorize]
    public class ProductPriceController : Controller
    {
        //POST: api/product/price
        [HttpPost]
        public Result Post([FromBody]ProductPriceInput input)
        {
            var command = new ProductPriceCommand
            {
                ProductId = input.ProductId,
                Category = input.Category,
                StartDate = input.StartDate.AsDate(),
                DueDate = input.DueDate.AsDate(),
                Amount = input.Amount,
                Grade = input.Grade,
                Enabled = input.Enabled,
                Mode = CommandMode.Create,
                Group = VerificationGroup.Create,
            };

            return ServiceLocator.CommandBus.Send(command);
        }

        // Put: api/product/price
        [HttpPut("{id}")]
        public Result Put(Guid id, [FromBody]ProductPriceInput input)
        {
            var command = new ProductPriceCommand
            {
                Id = id,
                ProductId = input.ProductId,
                Category = input.Category,
                StartDate = input.StartDate.AsDate(),
                DueDate = input.DueDate.AsDate(),
                Amount = input.Amount,
                Grade = input.Grade,
                Enabled = input.Enabled,
                Mode = CommandMode.Update,
                Group = VerificationGroup.Update,
            };

            return ServiceLocator.CommandBus.Send(command);
        }

        // GET api/product/price/{id}
        [HttpGet("{id}")]
        public Result<ProductPriceModel> Get(Guid id)
        {
            var condition = new QueryProductPriceCondition { Id = id };

            return ServiceLocator.QueryProcessor.Execute<QueryProductPriceCondition, ProductPriceModel>(condition);
        }

        // DELETE api/product/price/{id}
        [HttpDelete("{id}")]
        public Result Delete(Guid id)
        {
            var command = new ProductPriceCommand { Id = id, Mode = CommandMode.Delete, Group = VerificationGroup.Delete };

            return ServiceLocator.CommandBus.Send(command);
        }

        // GET: api/product/price/list/{id}
        [HttpGet("list/{id}")]
        public Result<Set<ProductPriceModel>> List(Guid id)
        {
            var condition = new QueryProductPriceSetCondition { ProductId = id };

            return ServiceLocator.QueryProcessor.Execute<QueryProductPriceSetCondition, Set<ProductPriceModel>>(condition);
        }
    }
}
