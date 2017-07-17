using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Localization;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Administration.Commands
{
    public class DataInitializationCommandHandler : ICommandHandler<DataInitializationCommand>
    {
        [Injection]
        public IRepository<Module> Modules { get; set; }

        [Injection]
        public IRepository<Role> Roles { get; set; }

        [Injection]
        public IRepository<User> Users { get; set; }

        [Injection]
        public IRepository<Client> Clients { get; set; }

        public Result Execute(DataInitializationCommand command)
        {
            bool initialized = this.Modules.Exist(e => e.Code == command.Seed.Modules.Administration.Code);

            if (initialized)
                return Result.Success(message: Language.Instance.GetByCulture("command", "system-data-initialized"));

            this.Modules.Add(command.Seed.Modules.Administration);
            this.Modules.Add(command.Seed.Modules.Sales);
            this.Modules.Context.Commit();

            this.Users.Add(command.Seed.Users.Administrator);
            this.Users.Context.Commit();

            this.Clients.Add(command.Seed.Clients.Central);
            this.Clients.Context.Commit();

            return Result.Success(message: Language.Instance.GetByCulture("command", "system-data-initialize-success"));
        }
    }
}
