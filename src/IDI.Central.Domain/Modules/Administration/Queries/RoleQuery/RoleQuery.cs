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
    public class RoleQuery : Query<RoleQueryCondition, Table<RoleRow>>
    {
        [Injection]
        public IQueryRepository<Role> Roles { get; set; }


        public override Result<Table<RoleRow>> Execute(RoleQueryCondition condition)
        {
            var roles = this.Roles.Get();

            var table = new Table<RoleRow>();

            table.Rows = roles.OrderBy(r => r.Name).Select(r => new RoleRow
            {
                Id = r.Id,
                Name = r.Name,
                Descrition = r.Descrition,
                IsActive = r.IsActive
            }).ToList();

            return Result.Success(table);
        }
    }
}
