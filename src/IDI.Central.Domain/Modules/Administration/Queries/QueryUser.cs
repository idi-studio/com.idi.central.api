using System.Linq;
using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Central.Models.Administration;
using IDI.Central.Models.Common;
using IDI.Core.Common;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Administration.Queries
{
    public class QueryUserCondition : Condition { }

    public class QueryUser : Query<QueryUserCondition, Table<UserRow>>
    {
        [Injection]
        public IQueryRepository<User> Users { get; set; }


        public override Result<Table<UserRow>> Execute(QueryUserCondition condition)
        {
            var roles = this.Users.Get();

            var table = new Table<UserRow>();

            table.Rows = roles.OrderBy(r => r.UserName).Select(r => new UserRow
            {
                Id = r.Id,
                UserName = r.UserName,
                IsActive = r.IsActive
            }).ToList();

            return Result.Success(table);
        }
    }
}
