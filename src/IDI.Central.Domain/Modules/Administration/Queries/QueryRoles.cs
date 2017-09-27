using System.Linq;
using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Central.Models.Administration;
using IDI.Core.Common;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Administration.Queries
{
    public class QueryRolesCondition : Condition { }

    public class QueryRoles : Query<QueryRolesCondition, Set<RoleModel>>
    {
        [Injection]
        public IQueryableRepository<Role> Roles { get; set; }

        public override Result<Set<RoleModel>> Execute(QueryRolesCondition condition)
        {
            var roles = this.Roles.Get();

            var collection = roles.OrderBy(r => r.Name).Select(r => new RoleModel
            {
                Id = r.Id,
                Name = r.Name,
                Descrition = r.Descrition,
                IsActive = r.IsActive,
                Permissions = r.Permissions
            }).ToList();

            return Result.Success(new Set<RoleModel>(collection));
        }
    }
}
