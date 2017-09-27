using System;
using IDI.Central.Common;
using IDI.Central.Core;
using IDI.Central.Domain.Modules.Material.Commands;
using IDI.Central.Domain.Modules.Material.Queries;
using IDI.Central.Models.Material;
using IDI.Core.Authentication;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Common.Extensions;
using IDI.Core.Infrastructure.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/product/price"), ApplicationAuthorize]
    [Module(Configuration.Modules.BasicInfo)]
    public class ProductPriceController : Controller, IAuthorizable
    {
        private readonly ICommandBus commandBus;
        private readonly IQueryProcessor queryProcessor;

        public ProductPriceController(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            this.commandBus = commandBus;
            this.queryProcessor = queryProcessor;
        }

        [HttpPost]
        [Permission("product-price", PermissionType.Add)]
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

        [HttpPut("{id}")]
        [Permission("product-price", PermissionType.Modify)]
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

        [HttpGet("{id}")]
        [Permission("product-price", PermissionType.Read)]
        public Result<ProductPriceModel> Get(Guid id)
        {
            var condition = new QueryProductPriceCondition { Id = id };

            return queryProcessor.Execute<QueryProductPriceCondition, ProductPriceModel>(condition);
        }

        [HttpDelete("{id}")]
        [Permission("product-price", PermissionType.Remove)]
        public Result Delete(Guid id)
        {
            var command = new ProductPriceCommand { Id = id, Mode = CommandMode.Delete, Group = VerificationGroup.Delete };

            return commandBus.Send(command);
        }

        [HttpGet("list/{id}")]
        [Permission("product-price", PermissionType.Query)]
        public Result<Set<ProductPriceModel>> List(Guid id)
        {
            var condition = new QueryProductPriceSetCondition { ProductId = id };

            return queryProcessor.Execute<QueryProductPriceSetCondition, Set<ProductPriceModel>>(condition);
        }
    }
}
