using System;
using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.Retailing.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Verification.Attributes;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Retailing.Commands
{
    public class ShippingAddressCommand : Command
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        [RequiredField(Group = VerificationGroup.Create | VerificationGroup.Update)]
        [StringLength(MaxLength = 30, Group = VerificationGroup.Create | VerificationGroup.Update)]
        public string Receiver { get; set; }

        [RequiredField(Group = VerificationGroup.Create | VerificationGroup.Update)]
        [StringLength(MaxLength = 20, Group = VerificationGroup.Create | VerificationGroup.Update)]
        public string ContactNo { get; set; }

        [RequiredField(Group = VerificationGroup.Create | VerificationGroup.Update)]
        [StringLength(MaxLength = 100, Group = VerificationGroup.Create | VerificationGroup.Update)]
        public string Province { get; set; }

        [RequiredField(Group = VerificationGroup.Create | VerificationGroup.Update)]
        [StringLength(MaxLength = 100, Group = VerificationGroup.Create | VerificationGroup.Update)]
        public string City { get; set; }

        [RequiredField(Group = VerificationGroup.Create | VerificationGroup.Update)]
        [StringLength(MaxLength = 100, Group = VerificationGroup.Create | VerificationGroup.Update)]
        public string Area { get; set; }

        [RequiredField(Group = VerificationGroup.Create | VerificationGroup.Update)]
        [StringLength(MaxLength = 200, Group = VerificationGroup.Create | VerificationGroup.Update)]
        public string Street { get; set; }

        [RequiredField(Group = VerificationGroup.Create | VerificationGroup.Update)]
        [StringLength(MaxLength = 200, Group = VerificationGroup.Create | VerificationGroup.Update)]
        public string Detail { get; set; }

        [RequiredField(Group = VerificationGroup.Create | VerificationGroup.Update)]
        [StringLength(MaxLength = 20, Group = VerificationGroup.Create | VerificationGroup.Update)]
        public string Postcode { get; set; }
    }

    public class ShippingCommandHandler : CommandHandler<ShippingAddressCommand>
    {
        [Injection]
        public IRepository<ShippingAddress> Shippings { get; set; }

        [Injection]
        public IRepository<Customer> Customers { get; set; }

        protected override Result Create(ShippingAddressCommand command)
        {
            var customer = this.Customers.Find(e => e.Id == command.CustomerId);

            if (customer == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.InvalidCustomer));

            var shipping = new ShippingAddress
            {
                CustomerId = command.CustomerId,
                Receiver = command.Receiver,
                ContactNo = command.ContactNo,
                Province = command.Province,
                City = command.City,
                Area = command.Area,
                Street = command.Street,
                Detail = command.Detail,
                Postcode = command.Postcode
            };

            this.Shippings.Add(shipping);
            this.Shippings.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.CreateSuccess));
        }

        protected override Result Update(ShippingAddressCommand command)
        {
            var customer = this.Customers.Find(e => e.Id == command.CustomerId);

            if (customer == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.InvalidCustomer));

            var shipping = this.Shippings.Find(command.Id);

            if (shipping == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.RecordNotExisting));

            shipping.CustomerId = command.CustomerId;
            shipping.Receiver = command.Receiver;
            shipping.ContactNo = command.ContactNo;
            shipping.Province = command.Province;
            shipping.City = command.City;
            shipping.Area = command.Area;
            shipping.Street = command.Street;
            shipping.Detail = command.Detail;
            shipping.Postcode = command.Postcode;

            this.Shippings.Update(shipping);
            this.Shippings.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.UpdateSuccess));
        }

        protected override Result Delete(ShippingAddressCommand command)
        {
            var shipping = this.Shippings.Find(command.Id);

            if (shipping == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.RecordNotExisting));

            this.Shippings.Remove(shipping);
            this.Shippings.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.DeleteSuccess));
        }
    }
}
