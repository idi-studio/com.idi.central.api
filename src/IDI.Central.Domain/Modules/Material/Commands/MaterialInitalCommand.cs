using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.Material.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Localization;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Material.Commands
{
    public class MaterialInitalCommand : Command
    {
        public Seed Seed { get; private set; }

        public MaterialInitalCommand()
        {
            this.Seed = new Seed();
        }
    }

    public class MaterialInitalCommandHandler : ICommandHandler<MaterialInitalCommand>
    {
        [Injection]
        public ILocalization Localization { get; set; }

        [Injection]
        public IRepository<Product> Products { get; set; }

        public Result Execute(MaterialInitalCommand command)
        {
            command.Seed.Products.iPhones.ForEach(e => this.Products.Add(e));
            command.Seed.Products.Others.ForEach(e => this.Products.Add(e));
            this.Products.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.SysDataInitSuccess));
        }
    }
}
