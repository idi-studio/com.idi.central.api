using System;
using IDI.Central.Common.Enums;
using IDI.Central.Core;
using IDI.Central.Domain.Modules.Sales.Commands;
using IDI.Central.Domain.Modules.Sales.Queries;
using IDI.Central.Models.Sales;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/order"), ApplicationAuthorize]
    public class OrderController : Controller
    {
        private readonly ICommandBus commandBus;
        private readonly IQueryProcessor queryProcessor;

        public OrderController(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            this.commandBus = commandBus;
            this.queryProcessor = queryProcessor;
        }

        [HttpPost]
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

            return commandBus.Send(command);
        }

        [HttpGet("list")]
        public Result<Set<OrderModel>> List()
        {
            var condition = new QueryOrderSetCondition { Category = OrderCategory.Sales, Deadline = DateTime.Now.AddMonths(-3) };

            return queryProcessor.Execute<QueryOrderSetCondition, Set<OrderModel>>(condition);
        }

        [HttpPut("{id}")]
        public Result Put(Guid id, [FromBody]OrderInput input)
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

            return commandBus.Send(command);
        }

        [HttpPut("confirm/{id}")]
        public Result Confirm(Guid id)
        {
            var command = new OrderCommand
            {
                Id = id,
                Category = OrderCategory.Sales,
                Status = OrderStatus.Confirmed,
                Mode = CommandMode.Update,
                Group = VerificationGroup.Update,
            };

            return commandBus.Send(command);
        }

        // GET api/order/{id}
        [HttpGet("{id}")]
        public Result<OrderModel> Get(Guid id)
        {
            var condition = new QueryOrderCondition { Id = id };

            return queryProcessor.Execute<QueryOrderCondition, OrderModel>(condition);
        }

        // DELETE api/order/{id}
        [HttpDelete("{id}")]
        public Result Delete(Guid id)
        {
            var command = new OrderCommand { Id = id, Mode = CommandMode.Delete, Group = VerificationGroup.Delete };

            return commandBus.Send(command);
        }
    }
}
