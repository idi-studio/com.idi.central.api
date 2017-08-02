using System.Linq;
using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Core.Authentication;
using IDI.Core.Common;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Infrastructure.Verification.Attributes;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Administration.Queries
{
    public class QueryUserIdentityCondition : Condition
    {
        [RequiredField(Resources.Key.DisplayName.Username)]
        public string UserName { get; set; }
    }

    public class QueryUserIdentity : Query<QueryUserIdentityCondition, UserIdentity>
    {
        [Injection]
        public IQueryRepository<User> UserRepository { get; set; }

        [Injection]
        public IQueryRepository<UserRole> UserRoleRepository { get; set; }

        public override Result<UserIdentity> Execute(QueryUserIdentityCondition condition)
        {
            var user = this.UserRepository.Find(e => e.UserName == condition.UserName, e => e.Profile);

            if (user == null)
                return Result.Fail<UserIdentity>($"无效的用户'{condition.UserName}'");

            var userRoles = this.UserRoleRepository.Get(e => e.UserId == user.Id, e => e.User, e => e.Role);

            var identity = new UserIdentity
            {
                Name = user.UserName,
                NameIdentifier = user.Id.ToString(),
                Role = userRoles.Select(r => r.Role.Name).JoinToString(","),
                Gender = user.Profile.Gender
            };

            return Result.Success(identity);
        }
    }
}
