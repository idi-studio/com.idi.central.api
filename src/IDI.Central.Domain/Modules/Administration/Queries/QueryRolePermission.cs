using System.Collections.Generic;
using System.Linq;
using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Central.Models.Administration;
using IDI.Core.Common;
using IDI.Core.Common.Extensions;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Infrastructure.Verification.Attributes;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Administration.Queries
{
    public class QueryRolePermissionCondition : Condition
    {
        [RequiredField]
        public string Name { get; set; }
    }

    public class QueryRolePermission : Query<QueryRolePermissionCondition, RolePermissionModel>
    {
        [Injection]
        public IQueryableRepository<Role> Roles { get; set; }

        [Injection]
        public IQueryableRepository<Permission> Permissions { get; set; }

        public override Result<RolePermissionModel> Execute(QueryRolePermissionCondition condition)
        {
            var permissions = Permissions.Get(e => !e.Everyone);
            var role = Roles.Find(e => e.Name == condition.Name);
            var current = role.Permissions.To<Dictionary<string, List<string>>>();

            var data = new RolePermissionModel
            {
                Role = role.Name,
                Modules = permissions.GroupBy(e => e.Module).Select(g => new ModulePermissionModel
                {
                    Name = g.Key,
                    Permissions = g.Select(e => new PermissionModel
                    {
                        Module = g.Key,
                        Code = e.Code,
                        Name = e.Name,
                        Type = e.Type,
                        Checked = current.Contains(g.Key, e.Code)
                    }).ToList()
                }).ToList()
            };

            return Result.Success(data);
        }
    }
}
