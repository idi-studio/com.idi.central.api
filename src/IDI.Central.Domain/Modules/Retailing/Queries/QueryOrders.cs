using System;
using System.Collections.Generic;
using System.Linq;
using IDI.Central.Common.Enums;
using IDI.Central.Domain.Modules.Retailing.AggregateRoots;
using IDI.Central.Models.Retailing;
using IDI.Core.Common;
using IDI.Core.Common.Extensions;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Retailing.Queries
{
    public class QueryOrdersCondition : Condition
    {
        public OrderCategory Category { get; set; }

        public DateTime Deadline { get; set; }
    }

    public class QueryOrders : Query<QueryOrdersCondition, Set<OrderModel>>
    {
        [Injection]
        public IQueryRepository<Order> Orders { get; set; }

        public override Result<Set<OrderModel>> Execute(QueryOrdersCondition condition)
        {
            var orders = this.Orders.Get(e => e.Category == condition.Category && e.Date <= condition.Deadline);

            var collection = orders.OrderBy(e => e.Date).Select(e => new OrderModel
            {
                Id = e.Id,
                Category = e.Category,
                CustomerId = e.CustomerId,
                Date = e.Date.AsLongDate(),
                SN = e.SN,
                Status = e.Status,
                StatusDesc = e.Status.ToString(),
                Remark = e.Remark,
                Items = new List<OrderItemModel>()
            }).ToList();

            return Result.Success(new Set<OrderModel>(collection));
        }
    }
}
