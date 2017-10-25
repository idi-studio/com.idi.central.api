using System;
using IDI.Central.Common;
using IDI.Central.Common.Enums;
using IDI.Central.Core;
using IDI.Central.Domain.Modules.Sales.Commands;
using IDI.Central.Domain.Modules.Sales.Queries;
using IDI.Central.Models.Sales;
using IDI.Core.Authentication;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/order"), ApplicationAuthorize]
    [Module(Configuration.Modules.Sales)]
    public class OrderController : Controller, IAuthorizable
    {
        private readonly ICommandBus bus;
        private readonly IQuerier querier;

        public OrderController(ICommandBus bus, IQuerier querier)
        {
            this.bus = bus;
            this.querier = querier;
        }

        [HttpPost]
        [Permission("order", PermissionType.Add)]
        public Result Post([FromBody]OrderInput input)
        {
            var command = new OrderCommand
            {
                Category = OrderCategory.Sales,
                Remark = input.Remark,
                CustomerId = input.CustomerId,
                Mode = CommandMode.Create,
                Group = VerificationGroup.Create,
            };

            return bus.Send(command);
        }

        [HttpGet("list")]
        [Permission("order", PermissionType.Query)]
        public Result<Set<OrderModel>> List()
        {
            var condition = new QueryOrderSetCondition { Category = OrderCategory.Sales, Deadline = DateTime.Now.AddMonths(-3) };

            return querier.Execute<QueryOrderSetCondition, Set<OrderModel>>(condition);
        }

        [HttpPut("{id}")]
        [Permission("order", PermissionType.Modify)]
        public Result Save(Guid id, [FromBody]OrderInput input)
        {
            var command = new OrderCommand
            {
                Id = id,
                Category = OrderCategory.Sales,
                CustomerId = input.CustomerId,
                Status = input.Status,
                Remark = input.Remark,
                Mode = CommandMode.Update,
                Group = VerificationGroup.Update,
            };

            return bus.Send(command);
        }

        [HttpPut("verify/{id}")]
        [Permission("order", PermissionType.Verify)]
        public Result Verify(Guid id)
        {
            var command = new OrderCommand
            {
                Id = id,
                Category = OrderCategory.Sales,
                Status = OrderStatus.Confirmed,
                Mode = CommandMode.Update,
                Group = VerificationGroup.Update,
            };

            return bus.Send(command);
        }

        [HttpPut("cancel/{id}")]
        [Permission("order", PermissionType.Cancel)]
        public Result Cancel(Guid id)
        {
            var command = new OrderCommand
            {
                Id = id,
                Category = OrderCategory.Sales,
                Status = OrderStatus.Cancelled,
                Mode = CommandMode.Update,
                Group = VerificationGroup.Update,
            };

            return bus.Send(command);
        }

        [HttpGet("{id}")]
        [Permission("order", PermissionType.Read)]
        public Result<OrderModel> Get(Guid id)
        {
            var condition = new QueryOrderCondition { Id = id };

            return querier.Execute<QueryOrderCondition, OrderModel>(condition);
        }

        [HttpDelete("{id}")]
        [Permission("order", PermissionType.Remove)]
        public Result Delete(Guid id)
        {
            var command = new OrderCommand { Id = id, Mode = CommandMode.Delete, Group = VerificationGroup.Delete };

            return bus.Send(command);
        }
    }
}
