using System;
using System.Collections.Generic;
using System.Linq;
using IDI.Central.Common.Enums;
using IDI.Central.Domain.Modules.Sales.AggregateRoots;
using IDI.Central.Models.Sales;
using IDI.Core.Common;
using IDI.Core.Common.Extensions;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Localization;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Sales.Queries
{
    public class QueryOrderSetCondition : Condition
    {
        public OrderCategory Category { get; set; }

        public DateTime Deadline { get; set; }
    }

    public class QueryOrderSet : Query<QueryOrderSetCondition, Set<OrderModel>>
    {
        [Injection]
        public IQueryableRepository<Order> Orders { get; set; }

        public override Result<Set<OrderModel>> Execute(QueryOrderSetCondition condition)
        {
            var orders = this.Orders.Include(e => e.Customer).Get(e => e.Category == condition.Category && e.Date >= condition.Deadline);

            var collection = orders.OrderBy(e => e.Date).Select(e => new OrderModel
            {
                Id = e.Id,
                Category = e.Category,
                CustomerId = e.CustomerId.HasValue ? e.CustomerId.Value : Guid.Empty,
                CustomerName = e.Customer == null ? string.Empty : e.Customer.Name,
                Date = e.Date.AsLongDate(),
                SN = e.SN,
                Status = e.Status,
                StatusDesc = Localization.Get(e.Status),
                Remark = e.Remark,
                Items = new List<OrderItemModel>()
            }).ToList();

            return Result.Success(new Set<OrderModel>(collection));
        }
    }
}
