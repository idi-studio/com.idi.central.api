using System.Collections.Generic;
using System.Linq;
using IDI.Central.Domain.Common;
using IDI.Central.Domain.Modules.Retailing.AggregateRoots;
using IDI.Central.Models.Retailing;
using IDI.Core.Common;
using IDI.Core.Common.Basetypes;
using IDI.Core.Common.Extensions;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Retailing.Queries
{
    public class QueryProductsCondition : Condition { }

    public class QueryProducts : Query<QueryProductsCondition, Collection<ProductModel>>
    {
        [Injection]
        public IQueryRepository<Product> Products { get; set; }

        public override Result<Collection<ProductModel>> Execute(QueryProductsCondition condition)
        {
            var products = this.Products.Get();

            var collection = products.OrderBy(product => product.Name).Select(product => new ProductModel
            {
                Id = product.Id,
                Name = product.Name,
                QRCode = product.QRCode,
                Description = product.Tags.To<List<Tag>>().AsString(),
                Tags = product.Tags.To<List<Tag>>(),
                Enabled = product.Enabled,
                OnShelf = product.OnShelf
            }).ToList();

            return Result.Success(new Collection<ProductModel>(collection));
        }
    }
}
