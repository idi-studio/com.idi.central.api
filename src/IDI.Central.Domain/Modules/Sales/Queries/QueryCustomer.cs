using System;
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

            model.Shippings = customer.Shippings.Select(e => new ShippingAddressModel
            {
                Id = e.Id,
                CustomerId = e.CustomerId,
                Receiver = e.Receiver,
                ContactNo = e.ContactNo,
                Province = e.Province,
                City = e.City,
                Area = e.Area,
                Street = e.Street,
                Detail = e.Detail,
                Postcode = e.Postcode,
                Default = e.Id == customer.DefaultShippingId
            }).ToList();

            return Result.Success(model);
        }
    }
}
