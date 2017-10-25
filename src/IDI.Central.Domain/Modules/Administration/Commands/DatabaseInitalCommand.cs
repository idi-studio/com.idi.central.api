using System.Linq;
using IDI.Central.Domain.Common;
using IDI.Central.Domain.Localization;
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
        private bool created = false;

        protected override void Executing(DatabaseInitalCommand command, ITransaction transaction)
        {
            created = transaction.EnsureCreated();
        }

        protected override Result Execute(DatabaseInitalCommand command, ITransaction transaction)
        {
            if (!created)
                return Result.Success(message: Localization.Get(Resources.Key.Command.SystemInitialized));

            transaction.AddRange(command.Seed.Modules.All.ToList());
            transaction.AddRange(command.Seed.Authorization.Permissions);
            transaction.Add(command.Seed.Users.Administrator);
            transaction.Add(command.Seed.Roles.Administrators);
            transaction.Add(command.Seed.Roles.Staffs);
            transaction.Add(command.Seed.Roles.Customers);
            transaction.Add(command.Seed.Clients.Central);
            transaction.Add(command.Seed.Clients.Wechat);
            transaction.Add(command.Seed.Store);
            transaction.AddRange(command.Seed.Customers.Customers);
            transaction.AddRange(command.Seed.Products.iPhones);
            transaction.AddRange(command.Seed.Products.Others);
            transaction.AddRange(command.Seed.StockTransactions);

            transaction.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.SystemInitSuccess));
        }
    }
}
