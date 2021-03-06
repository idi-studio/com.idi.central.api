﻿using System;
using IDI.Central.Common;
using IDI.Central.Core;
using IDI.Central.Domain.Modules.Sales.Commands;
using IDI.Central.Domain.Modules.Sales.Queries;
using IDI.Central.Models.Sales;
using IDI.Central.Models.Sales.Inputs;
using IDI.Core.Authentication;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/cust"), ApplicationAuthorize]
    [Module(Configuration.Modules.Sales)]
    public class CustomerController : Controller, IAuthorizable
    {
        private readonly ICommandBus bus;
        private readonly IQuerier querier;

        public CustomerController(ICommandBus bus, IQuerier querier)
        {
            this.bus = bus;
            this.querier = querier;
        }

        [HttpGet("{id}")]
        [Permission("customer", PermissionType.Read)]
        public Result<CustomerModel> Get(Guid id)
        {
            var condition = new QueryCustomerCondition {  Id = id };

            return querier.Execute<QueryCustomerCondition, CustomerModel>(condition);
        }

        [HttpGet("list")]
        [Permission("customer", PermissionType.Query)]
        public Result<Set<CustomerModel>> List()
        {
            return querier.Execute<QueryCustomerSetCondition, Set<CustomerModel>>();
        }

        [HttpPost]
        [Permission("customer", PermissionType.Add)]
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
                Group = ValidationGroup.Create,
            };

            return bus.Send(command);
        }

        [HttpPut("{id}")]
        [Permission("customer", PermissionType.Upload)]
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
                Group = ValidationGroup.Update,
            };

            return bus.Send(command);
        }

        [HttpDelete("{id}")]
        [Permission("customer", PermissionType.Remove)]
        public Result Delete(Guid id)
        {
            var command = new CustomerCommand { Id = id, Mode = CommandMode.Delete, Group = ValidationGroup.Delete };

            return bus.Send(command);
        }
    }
}
