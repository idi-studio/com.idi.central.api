using System;
using IDI.Central.Domain.Modules.Retailing.AggregateRoots;
using IDI.Central.Models.Retailing;
using IDI.Core.Common;
using IDI.Core.Common.Extensions;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Retailing.Queries
{
    public class QueryCustomerCondition : Condition
    {
        public Guid Id { get; set; }
    }

    public class QueryCustomer : Query<QueryCustomerCondition, CustomerModel>
    {
        [Injection]
        public IQueryableRepository<Customer> Customers { get; set; }

        public override Result<CustomerModel> Execute(QueryCustomerCondition condition)
        {
            var customer = this.Customers.Include(e => e.User).AlsoInclude(e => e.Profile).Include(e => e.Shippings).Find(condition.Id);

            var model = new CustomerModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Grade = customer.Grade,
                Date = customer.CreatedAt.AsShortDate(),
                PhoneNum = customer.User.Profile.PhoneNum,
                Gender = customer.User.Profile.Gender
            };

            return Result.Success(model);
        }
    }
}
