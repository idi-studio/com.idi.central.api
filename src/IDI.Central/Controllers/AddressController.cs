﻿using System;
using IDI.Central.Common;
using IDI.Central.Core;
using IDI.Central.Domain.Modules.Sales.Commands;
using IDI.Central.Models.Sales.Inputs;
using IDI.Core.Authentication;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/addr"), ApplicationAuthorize]
    [Module(Configuration.Modules.Sales)]
    public class AddressController : Controller, IAuthorizable
    {
        private readonly ICommandBus bus;
        private readonly IQuerier querier;

        public AddressController(ICommandBus bus, IQuerier querier)
        {
            this.bus = bus;
            this.querier = querier;
        }

        [HttpPost]
        [Permission("address", PermissionType.Add)]
        public Result Post([FromBody]ShippingAddressInput input)
        {
            var command = new ShippingAddressCommand
            {
                Id = input.Id,
                CustomerId = input.CustomerId,
                Receiver = input.Receiver,
                ContactNo = input.ContactNo,
                Province = input.Province,
                City = input.City,
                Area = input.Area,
                Street = input.Street,
                Detail = input.Detail,
                Postcode = input.Postcode,
                Default = input.Default,
                Mode = CommandMode.Create,
                Group = ValidationGroup.Create,
            };

            return bus.Send(command);
        }

        [HttpPut("{id}")]
        [Permission("address", PermissionType.Upload)]
        public Result Put(Guid id, [FromBody]ShippingAddressInput input)
        {
            var command = new ShippingAddressCommand
            {
                Id = id,
                CustomerId = input.CustomerId,
                Receiver = input.Receiver,
                ContactNo = input.ContactNo,
                Province = input.Province,
                City = input.City,
                Area = input.Area,
                Street = input.Street,
                Detail = input.Detail,
                Postcode = input.Postcode,
                Default = input.Default,
                Mode = CommandMode.Update,
                Group = ValidationGroup.Update,
            };

            return bus.Send(command);
        }

        [HttpDelete("{id}")]
        [Permission("address", PermissionType.Remove)]
        public Result Delete(Guid id)
        {
            var command = new ShippingAddressCommand { Id = id, Mode = CommandMode.Delete, Group = ValidationGroup.Delete };

            return bus.Send(command);
        }
    }
}
