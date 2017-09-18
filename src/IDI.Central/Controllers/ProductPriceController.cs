using System;
using IDI.Central.Core;
using IDI.Central.Domain.Modules.Material.Commands;
using IDI.Central.Domain.Modules.Material.Queries;
using IDI.Central.Models.Material;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Common.Extensions;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/product/price"), ApplicationAuthorize]
    public class ProductPriceController : Controller
    {
        private readonly ICommandBus commandBus;
        private readonly IQueryProcessor queryProcessor;

        public ProductPriceController(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            this.commandBus = commandBus;
            this.queryProcessor = queryProcessor;
        }

        //POST: api/product/price
        [HttpPost]
        public Result Post([FromBody]ProductPriceInput input)
        {
            var command = new ProductPriceCommand
            {
                ProductId = input.ProductId,
                Category = input.Category,
                PeriodStart = input.StartDate.AsDate(),
                PeriodEnd = input.DueDate.AsDate(),
                Amount = input.Amount,
                GradeFrom = input.GradeFrom,
                GradeTo = input.GradeTo,
                Enabled = input.Enabled,
                Mode = CommandMode.Create,
                Group = VerificationGroup.Create,
            };

            return commandBus.Send(command);
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
                PeriodStart = input.StartDate.AsDate(),
                PeriodEnd = input.DueDate.AsDate(),
                Amount = input.Amount,
                GradeFrom = input.GradeFrom,
                GradeTo = input.GradeTo,
                Enabled = input.Enabled,
                Mode = CommandMode.Update,
                Group = VerificationGroup.Update,
            };

            return commandBus.Send(command);
        }

        // GET api/product/price/{id}
        [HttpGet("{id}")]
        public Result<ProductPriceModel> Get(Guid id)
        {
            var condition = new QueryProductPriceCondition { Id = id };

            return queryProcessor.Execute<QueryProductPriceCondition, ProductPriceModel>(condition);
        }

        // DELETE api/product/price/{id}
        [HttpDelete("{id}")]
        public Result Delete(Guid id)
        {
            var command = new ProductPriceCommand { Id = id, Mode = CommandMode.Delete, Group = VerificationGroup.Delete };

            return commandBus.Send(command);
        }

        // GET: api/product/price/list/{id}
        [HttpGet("list/{id}")]
        public Result<Set<ProductPriceModel>> List(Guid id)
        {
            var condition = new QueryProductPriceSetCondition { ProductId = id };

            return queryProcessor.Execute<QueryProductPriceSetCondition, Set<ProductPriceModel>>(condition);
        }
    }
}
