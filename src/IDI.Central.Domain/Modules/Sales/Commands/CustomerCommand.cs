using System;
using IDI.Central.Common;
using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Central.Domain.Modules.Sales.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.Verification.Attributes;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Sales.Commands
{
    public class CustomerCommand : Command
    {
        public Guid Id { get; set; }

        [RequiredField(Group = ValidationGroup.Create | ValidationGroup.Update)]
        [StringLength(MaxLength = 50, Group = ValidationGroup.Create | ValidationGroup.Update)]
        public string Name { get; set; }

        [RequiredField(Group = ValidationGroup.Create | ValidationGroup.Update)]
        [StringLength(MaxLength = 50, Group = ValidationGroup.Create | ValidationGroup.Update)]
        public string PhoneNum { get; set; }

        public int Grade { get; set; }

        public Gender Gender { get; set; }
    }

    public class CustomerCommandHandler : CRUDTransactionCommandHandler<CustomerCommand>
    {
        protected override Result Create(CustomerCommand command, ITransaction transaction)
        {
            if (transaction.Source<User>().Include(e => e.Profile).Exist(e => e.Profile.PhoneNum == command.PhoneNum))
                return Result.Fail(Localization.Get(Resources.Key.Command.PhoneNumRegistered));

            var customer = new Customer
            {
                Name = command.Name,
                Grade = command.Grade,
            };

            customer.User = new User
            {
                UserName = $"cust{command.PhoneNum}",
                SecretKey = Cryptography.NewSecretKey(command.PhoneNum.Substring(2, command.PhoneNum.Length - 3)).ToString(),
                IsLocked = true,
                LockTime = DateTime.MaxValue,
                Profile = new UserProfile
                {
                    Name = command.Name,
                    Gender = command.Gender,
                    PhoneNum = command.PhoneNum,
                    Photo = $"{command.Gender.ToString().ToLower()}.png"
                },
                Role = new UserRole
                {
                    Roles = Configuration.Roles.Customers
                }
            };

            transaction.Add(customer);
            transaction.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.CreateSuccess));
        }

        protected override Result Update(CustomerCommand command, ITransaction transaction)
        {
            var customer = transaction.Source<Customer>().Include(e => e.User).AlsoInclude(e => e.Profile).Find(command.Id);

            if (customer == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.RecordNotExisting));

            customer.Name = command.Name;
            customer.Grade = command.Grade;

            transaction.Update(customer);

            if (!customer.User.Profile.PhoneVerified)
            {
                customer.User.Profile.Gender = command.Gender;
                customer.User.Profile.PhoneNum = command.PhoneNum;
                customer.User.Profile.Photo = $"{command.Gender.ToString().ToLower()}.png";

                transaction.Update(customer.User.Profile);
            }

            transaction.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.UpdateSuccess));
        }

        protected override Result Delete(CustomerCommand command, ITransaction transaction)
        {
            var customer = transaction.Source<Customer>().Include(e => e.User).Include(e => e.Shippings).Include(e => e.Orders).Find(command.Id);

            if (customer == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.RecordNotExisting));

            if (customer.Orders.Count > 0 || customer.Shippings.Count > 0)
                return Result.Fail(Localization.Get(Resources.Key.Command.OperationNonsupport));

            transaction.Remove(customer);
            transaction.Remove(customer.User);
            transaction.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.DeleteSuccess));
        }
    }
}
