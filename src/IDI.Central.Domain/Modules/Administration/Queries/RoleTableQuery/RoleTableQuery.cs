using System.Linq;
using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Central.Models.Administration;
using IDI.Core.Common;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Administration.Queries
{
    public class RoleTableQuery : Query<RoleTableQueryCondition, RoleTable>
    {
        [Injection]
        public IQueryRepository<Role> RoleRepository { get; set; }


        public override Result<RoleTable> Execute(RoleTableQueryCondition condition)
        {
            var roles = this.RoleRepository.Get();

            var table = new RoleTable();
            table.Rows = roles.OrderBy(r => r.Name).Select(r => new RoleTableRow
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
