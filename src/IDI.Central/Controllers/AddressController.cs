using System;
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
    [Module(Common.Constants.Module.Sales)]
    public class AddressController : Controller, IAuthorizable
    {
        private readonly ICommandBus commandBus;
        private readonly IQueryProcessor queryProcessor;

        public AddressController(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            this.commandBus = commandBus;
            this.queryProcessor = queryProcessor;
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
                Group = VerificationGroup.Create,
            };

            return commandBus.Send(command);
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
                Group = VerificationGroup.Update,
            };

            return commandBus.Send(command);
        }

        [HttpDelete("{id}")]
        [Permission("address", PermissionType.Remove)]
        public Result Delete(Guid id)
        {
            var command = new ShippingAddressCommand { Id = id, Mode = CommandMode.Delete, Group = VerificationGroup.Delete };

            return commandBus.Send(command);
        }
    }
}
