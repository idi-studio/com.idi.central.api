using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.BasicInfo.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Localization;
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

    public class BasicInfoInitialCommandHandler : ICommandHandler<BasicInfoInitialCommand>
    {
        [Injection]
        public ILocalization Localization { get; set; }

        [Injection]
        public IRepository<Product> Products { get; set; }

        public Result Execute(BasicInfoInitialCommand command)
        {
            command.Seed.Products.iPhones.ForEach(e => this.Products.Add(e));
            command.Seed.Products.Others.ForEach(e => this.Products.Add(e));
            this.Products.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.SysDataInitSuccess));
        }
    }
}
