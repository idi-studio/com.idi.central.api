using System;
using IDI.Central.Common.Enums;
using IDI.Central.Domain.Modules.Retailing.Commands;
using IDI.Central.Domain.Modules.Retailing.Queries;
using IDI.Central.Models.Retailing;
using IDI.Central.Providers;
using IDI.Core.Common;
using IDI.Core.Common.Basetypes;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/order"), ApplicationAuthorize]
    public class OrderController : Controller
    {
        //POST: api/orders
        [HttpPost]
        public Result Post([FromBody]OrderInput input)
        {
            var command = new OrderCommand
            {
                Category = OrderCategory.Sales,
                Remark = input.Remark,
                CustomerId = input.CustomerId,
                Mode = CommandMode.Create,
                Group = Constants.VerificationGroup.Create,
            };

            return ServiceLocator.CommandBus.Send(command);
        }

        // GET: api/orders
        [HttpGet]
        public Result<Collection<OrderModel>> Get()
        {
            var condition = new QueryOrdersCondition { Category = OrderCategory.Sales, Deadline = DateTime.Now.AddMonths(-3) };

            return ServiceLocator.QueryProcessor.Execute<QueryOrdersCondition, Collection<OrderModel>>(condition);
        }

        // Put: api/orders
        [HttpPut("{id}")]
        public Result Put(Guid id, [FromBody]OrderInput input)
        {
            var command = new OrderCommand
            {
                Id = id,
                Category = OrderCategory.Sales,
                Remark = input.Remark,
                CustomerId = input.CustomerId,
                Mode = CommandMode.Update,
                Group = Constants.VerificationGroup.Update,
            };

            return ServiceLocator.CommandBus.Send(command);
        }

        // GET api/orders/{id}
        [HttpGet("{id}")]
        public Result<OrderModel> Get(Guid id)
        {
            var condition = new QueryOrderCondition { Id = id };

            return ServiceLocator.QueryProcessor.Execute<QueryOrderCondition, OrderModel>(condition);
        }

        // DELETE api/orders/{id}
        [HttpDelete("{id}")]
        public Result Delete(Guid id)
        {
            var command = new OrderCommand { Id = id, Mode = CommandMode.Delete, Group = Constants.VerificationGroup.Delete };

            return ServiceLocator.CommandBus.Send(command);
        }
    }
}
