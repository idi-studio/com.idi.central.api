using System;
using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Central.Domain.Modules.Sales.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Verification.Attributes;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Sales.Commands
{
    public class CustomerCommand : Command
    {
        public Guid Id { get; set; }

        [RequiredField(Group = VerificationGroup.Create | VerificationGroup.Update)]
        [StringLength(MaxLength = 50, Group = VerificationGroup.Create | VerificationGroup.Update)]
        public string Name { get; set; }

        [RequiredField(Group = VerificationGroup.Create | VerificationGroup.Update)]
        [StringLength(MaxLength = 50, Group = VerificationGroup.Create | VerificationGroup.Update)]
        public string PhoneNum { get; set; }

        public int Grade { get; set; }

        public Gender Gender { get; set; }
    }

    public class CustomerCommandHandler : CommandHandler<CustomerCommand>
    {
        [Injection]
        public IRepository<Customer> Customers { get; set; }

        [Injection]
        public IRepository<User> Users { get; set; }

        [Injection]
        public IRepository<UserProfile> Profiles { get; set; }

        [Injection]
        public IRepository<Role> Roles { get; set; }

        [Injection]
        public IRepository<UserRole> UserRoles { get; set; }

        protected override Result Create(CustomerCommand command)
        {
            if (this.Users.Include(e => e.Profile).Exist(e => e.Profile.PhoneNum == command.PhoneNum))
                return Result.Fail(Localization.Get(Resources.Key.Command.PhoneNumRegistered));

            var customers = this.Roles.Find(e => e.Name == Central.Common.Constants.Roles.Customers);

            if (customers == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.InvalidRole));

            var customer = new Customer
            {
                Name = command.Name,
                Grade = command.Grade,
            };

            var salt = Cryptography.Salt();

            customer.User = new User
            {
                UserName = $"cust{command.PhoneNum}",
                Salt = salt,
                Password = Cryptography.Encrypt(command.PhoneNum.Substring(2, command.PhoneNum.Length - 3), salt),
                IsLocked = true,
                LockTime = DateTime.MaxValue,
                Profile = new UserProfile
                {
                    Name = command.Name,
                    Gender = command.Gender,
                    PhoneNum = command.PhoneNum,
                    Photo = $"{command.Gender.ToString().ToLower()}.png"
                }
            };

            this.Customers.Add(customer);
            this.Customers.Commit();

            this.UserRoles.Add(new UserRole { UserId = customer.User.Id, RoleId = customers.Id });
            this.UserRoles.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.CreateSuccess));
        }

        protected override Result Update(CustomerCommand command)
        {
            var customer = this.Customers.Include(e => e.User).AlsoInclude(e => e.Profile).Find(command.Id);

            if (customer == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.RecordNotExisting));

            customer.Name = command.Name;
            customer.Grade = command.Grade;

            this.Customers.Update(customer);
            this.Customers.Commit();

            if (!customer.User.Profile.PhoneVerified)
            {
                customer.User.Profile.Gender = command.Gender;
                customer.User.Profile.PhoneNum = command.PhoneNum;
                customer.User.Profile.Photo = $"{command.Gender.ToString().ToLower()}.png";

                this.Profiles.Update(customer.User.Profile);
                this.Profiles.Commit();
            }

            return Result.Success(message: Localization.Get(Resources.Key.Command.UpdateSuccess));
        }

        protected override Result Delete(CustomerCommand command)
        {
            var customer = this.Customers.Include(e => e.User).Include(e => e.Shippings).Include(e => e.Orders).Find(command.Id);

            if (customer == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.RecordNotExisting));

            if (customer.Orders.Count > 0 || customer.Shippings.Count > 0)
                return Result.Fail(Localization.Get(Resources.Key.Command.OperationNonsupport));

            this.Customers.Remove(customer);
            this.Customers.Commit();

            this.Users.Remove(customer.User);
            this.Users.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.DeleteSuccess));
        }
    }
}
