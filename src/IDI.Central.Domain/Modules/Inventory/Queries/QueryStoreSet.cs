using System.Linq;
using IDI.Central.Domain.Modules.Inventory.AggregateRoots;
using IDI.Central.Models.Inventory;
using IDI.Core.Common;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Inventory.Queries
{
    public class QueryStoreSetCondition : Condition { }

    public class QueryStoreSet : Query<QueryStoreSetCondition, Set<StoreModel>>
    {
        [Injection]
        public IQueryableRepository<Store> Stores { get; set; }

        public override Result<Set<StoreModel>> Execute(QueryStoreSetCondition condition)
        {
            var stores = this.Stores.Get();

            var data = stores.Select(e => new StoreModel
            {
                Id = e.Id,
                Name = e.Name,
                Active = e.Active
            }).ToList();

            return Result.Success(new Set<StoreModel>(data));
        }
    }
}
