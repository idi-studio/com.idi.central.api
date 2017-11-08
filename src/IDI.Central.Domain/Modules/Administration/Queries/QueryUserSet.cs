using System.Linq;
using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Central.Models.Administration;
using IDI.Core.Common;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Repositories;
using IDI.Core.Common.Extensions;

namespace IDI.Central.Domain.Modules.Administration.Queries
{
    public class QueryUserSetCondition : Condition { }

    public class QueryUserSet : Query<QueryUserSetCondition, Set<UserModel>>
    {
        [Injection]
        public IQueryableRepository<User> Users { get; set; }

        public override Result<Set<UserModel>> Execute(QueryUserSetCondition condition)
        {
            var users = this.Users.Include(e => e.Profile).Get();

            var collection = users.OrderBy(r => r.UserName).Select(r => new UserModel
            {
                Id = r.Id,
                UserName = r.UserName,
                IsActive = r.Active,
                Name = r.Profile.Name,
                Gender = r.Profile.Gender,
                Birthday = r.Profile.Birthday.AsShortDate(),
                Photo = r.Profile.Photo,
                IsLocked = r.IsLocked,
                LockTime = r.LockTime.AsLongDate(),
                LatestLoginTime = r.LatestLoginTime.AsLongDate()
            }).OrderBy(e => e.UserName).ToList();

            return Result.Success(new Set<UserModel>(collection));
        }
    }
}
