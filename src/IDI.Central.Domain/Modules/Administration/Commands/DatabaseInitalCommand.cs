using IDI.Central.Common;
using IDI.Central.Domain.Common;
using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Administration.Commands
{
    public class DatabaseInitalCommand : Command
    {
        public Seed Seed { get; private set; }

        public DatabaseInitalCommand()
        {
            this.Seed = new Seed();
        }
    }

    public class DatabaseInitalCommandHandler : TransactionCommandHandler<DatabaseInitalCommand>
    {
        protected override Result Execute(DatabaseInitalCommand command, ITransaction transaction)
        {
            bool initialized = transaction.Source<Role>().Exist(e => e.Name == Configuration.Roles.Administrators);

            if (initialized)
                return Result.Success(message: Localization.Get(Resources.Key.Command.SystemInitialized));

            transaction.AddRange(command.Seed.Authorization.Permissions);
            transaction.Add(command.Seed.Users.Administrator);
            transaction.Add(command.Seed.Roles.Administrators);
            transaction.Add(command.Seed.Roles.Staffs);
            transaction.Add(command.Seed.Roles.Customers);
            transaction.Add(command.Seed.Clients.Central);
            transaction.Add(command.Seed.Store);
            transaction.AddRange(command.Seed.Products.iPhones);
            transaction.AddRange(command.Seed.Products.Others);
            transaction.AddRange(command.Seed.Customers.Customers);

            transaction.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.SystemInitSuccess));
        }
    }
}
