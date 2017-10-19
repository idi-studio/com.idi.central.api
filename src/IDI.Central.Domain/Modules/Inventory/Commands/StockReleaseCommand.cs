using System;
using System.Collections.Generic;
using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.BasicInfo;
using IDI.Central.Domain.Modules.BasicInfo.AggregateRoots;
using IDI.Central.Domain.Modules.Inventory.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Inventory.Commands
{
    public class StockReleaseCommand : Command
    {
        public Guid StroeId { get; set; }

        public List<StockItem> Items { get; set; } = new List<StockItem>();
    }

    public class StockReleaseCommandHandler : TransactionCommandHandler<StockReleaseCommand>
    {
        protected override Result Execute(StockReleaseCommand command, ITransaction transaction)
        {
            var store = transaction.Source<Store>().Include(e => e.Stocks).Find(e => e.Id == command.StroeId);

            if (store == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.StoreNotExisting));

            var storeTrans = new List<StockTransaction>();

            foreach (var item in command.Items)
            {
                var product = transaction.Source<Product>().Find(e => e.Id == item.ProductId);

                if (product == null)
                    return Result.Fail(Localization.Get(Resources.Key.Command.ProductNotExisting));

                var trans = new List<StockTransaction>();

                store.StockIn(product, item.Quantity, item.BinCode, out trans);

                storeTrans.AddRange(trans);
            }

            transaction.Update(store);
            transaction.AddRange(storeTrans);
            transaction.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.StockReleaseSuccess));
        }
    }
}
