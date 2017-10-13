using System;
using System.Collections.Generic;
using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.BasicInfo;
using IDI.Central.Domain.Modules.BasicInfo.AggregateRoots;
using IDI.Central.Domain.Modules.Inventory.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Localization;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Inventory.Commands
{
    public class BatchOutStoreCommand : Command
    {
        public Guid StroeId { get; set; }

        public List<StockItem> Items { get; set; } = new List<StockItem>();
    }

    public class BatchOutStoreCommandHandler : ICommandHandler<BatchOutStoreCommand>
    {
        [Injection]
        public ILocalization Localization { get; set; }

        [Injection]
        public IRepository<Store> Stores { get; set; }

        [Injection]
        public IRepository<Product> Products { get; set; }

        public Result Execute(BatchOutStoreCommand command)
        {
            var store = this.Stores.Include(e => e.Stocks).Find(e => e.Id == command.StroeId);

            if (store == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.StoreNotExisting));

            foreach (var item in command.Items)
            {
                var product = this.Products.Find(e => e.Id == item.ProductId);

                if (product == null)
                    return Result.Fail(Localization.Get(Resources.Key.Command.ProductNotExisting));

                decimal remain;

                if (!store.OutStore(product, item.Quantity, out remain, item.BinCode))
                    return Result.Fail(message: Localization.Get(Resources.Key.Command.ProductOutOfStock.ToFormat(product.Name)));
            }

            this.Stores.Update(store);
            this.Stores.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.OutboundSuccess));
        }
    }
}
