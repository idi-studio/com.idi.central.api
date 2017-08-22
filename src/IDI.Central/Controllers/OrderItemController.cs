using System;
using IDI.Central.Domain.Modules.Retailing.Commands;
using IDI.Central.Models.Retailing;
using IDI.Central.Providers;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/order/item"), ApplicationAuthorize]
    public class OrderItemController : Controller
    {
        //POST: api/order/item
        [HttpPost]
        public Result Post([FromBody]OrderItemInput input)
        {
            var command = new OrderItemCommand
            {
                OrderId = input.OrderId,
                ProductId = input.ProductId,
                Quantity = input.Quantity,
                ReadjustUnitPrice = input.ReadjustUnitPrice,
                UnitPrice = input.UnitPrice,
                Mode = CommandMode.Create,
                Group = VerificationGroup.Create,
            };

            return ServiceLocator.CommandBus.Send(command);
        }

        // Put: api/order/item
        [HttpPut("{id}")]
        public Result Put(Guid id, [FromBody]OrderItemInput input)
        {
            var command = new OrderItemCommand
            {
                Id = id,
                OrderId = input.OrderId,
                ProductId = input.ProductId,
                Quantity = input.Quantity,
                ReadjustUnitPrice = input.ReadjustUnitPrice,
                UnitPrice = input.UnitPrice,
                Mode = CommandMode.Update,
                Group = VerificationGroup.Update,
            };

            return ServiceLocator.CommandBus.Send(command);
        }

        // DELETE api/order/item/{id}
        [HttpDelete("{id}")]
        public Result Delete(Guid id)
        {
            var command = new OrderItemCommand { Id = id, Mode = CommandMode.Delete, Group = VerificationGroup.Delete };

            return ServiceLocator.CommandBus.Send(command);
        }
    }
}
