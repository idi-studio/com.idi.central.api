using IDI.Central.Domain.Localization;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;
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

    public class SalesInitalCommandHandler : TransactionCommandHandler<SalesInitalCommand>
    {
        protected override Result Execute(SalesInitalCommand command, ITransaction transaction)
        {
            command.Seed.Customers.Customers.ForEach(e => transaction.Add(e));
            transaction.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.SysDataInitSuccess));
        }
    }
}
