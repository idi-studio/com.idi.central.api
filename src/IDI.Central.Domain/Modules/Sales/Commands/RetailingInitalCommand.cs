using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.Sales.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Localization;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Sales.Commands
{
    public class SalesInitalCommand : Command
    {
        public Seed Seed { get; private set; }

        public SalesInitalCommand()
        {
            this.Seed = new Seed();
        }
    }

    public class SalesInitalCommandHandler : ICommandHandler<SalesInitalCommand>
    {
        [Injection]
        public ILocalization Localization { get; set; }

        [Injection]
        public IRepository<Customer> Customers { get; set; }

        public Result Execute(SalesInitalCommand command)
        {
            command.Seed.Customers.Customers.ForEach(e => this.Customers.Add(e));
            this.Customers.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.SysDataInitSuccess));
        }
    }
}
