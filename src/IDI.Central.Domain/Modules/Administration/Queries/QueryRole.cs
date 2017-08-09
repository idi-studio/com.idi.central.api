using System.Linq;
using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Central.Models.Administration;
using IDI.Core.Common;
using IDI.Core.Common.Basetypes;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Administration.Queries
{
    public class QueryRoleCondition : Condition { }

    public class QueryRole : Query<QueryRoleCondition, Collection<RoleModel>>
    {
        [Injection]
        public IQueryRepository<Role> Roles { get; set; }

        public override Result<Collection<RoleModel>> Execute(QueryRoleCondition condition)
        {
            var roles = this.Roles.Get();

            var collection = roles.OrderBy(r => r.Name).Select(r => new RoleModel
            {
                Id = r.Id,
                Name = r.Name,
                Descrition = r.Descrition,
                IsActive = r.IsActive
            }).ToList();

            return Result.Success(new Collection<RoleModel>(collection));
        }
    }
}
