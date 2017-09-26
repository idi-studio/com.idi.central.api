using System;
using IDI.Central.Core;
using IDI.Central.Domain.Modules.Sales.Commands;
using IDI.Central.Models.Sales;
using IDI.Core.Authentication;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/order/item"), ApplicationAuthorize]
    [Module(Common.Constants.Module.Sales)]
    public class OrderItemController : Controller, IAuthorizable
    {
        private readonly ICommandBus commandBus;
        private readonly IQueryProcessor queryProcessor;

        public OrderItemController(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            this.commandBus = commandBus;
            this.queryProcessor = queryProcessor;
        }

        [HttpPost]
        [Permission("order-item", PermissionType.Add)]
        public Result Post([FromBody]OrderItemInput input)
        {
            var command = new OrderItemCommand
            {
                OrderId = input.OrderId,
                ProductId = input.ProductId,
                Quantity = input.Quantity,
                PriceId = input.PriceId,
                Mode = CommandMode.Create,
                Group = VerificationGroup.Create,
            };

            return commandBus.Send(command);
        }

        [HttpPut("{id}")]
        [Permission("order-item", PermissionType.Modify)]
        public Result Put(Guid id, [FromBody]OrderItemInput input)
        {
            var command = new OrderItemCommand
            {
                Id = id,
                OrderId = input.OrderId,
                ProductId = input.ProductId,
                Quantity = input.Quantity,
                PriceId = input.PriceId,
                Mode = CommandMode.Update,
                Group = VerificationGroup.Update,
            };

            return commandBus.Send(command);
        }

        [HttpDelete("{id}")]
        [Permission("order-item", PermissionType.Remove)]
        public Result Delete(Guid id)
        {
            var command = new OrderItemCommand { Id = id, Mode = CommandMode.Delete, Group = VerificationGroup.Delete };

            return commandBus.Send(command);
        }
    }
}
