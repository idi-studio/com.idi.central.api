using System;
using System.Collections.Generic;
using System.Linq;
using IDI.Central.Domain.Common;
using IDI.Central.Domain.Modules.BasicInfo;
using IDI.Central.Domain.Modules.BasicInfo.AggregateRoots;
using IDI.Central.Domain.Modules.Sales.AggregateRoots;
using IDI.Central.Models.BasicInfo;
using IDI.Central.Models.Sales;
using IDI.Core.Common;
using IDI.Core.Common.Extensions;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Sales.Queries
{
    public class QuerySellSetCondition : Condition
    {
        public Guid CustomerId { get; set; }
    }

    public class QueryProductSellSet : Query<QuerySellSetCondition, Set<SellModel>>
    {
        [Injection]
        public IQueryableRepository<Customer> Customers { get; set; }

        [Injection]
        public IQueryableRepository<Product> Products { get; set; }

        public override Result<Set<SellModel>> Execute(QuerySellSetCondition condition)
        {
            var customer = this.Customers.Find(condition.CustomerId);

            var grade = customer == null ? 0 : customer.Grade;

            var products = this.Products.Include(e => e.Prices).Get(e => e.OnShelf && e.Enabled);

            var collection = products.OrderBy(product => product.Name).Select(product => new SellModel
            {
                Id = product.Id,
                Name = product.Name,
                QRCode = product.QRCode,
                Description = product.Tags.To<List<TagModel>>().AsString(),
                Tags = product.Tags.To<List<TagModel>>(),
                Price = product.FavorablePrice(grade)
            }).Where(e => e.Price != null).ToList();

            return Result.Success(new Set<SellModel>(collection));
        }
    }
}
