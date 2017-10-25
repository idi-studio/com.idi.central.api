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
    public class QueryRoleMenuCondition : Condition
    {
        [RequiredField]
        public string Name { get; set; }
    }

    public class QueryRoleMenu : Query<QueryRoleMenuCondition, RoleMenuModel>
    {
        [Injection]
        public IQueryableRepository<Role> Roles { get; set; }

        [Injection]
        public IQueryableRepository<Module> Modules { get; set; }

        public override Result<RoleMenuModel> Execute(QueryRoleMenuCondition condition)
        {
            var modules = Modules.Include(e => e.Menus).Get();

            var role = Roles.Find(e => e.Name == condition.Name);

            var current = role.Menus.To<List<int>>();

            var data = new RoleMenuModel
            {
                Role = role.Name,
                Menus = modules.Select(e => new MenuItem
                {
                    SN = e.SN,
                    Name = e.Name,
                    Checked = current.Contains(e.SN),
                    Sub = e.Menus.Select(o => new MenuItem { SN = o.SN, Name = o.Name, Checked = current.Contains(o.SN) }).ToList()
                }).ToList()
            };

            return Result.Success(data);
        }
    }
}
