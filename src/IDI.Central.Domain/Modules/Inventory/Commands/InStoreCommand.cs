using System;
using IDI.Central.Common;
using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.BasicInfo;
using IDI.Central.Domain.Modules.BasicInfo.AggregateRoots;
using IDI.Central.Domain.Modules.Inventory.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Verification.Attributes;
using IDI.Core.Localization;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Inventory.Commands
{
    public class InStoreCommand : Command
    {
        public Guid StroeId { get; set; }

        public Guid ProductId { get; set; }

        public string BinCode { get; set; } = Configuration.Inventory.DefaultBinCode;

        [DecimalRange(Minimum = 0.01)]
        public decimal Quantity { get; set; }
    }

    public class InStoreCommandHandler : ICommandHandler<InStoreCommand>
    {
        [Injection]
        public ILocalization Localization { get; set; }

        [Injection]
        public IRepository<Store> Stores { get; set; }

        [Injection]
        public IRepository<Product> Products { get; set; }

        public Result Execute(InStoreCommand command)
        {
            var product = this.Products.Find(e => e.Id == command.ProductId);

            if (product == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.ProductNotExisting));

            var store = this.Stores.Include(e => e.Stocks).Find(e => e.Id == command.StroeId);

            if (store == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.StoreNotExisting));

            store.InStore(product, command.Quantity, command.BinCode);

            this.Stores.Update(store);
            this.Stores.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.InboundSuccess));
        }
    }
}
