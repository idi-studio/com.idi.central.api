using System.Linq;
using IDI.Central.Domain.Modules.Retailing.AggregateRoots;
using IDI.Central.Models.Retailing;
using IDI.Core.Common;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Retailing.Queries
{
    public class QueryCustomerSetCondition : Condition { }

    public class QueryCustomerSet : Query<QueryCustomerSetCondition, Set<CustomerModel>>
    {
        [Injection]
        public IQueryableRepository<Customer> Customers { get; set; }

        public override Result<Set<CustomerModel>> Execute(QueryCustomerSetCondition condition)
        {
            var Customers = this.Customers.Get();

            var collection = Customers.OrderBy(e => e.Name).Select(e => new CustomerModel
            {
                Id = e.Id,
                Name = e.Name,
                Grade = e.Grade,
                Phone = e.Phone
            }).ToList();

            return Result.Success(new Set<CustomerModel>(collection));
        }
    }
}
