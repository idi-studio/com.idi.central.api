using System.Linq;
using IDI.Central.Domain.Modules.Retailing.AggregateRoots;
using IDI.Central.Models.Retailing;
using IDI.Core.Common;
using IDI.Core.Common.Basetypes;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Retailing.Queries
{
    public class QueryProductPricesCondition : Condition { }

    public class QueryProductPrices : Query<QueryProductPricesCondition, Collection<ProductPriceModel>>
    {
        [Injection]
        public IQueryRepository<ProductPrice> Prices { get; set; }

        public override Result<Collection<ProductPriceModel>> Execute(QueryProductPricesCondition condition)
        {
            var prices = this.Prices.Get();

            var collection = prices.OrderBy(e => e.Category).Select(price => new ProductPriceModel
            {
                Id = price.Id,
                Amount = price.Amount,
                Category = price.Category,
                DueDate = price.DueDate,
                Grade = price.Grade,
                ProductId = price.ProductId,
                StartDate = price.StartDate,
                Enabled = price.Enabled
            }).ToList();

            return Result.Success(new Collection<ProductPriceModel>(collection));
        }
    }
}
