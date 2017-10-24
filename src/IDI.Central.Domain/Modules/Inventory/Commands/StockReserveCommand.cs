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
    public class StockReserveCommand : Command
    {
        public Guid StroeId { get; set; }

        public List<StockItem> Items { get; set; } = new List<StockItem>();
    }

    public class StockReserveCommandHandler : TransactionCommandHandler<StockReserveCommand>
    {
        protected override Result Execute(StockReserveCommand command, ITransaction transaction)
        {
            var store = transaction.Source<Store>().Include(e => e.Stocks).Find(e => e.Id == command.StroeId);

            if (store == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.StoreNotExisting));

            var result = new List<StockTransaction>();

            foreach (var item in command.Items)
            {
                var product = transaction.Source<Product>().Find(e => e.Id == item.ProductId);

                if (product == null)
                    return Result.Fail(Localization.Get(Resources.Key.Command.ProductNotExisting));

                decimal remain;

                var trans = new List<StockTransaction>();

                if (!store.Reserve(product, item.Quantity, item.BinCode, out remain, out trans))
                    return Result.Fail(message: Localization.Get(Resources.Key.Command.ProductOutOfStock.ToFormat(product.Name)));

                result.AddRange(trans);
            }

            transaction.Update(store);
            transaction.AddRange(result);
            transaction.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.StockReserveSuccess));
        }
    }
}
