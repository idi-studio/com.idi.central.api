using System;
using System.Linq;
using IDI.Central.Domain.Modules.BasicInfo.AggregateRoots;
using IDI.Central.Models.Inventory;
using IDI.Core.Common;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Inventory.Queries
{
    public class QueryStockSetCondition : Condition
    {
        public Guid ProductId { get; set; }
    }

    public class QueryStockSet : Query<QueryStockSetCondition, Set<StockModel>>
    {
        [Injection]
        public IQueryableRepository<Product> Products { get; set; }

        public override Result<Set<StockModel>> Execute(QueryStockSetCondition condition)
        {
            var product = this.Products.Include(e => e.Stocks).AlsoInclude(e => e.Store).Find(e => e.Id == condition.ProductId);

            if (product == null)
                return Result.Success(new Set<StockModel>());

            var data = product.Stocks.Select(e => new StockModel
            {
                Id = e.Id,
                ProductId = e.ProductId,
                ProductName = product.Name,
                StoreId = e.StoreId,
                StoreName = e.Store.Name,
                BinCode = e.BinCode,
                Available = e.Available,
                Reserve = e.Reserve,
                Quantity = e.Quantity
            }).ToList();

            return Result.Success(new Set<StockModel>(data));
        }
    }
}
