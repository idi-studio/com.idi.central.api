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
    public class QueryUsersCondition : Condition { }

    public class QueryUsers : Query<QueryUsersCondition, Collection<UserModel>>
    {
        [Injection]
        public IQueryRepository<User> Users { get; set; }

        public override Result<Collection<UserModel>> Execute(QueryUsersCondition condition)
        {
            var users = this.Users.Get(u => u.Profile);

            var collection = users.OrderBy(r => r.UserName).Select(r => new UserModel
            {
                Id = r.Id,
                UserName = r.UserName,
                IsActive = r.IsActive,
                Name = r.Profile.Name,
                Gender = r.Profile.Gender,
                Birthday = r.Profile.Birthday,
                Photo = r.Profile.Photo
            }).ToList();

            return Result.Success(new Collection<UserModel>(collection));
        }
    }
}
