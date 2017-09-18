using System.Linq;
using IDI.Central.Domain.Modules.Sales.AggregateRoots;
using IDI.Central.Models.Sales;
using IDI.Core.Common;
using IDI.Core.Common.Extensions;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Sales.Queries
{
    public class QueryCustomerSetCondition : Condition { }

    public class QueryCustomerSet : Query<QueryCustomerSetCondition, Set<CustomerModel>>
    {
        [Injection]
        public IQueryableRepository<Customer> Customers { get; set; }

        public override Result<Set<CustomerModel>> Execute(QueryCustomerSetCondition condition)
        {
            var Customers = this.Customers.Include(e => e.User).AlsoInclude(e => e.Profile).Get();

            var collection = Customers.OrderBy(e => e.Name).Select(e => new CustomerModel
            {
                Id = e.Id,
                Name = e.Name,
                Grade = e.Grade,
                PhoneNum = e.User.Profile.PhoneNum,
                Verified = e.User.Profile.PhoneVerified,
                Gender = e.User.Profile.Gender,
                Date = e.CreatedAt.AsShortDate(),
            }).ToList();

            return Result.Success(new Set<CustomerModel>(collection));
        }
    }
}
