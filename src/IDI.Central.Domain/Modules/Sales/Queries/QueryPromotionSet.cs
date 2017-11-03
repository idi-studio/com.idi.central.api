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
    public class QueryPromotionSetCondition : Condition { }

    public class QueryPromotionSet : Query<QueryPromotionSetCondition, Set<PromotionModel>>
    {
        [Injection]
        public IQueryableRepository<Promotion> Promotions { get; set; }

        public override Result<Set<PromotionModel>> Execute(QueryPromotionSetCondition condition)
        {
            var promotions = this.Promotions.Include(e => e.Product).Get();

            var collection = promotions.OrderBy(e => e.StartTime).Select(e => new PromotionModel
            {
                Id = e.Id,
                ProductId = e.ProductId,
                Subject = e.Subject,
                ProductName = e.Product.Name,
                StartTime = e.StartTime,//.AsShortDate(),
                EndTime = e.EndTime,//.AsShortDate(),
                Enabled = e.Enabled,
                Price = e.Price.To<PromotionPrice>(),
            }).ToList();

            return Result.Success(new Set<PromotionModel>(collection));
        }
    }
}
