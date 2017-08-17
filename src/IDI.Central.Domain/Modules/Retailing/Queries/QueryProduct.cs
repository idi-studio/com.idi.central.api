using System;
using System.Collections.Generic;
using IDI.Central.Domain.Common;
using IDI.Central.Domain.Modules.Retailing.AggregateRoots;
using IDI.Central.Models.Retailing;
using IDI.Core.Common;
using IDI.Core.Common.Extensions;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Retailing.Queries
{
    public class QueryProductCondition : Condition
    {
        public Guid Id { get; set; }
    }

    public class QueryProduct : Query<QueryProductCondition, ProductModel>
    {
        [Injection]
        public IQueryRepository<Product> Products { get; set; }

        public override Result<ProductModel> Execute(QueryProductCondition condition)
        {
            var product = this.Products.Find(condition.Id);

            var model = new ProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Code = product.Code,
                Description = product.Tags.To<List<Tag>>().AsString(),
                Tags = product.Tags.To<List<Tag>>(),
                Enabled = product.Enabled,
                OnShelf = product.OnShelf
            };

            return Result.Success(model);
        }
    }
}
