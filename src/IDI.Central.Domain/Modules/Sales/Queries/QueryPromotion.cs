using System;
using System.Linq;
using IDI.Central.Common.JsonTypes;
using IDI.Central.Domain.Modules.Sales.AggregateRoots;
using IDI.Central.Models.Sales;
using IDI.Core.Common;
using IDI.Core.Common.Extensions;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Sales.Queries
{
    public class QueryPromotionCondition : Condition
    {
        public Guid Id { get; set; }
    }

    public class QueryPromotion : Query<QueryPromotionCondition, PromotionModel>
    {
        [Injection]
        public IQueryableRepository<Promotion> Promotions { get; set; }

        public override Result<PromotionModel> Execute(QueryPromotionCondition condition)
        {
            var promotion = this.Promotions.Include(e => e.Product).Find(condition.Id);

            var model = new PromotionModel
            {
                Id = promotion.Id,
                ProductId = promotion.ProductId,
                ProductName = promotion.Product.Name,
                Subject = promotion.Subject,
                StartTime = promotion.StartTime.AsShortDate(),
                EndTime = promotion.EndTime.AsShortDate(),
                Enabled = promotion.Enabled,
                Price = promotion.Price.To<PromotionPrice>(),
            };

            return Result.Success(model);
        }
    }
}
