using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Localization;
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

    public class DatabaseInitalCommandHandler : ICommandHandler<DatabaseInitalCommand>
    {
        [Injection]
        public ILocalization Localization { get; set; }

        [Injection]
        public IRepository<Module> Modules { get; set; }

        [Injection]
        public IRepository<Role> Roles { get; set; }

        [Injection]
        public IRepository<User> Users { get; set; }

        [Injection]
        public IRepository<Client> Clients { get; set; }

        public Result Execute(DatabaseInitalCommand command)
        {
            bool initialized = this.Modules.Exist(e => e.Code == command.Seed.Modules.Administration.Code);

            if (initialized)
                return Result.Success(message: Localization.Get(Resources.Key.Command.SysDbInitialized));

            this.Modules.Add(command.Seed.Modules.Administration);
            this.Modules.Add(command.Seed.Modules.Retailing);
            this.Modules.Commit();

            this.Users.Add(command.Seed.Users.Administrator);
            this.Users.Commit();

            this.Roles.Add(command.Seed.Roles.Staffs);
            this.Roles.Add(command.Seed.Roles.Customers);
            this.Roles.Commit();

            this.Clients.Add(command.Seed.Clients.Central);
            this.Clients.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.SysDbInitSuccess));
        }
    }
}
