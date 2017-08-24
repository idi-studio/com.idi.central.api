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
    public class QueryMyProfileCondition : Condition
    {
        [RequiredField(Resources.Key.DisplayName.Username)]
        public string UserName { get; set; }
    }

    public class QueryMyProfile : Query<QueryMyProfileCondition, MyProfile>
    {
        [Injection]
        public IQueryableRepository<User> Users { get; set; }

        public override Result<MyProfile> Execute(QueryMyProfileCondition condition)
        {
            var user = this.Users.Include(e => e.Profile).Find(u => u.UserName == condition.UserName);

            var profile = new MyProfile
            {
                Id = user.Id,
                UserName = user.UserName,
                Name = user.Profile.Name,
                Gender = user.Profile.Gender,
                Birthday = user.Profile.Birthday,
                Photo = user.Profile.Photo
            };

            return Result.Success(profile);
        }
    }
}
