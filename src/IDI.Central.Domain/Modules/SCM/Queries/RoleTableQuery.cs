using System.Linq;
using IDI.Central.Domain.Modules.SCM.AggregateRoots;
using IDI.Central.Domain.Modules.SCM.Conditions;
using IDI.Central.Models.SCM;
using IDI.Core.Common;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.SCM.Queries
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
