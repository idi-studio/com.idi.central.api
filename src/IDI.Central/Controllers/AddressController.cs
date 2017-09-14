using System;
using IDI.Central.Core;
using IDI.Central.Domain.Modules.Retailing.Commands;
using IDI.Central.Models.Retailing.Inputs;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/addr"), ApplicationAuthorize]
    public class AddressController : Controller
    {
        [HttpPost]
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
                Mode = CommandMode.Create,
                Group = VerificationGroup.Create,
            };

            return ServiceLocator.CommandBus.Send(command);
        }

        [HttpPut("{id}")]
        public Result Put(Guid id, [FromBody]ShippingAddressInput input)
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
                Mode = CommandMode.Update,
                Group = VerificationGroup.Update,
            };

            return ServiceLocator.CommandBus.Send(command);
        }

        [HttpDelete("{id}")]
        public Result Delete(Guid id)
        {
            var command = new ShippingAddressCommand { Id = id, Mode = CommandMode.Delete, Group = VerificationGroup.Delete };

            return ServiceLocator.CommandBus.Send(command);
        }
    }
}
