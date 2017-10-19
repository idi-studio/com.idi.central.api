using System;
using System.Collections.Generic;
using System.Linq;
using IDI.Central.Domain.Modules.BasicInfo.AggregateRoots;
using IDI.Central.Domain.Modules.Inventory.AggregateRoots;
using IDI.Central.Models.BasicInfo;
using IDI.Core.Common;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Infrastructure.Verification.Attributes;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.BasicInfo.Queries
{
    public class QueryOptionSetCondition : Condition
    {
        [RequiredField]
        public string Category { get; set; }
    }

    public class QueryOptionSet : Query<QueryOptionSetCondition, Set<OptionModel>>
    {
        private readonly Dictionary<string, Func<List<OptionModel>>> source;

        [Injection]
        public IQueryableRepository<Product> Products { get; set; }

        [Injection]
        public IQueryableRepository<Store> Stores { get; set; }

        public QueryOptionSet()
        {
            source = new Dictionary<string, Func<List<OptionModel>>>
            {
                {"product",ProductOptions },{"store",StoreOptions }
            };
        }

        public override Result<Set<OptionModel>> Execute(QueryOptionSetCondition condition)
        {
            if (!source.ContainsKey(condition.Category))
                return Result.Success(new Set<OptionModel>());

            var options = source[condition.Category]().OrderBy(e => e.Name);

            return Result.Success(new Set<OptionModel>(options));
        }

        private List<OptionModel> ProductOptions()
        {
            return this.Products.Get().Select(e => new OptionModel { Id = e.Id, Name = e.Name }).ToList();
        }

        private List<OptionModel> StoreOptions()
        {
            return this.Stores.Get().Select(e => new OptionModel { Id = e.Id, Name = e.Name }).ToList();
        }
    }
}
