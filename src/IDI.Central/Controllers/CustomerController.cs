using System;
using IDI.Central.Core;
using IDI.Central.Domain.Modules.Sales.Commands;
using IDI.Central.Domain.Modules.Sales.Queries;
using IDI.Central.Models.Sales;
using IDI.Central.Models.Sales.Inputs;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/cust"), ApplicationAuthorize]
    public class CustomerController : Controller
    {
        [HttpGet("{id}")]
        public Result<CustomerModel> Get(Guid id)
        {
            var condition = new QueryCustomerCondition {  Id = id };

            return ServiceLocator.QueryProcessor.Execute<QueryCustomerCondition, CustomerModel>(condition);
        }

        [HttpGet("list")]
        public Result<Set<CustomerModel>> List()
        {
            return ServiceLocator.QueryProcessor.Execute<QueryCustomerSetCondition, Set<CustomerModel>>();
        }

        [HttpPost]
        public Result Post([FromBody]CustomerInput input)
        {
            var command = new CustomerCommand
            {
                Id = input.Id,
                Name = input.Name,
                Gender = input.Gender,
                Grade = input.Grade,
                PhoneNum = input.PhoneNum,
                Mode = CommandMode.Create,
                Group = VerificationGroup.Create,
            };

            return ServiceLocator.CommandBus.Send(command);
        }

        [HttpPut("{id}")]
        public Result Put(Guid id, [FromBody]CustomerInput input)
        {
            var command = new CustomerCommand
            {
                Id = id,
                Name = input.Name,
                Gender = input.Gender,
                Grade = input.Grade,
                PhoneNum = input.PhoneNum,
                Mode = CommandMode.Update,
                Group = VerificationGroup.Update,
            };

            return ServiceLocator.CommandBus.Send(command);
        }

        [HttpDelete("{id}")]
        public Result Delete(Guid id)
        {
            var command = new CustomerCommand { Id = id, Mode = CommandMode.Delete, Group = VerificationGroup.Delete };

            return ServiceLocator.CommandBus.Send(command);
        }
    }
}
