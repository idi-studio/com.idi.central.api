using System;
using IDI.Central.Common;
using IDI.Central.Core;
using IDI.Central.Domain.Modules.BasicInfo.Commands;
using IDI.Central.Domain.Modules.BasicInfo.Queries;
using IDI.Central.Models.BasicInfo;
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
        private readonly ICommandBus bus;
        private readonly IQuerier querier;

        public ProductPriceController(ICommandBus bus, IQuerier querier)
        {
            this.bus = bus;
            this.querier = querier;
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

            return bus.Send(command);
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

            return bus.Send(command);
        }

        [HttpGet("{id}")]
        [Permission("product-price", PermissionType.Read)]
        public Result<ProductPriceModel> Get(Guid id)
        {
            var condition = new QueryProductPriceCondition { Id = id };

            return querier.Execute<QueryProductPriceCondition, ProductPriceModel>(condition);
        }

        [HttpDelete("{id}")]
        [Permission("product-price", PermissionType.Remove)]
        public Result Delete(Guid id)
        {
            var command = new ProductPriceCommand { Id = id, Mode = CommandMode.Delete, Group = VerificationGroup.Delete };

            return bus.Send(command);
        }

        [HttpGet("list/{id}")]
        [Permission("product-price", PermissionType.Query)]
        public Result<Set<ProductPriceModel>> List(Guid id)
        {
            var condition = new QueryProductPriceSetCondition { ProductId = id };

            return querier.Execute<QueryProductPriceSetCondition, Set<ProductPriceModel>>(condition);
        }
    }
}
