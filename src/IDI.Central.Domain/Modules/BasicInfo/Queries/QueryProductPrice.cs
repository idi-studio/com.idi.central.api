using System;
using IDI.Central.Domain.Modules.BasicInfo.AggregateRoots;
using IDI.Central.Models.BasicInfo;
using IDI.Core.Common;
using IDI.Core.Common.Extensions;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.BasicInfo.Queries
{
    public class QueryProductPriceCondition : Condition
    {
        public Guid Id { get; set; }
    }

    public class QueryProductPrice : Query<QueryProductPriceCondition, ProductPriceModel>
    {
        [Injection]
        public IQueryableRepository<ProductPrice> Prices { get; set; }

        public override Result<ProductPriceModel> Execute(QueryProductPriceCondition condition)
        {
            var price = this.Prices.Find(condition.Id);

            var model = new ProductPriceModel
            {
                Id = price.Id,
                Amount = price.Amount,
                Category = price.Category,
                CategoryName = Localization.Get(price.Category),
                PeriodEnd = price.PeriodEnd.AsLongDate(),
                PeriodStart = price.PeriodStart.AsLongDate(),
                GradeFrom = price.GradeFrom,
                GradeTo = price.GradeTo,
                ProductId = price.ProductId,
                Enabled = price.Enabled
            };

            return Result.Success(model);
        }
    }
}
