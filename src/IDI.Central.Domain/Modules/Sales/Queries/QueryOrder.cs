using System;
using System.Linq;
using IDI.Central.Domain.Modules.Sales.AggregateRoots;
using IDI.Central.Models.Sales;
using IDI.Core.Common;
using IDI.Core.Common.Extensions;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IDI.Central.Domain.Modules.Sales.Queries
{
    public class QueryOrderCondition : Condition
    {
        public Guid Id { get; set; }
    }

    public class QueryOrder : Query<QueryOrderCondition, OrderModel>
    {
        [Injection]
        public IQueryableRepository<Order> Orders { get; set; }

        public override Result<OrderModel> Execute(QueryOrderCondition condition)
        {
            var order = this.Orders.Include(e => e.Items).AlsoInclude(a => a.Product).Find(condition.Id);

            var model = new OrderModel
            {
                Id = order.Id,
                Category = order.Category,
                CustomerId = order.CustomerId.HasValue ? order.CustomerId.Value : Guid.Empty,
                Date = order.Date.AsLongDate(),
                SN = order.SN,
                Status = order.Status,
                StatusDesc = order.Status.ToString(),
                Remark = order.Remark,
                Items = order.Items.Select(e => new OrderItemModel
                {
                    Id = e.Id,
                    OrderId = e.OrderId,
                    ProductId = e.ProductId,
                    ProductName = e.Product != null ? e.Product.Name : "N/A",
                    Quantity = e.Quantity,
                    UnitPrice = e.UnitPrice
                }).ToList()
            };

            return Result.Success(model);
        }
    }
}
