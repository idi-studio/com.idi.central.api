using System;
using System.Linq;
using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.Inventory.AggregateRoots;
using IDI.Central.Models.Inventory;
using IDI.Core.Common;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Inventory.Queries
{
    public class QueryStoreCondition : Condition
    {
        public Guid StoreId { get; set; }
    }

    public class QueryStore : Query<QueryStoreCondition, StoreModel>
    {
        [Injection]
        public IQueryableRepository<Store> Stores { get; set; }

        public override Result<StoreModel> Execute(QueryStoreCondition condition)
        {
            var store = this.Stores.Include(e => e.Stocks).AlsoInclude(e => e.Product).Find(e => e.Id == condition.StoreId);

            if (store == null)
                return Result.Fail<StoreModel>(Resources.Key.Command.RecordNotExisting);

            var model = new StoreModel
            {
                Id = store.Id,
                Name = store.Name,
                Inactive = store.Inactive,
                Stocks = store.Stocks.Select(e => new StockModel
                {
                    Id = e.Id,
                    ProductId = e.ProductId,
                    ProductName = e.Product.Name,
                    StoreId = e.StoreId,
                    StoreName = store.Name,
                    BinCode = e.BinCode,
                    Available = e.Available,
                    Frozen = e.Frozen,
                    Quantity = e.Quantity
                }).ToList()
            };

            return Result.Success(model);
        }
    }
}
