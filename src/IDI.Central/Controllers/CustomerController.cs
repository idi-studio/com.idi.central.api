using System;
using IDI.Central.Core;
using IDI.Central.Domain.Modules.Sales.Commands;
using IDI.Central.Domain.Modules.Sales.Queries;
using IDI.Central.Models.Sales;
using IDI.Central.Models.Sales.Inputs;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/cust"), ApplicationAuthorize]
    public class CustomerController : Controller
    {
        private readonly ICommandBus commandBus;
        private readonly IQueryProcessor queryProcessor;

        public CustomerController(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            this.commandBus = commandBus;
            this.queryProcessor = queryProcessor;
        }

        [HttpGet("{id}")]
        public Result<CustomerModel> Get(Guid id)
        {
            var condition = new QueryCustomerCondition {  Id = id };

            return queryProcessor.Execute<QueryCustomerCondition, CustomerModel>(condition);
        }

        [HttpGet("list")]
        public Result<Set<CustomerModel>> List()
        {
            return queryProcessor.Execute<QueryCustomerSetCondition, Set<CustomerModel>>();
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

            return commandBus.Send(command);
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

            return commandBus.Send(command);
        }

        [HttpDelete("{id}")]
        public Result Delete(Guid id)
        {
            var command = new CustomerCommand { Id = id, Mode = CommandMode.Delete, Group = VerificationGroup.Delete };

            return commandBus.Send(command);
        }
    }
}
