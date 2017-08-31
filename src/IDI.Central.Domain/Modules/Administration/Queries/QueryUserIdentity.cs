using System.Linq;
using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Central.Models.Administration;
using IDI.Core.Common;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Infrastructure.Verification.Attributes;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Administration.Queries
{
    public class QueryUserIdentityCondition : Condition
    {
        [RequiredField]
        public string UserName { get; set; }
    }

    public class QueryUserIdentity : Query<QueryUserIdentityCondition, UserIdentity>
    {
        [Injection]
        public IQueryableRepository<User> UserRepository { get; set; }

        [Injection]
        public IQueryableRepository<UserRole> UserRoleRepository { get; set; }

        public override Result<UserIdentity> Execute(QueryUserIdentityCondition condition)
        {
            var user = this.UserRepository.Include(e => e.Profile).Find(e => e.UserName == condition.UserName);

            if (user == null)
                return Result.Fail<UserIdentity>($"无效的用户'{condition.UserName}'");

            var userRoles = this.UserRoleRepository.Include(e => e.User).Include(e => e.Role).Get(e => e.UserId == user.Id);

            var identity = new UserIdentity
            {
                Name = user.UserName,
                NameIdentifier = user.Id.ToString(),
                Role = userRoles.Select(r => r.Role.Name).JoinToString(","),
                Gender = user.Profile.Gender.ToString()
            };

            return Result.Success(identity);
        }
    }
}
