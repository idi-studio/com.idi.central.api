using System.Linq;
using IDI.Central.Domain.Modules.BasicInfo.AggregateRoots;
using IDI.Central.Models.Inventory;
using IDI.Core.Common;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Inventory.Queries
{
    public class QueryStockOptionSetCondition : Condition { }

    public class QueryStockOptionSet : Query<QueryStockOptionSetCondition, Set<StockOptionModel>>
    {
        [Injection]
        public IQueryableRepository<Product> Products { get; set; }

        public override Result<Set<StockOptionModel>> Execute(QueryStockOptionSetCondition condition)
        {
            var products = this.Products.Get();

            var collection = products.OrderBy(product => product.Name).Select(product => new StockOptionModel
            {
                Id = product.Id,
                Name = product.Name
            }).ToList();

            return Result.Success(new Set<StockOptionModel>(collection));
        }
    }
}
