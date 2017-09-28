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
    public class QueryUserRoleCondition : Condition
    {
        [RequiredField]
        public string UserName { get; set; }
    }

    public class QueryUserRole : Query<QueryUserRoleCondition, UserRoleModel>
    {
        [Injection]
        public IQueryableRepository<User> Users { get; set; }

        [Injection]
        public IQueryableRepository<Role> Roles { get; set; }

        public override Result<UserRoleModel> Execute(QueryUserRoleCondition condition)
        {
            var user = Users.Include(e => e.Role).Find(e => e.UserName == condition.UserName);
            var roles = Roles.Get();
            var current = user.Role.Roles.To<List<string>>();


            var data = new UserRoleModel
            {
                UserName = user.UserName,
                Roles = roles.Select(e => new RoleSelectionModel { Name = e.Name, Checked = current.Contains(e.Name) }).ToList()
            };

            return Result.Success(data);
        }
    }
}
