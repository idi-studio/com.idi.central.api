using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.Retailing.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Localization;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Retailing.Commands
{
    public class RetailingInitalCommand : Command
    {
        public Seed Seed { get; private set; }

        public RetailingInitalCommand()
        {
            this.Seed = new Seed();
        }
    }

    public class RetailingInitalCommandHandler : ICommandHandler<RetailingInitalCommand>
    {
        [Injection]
        public ILocalization Localization { get; set; }

        [Injection]
        public IRepository<Customer> Customers { get; set; }

        public Result Execute(RetailingInitalCommand command)
        {
            command.Seed.Customers.Customers.ForEach(e => this.Customers.Add(e));
            this.Customers.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.SysDataInitSuccess));
        }
    }
}
