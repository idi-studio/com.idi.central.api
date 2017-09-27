using IDI.Central.Common;
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

        //[Injection]
        //public IRepository<Module> Modules { get; set; }

        [Injection]
        public IRepository<Permission> Permissions { get; set; }

        [Injection]
        public IRepository<Role> Roles { get; set; }

        [Injection]
        public IRepository<User> Users { get; set; }

        [Injection]
        public IRepository<Client> Clients { get; set; }

        public Result Execute(DatabaseInitalCommand command)
        {
            bool initialized = this.Roles.Exist(e => e.Name == Configuration.Roles.Administrators);

            if (initialized)
                return Result.Success(message: Localization.Get(Resources.Key.Command.SysDbInitialized));

            Permissions.AddRange(command.Seed.Authorization.Permissions);
            Permissions.Commit();

            Users.Add(command.Seed.Users.Administrator);
            Users.Commit();

            Roles.Add(command.Seed.Roles.Administrators);
            Roles.Add(command.Seed.Roles.Staffs);
            Roles.Add(command.Seed.Roles.Customers);
            Roles.Commit();

            Clients.Add(command.Seed.Clients.Central);
            Clients.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.SysDbInitSuccess));
        }
    }
}
