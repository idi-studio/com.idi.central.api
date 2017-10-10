using System.Collections.Generic;
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
    public class QueryMyProfileCondition : Condition
    {
        [RequiredField]
        public string UserName { get; set; }
    }

    public class QueryMyProfile : Query<QueryMyProfileCondition, MyProfile>
    {
        [Injection]
        public IQueryableRepository<User> Users { get; set; }

        public override Result<MyProfile> Execute(QueryMyProfileCondition condition)
        {
            var user = this.Users.Include(e => e.Profile).Include(e => e.Role).Find(u => u.UserName == condition.UserName);

            var profile = new MyProfile
            {
                Id = user.Id,
                UserName = user.UserName,
                Name = user.Profile.Name,
                Gender = user.Profile.Gender,
                Birthday = user.Profile.Birthday,
                Photo = user.Profile.Photo,
                Roles = user.Role.Roles.To<List<string>>()
            };

            return Result.Success(profile);
        }
    }
}
