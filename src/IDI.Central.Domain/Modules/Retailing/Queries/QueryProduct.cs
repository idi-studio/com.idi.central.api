using System.Collections.Generic;
using System.Linq;
using IDI.Central.Domain.Common;
using IDI.Central.Domain.Modules.Retailing.AggregateRoots;
using IDI.Central.Models.Common;
using IDI.Central.Models.Retailing;
using IDI.Core.Common;
using IDI.Core.Common.Extensions;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Retailing.Queries
{
    public class QueryProductCondition : Condition { }

    public class QueryProduct : Query<QueryProductCondition, Table<ProductRow>>
    {
        [Injection]
        public IQueryRepository<Product> Products { get; set; }

        public override Result<Table<ProductRow>> Execute(QueryProductCondition condition)
        {
            var data = this.Products.Get();

            var table = new Table<ProductRow>();

            table.Rows = data.OrderBy(r => r.Name).Select(r => new ProductRow
            {
                Id = r.Id,
                Name = r.Name,
                Code = r.Code,
                Description = r.Tags.To<List<Tag>>().AsString(),
                Tags = r.Tags.To<List<Tag>>(),
                Enabled = r.Enabled
            }).ToList();

            return Result.Success(table);
        }
    }
}
