using System.Linq;
using IDI.Central.Common;
using IDI.Central.Domain.Modules.Administration;
using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Central.Models.Dashboard;
using IDI.Core.Common;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Dashboard.Queries
{
    public class QueryUserScaleCondition : Condition { }

    public class QueryUserScale : Query<QueryUserScaleCondition, UserScaleModel>
    {
        [Injection]
        public IQueryableRepository<User> Users { get; set; }

        public override Result<UserScaleModel> Execute(QueryUserScaleCondition condition)
        {
            var users = Users.Include(e => e.Profile).Include(e => e.Role).Get();
            var customers = users.Where(e => e.Roles().Contains(Configuration.Roles.Customers));
            var staffs = users.Where(e => e.Roles().Contains(Configuration.Roles.Staffs));

            var model = new UserScaleModel
            {
                CustomerTotal = customers.Count(),
                FemaleCustomerTotal = customers.Count(e => e.Profile.Gender == Gender.Female),
                MaleCustomerTotal = customers.Count(e => e.Profile.Gender == Gender.Male),
                StaffTotal = staffs.Count(),
                FemaleStaffTotal = staffs.Count(e => e.Profile.Gender == Gender.Female),
                MaleStaffTotal = staffs.Count(e => e.Profile.Gender == Gender.Male),
            };

            return Result.Success(model);
        }
    }
}
