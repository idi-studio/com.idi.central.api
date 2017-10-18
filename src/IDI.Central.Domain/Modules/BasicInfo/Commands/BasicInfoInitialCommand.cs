using IDI.Central.Domain.Localization;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.BasicInfo.Commands
{
    public class BasicInfoInitialCommand : Command
    {
        public Seed Seed { get; private set; }

        public BasicInfoInitialCommand()
        {
            this.Seed = new Seed();
        }
    }

    public class BasicInfoInitialCommandHandler : TransactionCommandHandler<BasicInfoInitialCommand>
    {
        protected override Result Execute(BasicInfoInitialCommand command, ITransaction transaction)
        {
            command.Seed.Products.iPhones.ForEach(e => transaction.Add(e));
            command.Seed.Products.Others.ForEach(e => transaction.Add(e));

            transaction.Add(command.Seed.Store);
            transaction.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.SysDataInitSuccess));
        }
    }
}
