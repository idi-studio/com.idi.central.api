using System;
using System.Linq;
using IDI.Central.Domain.Modules.Retailing.AggregateRoots;
using IDI.Central.Models.Retailing;
using IDI.Core.Common;
using IDI.Core.Common.Extensions;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Retailing.Queries
{
    public class QueryOrderCondition : Condition
    {
        public Guid Id { get; set; }
    }

    public class QueryOrder : Query<QueryOrderCondition, OrderModel>
    {
        [Injection]
        public IQueryRepository<Order> Orders { get; set; }

        public override Result<OrderModel> Execute(QueryOrderCondition condition)
        {
            var order = this.Orders.Find(e => e.Id == condition.Id, e => e.Items);

            var model = new OrderModel
            {
                Id = order.Id,
                Category = order.Category,
                CustomerId = order.CustomerId,
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
                    ReadjustUnitPrice = e.ReadjustUnitPrice,
                    UnitPrice = e.UnitPrice
                }).ToList()
            };

            return Result.Success(model);
        }
    }
}
