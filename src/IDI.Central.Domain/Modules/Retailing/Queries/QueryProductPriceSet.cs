using System;
using System.Linq;
using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.Retailing.AggregateRoots;
using IDI.Central.Models.Retailing;
using IDI.Core.Common;
using IDI.Core.Common.Extensions;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Localization;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Retailing.Queries
{
    public class QueryProductPriceSetCondition : Condition
    {
        public Guid ProductId { get; set; }
    }

    public class QueryProductPriceSet : Query<QueryProductPriceSetCondition, Set<ProductPriceModel>>
    {
        [Injection]
        public ILocalization Localization { get; set; }

        [Injection]
        public IQueryRepository<ProductPrice> Prices { get; set; }

        public override Result<Set<ProductPriceModel>> Execute(QueryProductPriceSetCondition condition)
        {
            var prices = this.Prices.Get(e => e.ProductId == condition.ProductId);

            var collection = prices.OrderByDescending(e => e.Category).ThenBy(e => e.Grade).Select(price => new ProductPriceModel
            {
                Id = price.Id,
                Amount = price.Amount,
                Category = price.Category,
                CategoryName = Localization.Get(price.Category),
                DueDate = price.DueDate.AsLongDate(),
                Grade = price.Grade,
                ProductId = price.ProductId,
                StartDate = price.StartDate.AsLongDate(),
                Enabled = price.Enabled
            }).ToList();

            return Result.Success(new Set<ProductPriceModel>(collection));
        }
    }
}
