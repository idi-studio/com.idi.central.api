using System;
using IDI.Central.Common.Enums;
using IDI.Central.Domain.Modules.Retailing.Commands;
using IDI.Central.Domain.Modules.Retailing.Queries;
using IDI.Central.Models.Retailing;
using IDI.Central.Providers;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/order"), ApplicationAuthorize]
    public class OrderController : Controller
    {
        //POST: api/order
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

            return ServiceLocator.CommandBus.Send(command);
        }

        // GET: api/order/list
        [HttpGet("list")]
        public Result<Set<OrderModel>> List()
        {
            var condition = new QueryOrderSetCondition { Category = OrderCategory.Sales, Deadline = DateTime.Now.AddMonths(-3) };

            return ServiceLocator.QueryProcessor.Execute<QueryOrderSetCondition, Set<OrderModel>>(condition);
        }

        // Put: api/order
        [HttpPut("{id}")]
        public Result Put(Guid id, [FromBody]OrderInput input)
        {
            var command = new OrderCommand
            {
                Id = id,
                Category = OrderCategory.Sales,
                //Remark = input.Remark,
                //CustomerId = input.CustomerId,
                Mode = CommandMode.Update,
                Group = VerificationGroup.Update,
            };

            return ServiceLocator.CommandBus.Send(command);
        }

        // GET api/order/{id}
        [HttpGet("{id}")]
        public Result<OrderModel> Get(Guid id)
        {
            var condition = new QueryOrderCondition { Id = id };

            return ServiceLocator.QueryProcessor.Execute<QueryOrderCondition, OrderModel>(condition);
        }

        // DELETE api/order/{id}
        [HttpDelete("{id}")]
        public Result Delete(Guid id)
        {
            var command = new OrderCommand { Id = id, Mode = CommandMode.Delete, Group = VerificationGroup.Delete };

            return ServiceLocator.CommandBus.Send(command);
        }
    }
}
