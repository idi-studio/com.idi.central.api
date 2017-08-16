using System;
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
    public class QueryProductPriceCondition : Condition
    {
        public Guid Id { get; set; }
    }

    public class QueryProductPrice : Query<QueryProductPriceCondition, ProductPriceModel>
    {
        [Injection]
        public ILocalization Localization { get; set; }

        [Injection]
        public IQueryRepository<ProductPrice> Prices { get; set; }

        public override Result<ProductPriceModel> Execute(QueryProductPriceCondition condition)
        {
            var price = this.Prices.Find(condition.Id);

            var model = new ProductPriceModel
            {
                Id = price.Id,
                Amount = price.Amount,
                Category = price.Category,
                CategoryName = Localization.Get(Resources.Prefix.PRICE_CATEGORY, price.Category.ToString()),
                DueDate = price.DueDate.AsLongDate(),
                Grade = price.Grade,
                ProductId = price.ProductId,
                StartDate = price.StartDate.AsLongDate(),
                Enabled = price.Enabled
            };

            return Result.Success(model);
        }
    }
}
