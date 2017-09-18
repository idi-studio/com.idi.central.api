using System;
using System.Collections.Generic;
using System.Linq;
using IDI.Central.Domain.Common;
using IDI.Central.Domain.Modules.Material.AggregateRoots;
using IDI.Central.Domain.Modules.Retailing.AggregateRoots;
using IDI.Central.Models.Common;
using IDI.Central.Models.Material;
using IDI.Core.Common;
using IDI.Core.Common.Extensions;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Material.Queries
{
    public class QueryProductSellSetCondition : Condition
    {
        public Guid CustomerId { get; set; }
    }

    public class QueryProductSellSet : Query<QueryProductSellSetCondition, Set<ProductSellModel>>
    {
        [Injection]
        public IQueryableRepository<Customer> Customers { get; set; }

        [Injection]
        public IQueryableRepository<Product> Products { get; set; }

        public override Result<Set<ProductSellModel>> Execute(QueryProductSellSetCondition condition)
        {
            var customer = this.Customers.Find(condition.CustomerId);

            var grade = customer == null ? 0 : customer.Grade;

            var products = this.Products.Include(e => e.Prices).Get(e => e.OnShelf && e.Enabled);

            var collection = products.OrderBy(product => product.Name).Select(product => new ProductSellModel
            {
                Id = product.Id,
                Name = product.Name,
                QRCode = product.QRCode,
                Description = product.Tags.To<List<TagModel>>().AsString(),
                Tags = product.Tags.To<List<TagModel>>(),
                Price = product.FavorablePrice(grade)
            }).Where(e => e.Price != null).ToList();

            return Result.Success(new Set<ProductSellModel>(collection));
        }
    }
}
