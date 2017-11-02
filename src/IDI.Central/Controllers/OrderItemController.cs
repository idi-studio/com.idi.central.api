using System;
using IDI.Central.Common;
using IDI.Central.Core;
using IDI.Central.Domain.Modules.Sales.Commands;
using IDI.Central.Models.Sales;
using IDI.Core.Authentication;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/order/item"), ApplicationAuthorize]
    [Module(Configuration.Modules.Sales)]
    public class OrderItemController : Controller, IAuthorizable
    {
        private readonly ICommandBus bus;
        private readonly IQuerier querier;

        public OrderItemController(ICommandBus bus, IQuerier querier)
        {
            this.bus = bus;
            this.querier = querier;
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
                Group = ValidationGroup.Create,
            };

            return bus.Send(command);
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
                Group = ValidationGroup.Update,
            };

            return bus.Send(command);
        }

        [HttpDelete("{id}")]
        [Permission("order-item", PermissionType.Remove)]
        public Result Delete(Guid id)
        {
            var command = new OrderItemCommand { Id = id, Mode = CommandMode.Delete, Group = ValidationGroup.Delete };

            return bus.Send(command);
        }
    }
}
