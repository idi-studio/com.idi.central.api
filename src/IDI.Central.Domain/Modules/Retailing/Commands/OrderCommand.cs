using System;
using System.Collections.Generic;
using System.Linq;
using IDI.Central.Common.Enums;
using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.Retailing.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Retailing.Commands
{
    public class OrderCommand : Command
    {
        public Guid Id { get; set; }

        public OrderCategory Category { get; set; }

        public string Remark { get; set; }

        public Guid CustomerId { get; set; }

        public List<OrderItemCommand> Items { get; set; }
    }

    public class OrderCommandHandler : CommandHandler<OrderCommand>
    {
        [Injection]
        public IRepository<Order> Orders { get; set; }

        protected override Result Create(OrderCommand command)
        {
            //if (this.Products.Exist(e => e.QRCode == command.Code))
            //    return Result.Fail(Localization.Get(Resources.Key.Command.ProductCodeDuplicated));

            //var product = new Product
            //{
            //    Name = command.Name.TrimContiguousSpaces(),
            //    QRCode = command.Code,
            //    Tags = command.Tags,
            //    Enabled = command.Enabled,
            //    OnShelf = false
            //};

            //this.Products.Add(product);
            //this.Products.Context.Commit();
            //this.Products.Context.Dispose();

            return Result.Success(message: Localization.Get(Resources.Key.Command.CreateSuccess));
        }

        protected override Result Update(OrderCommand command)
        {
            //if (this.Products.Exist(e => e.QRCode == command.Code && e.Id != command.Id))
            //    return Result.Fail(Localization.Get(Resources.Key.Command.ProductCodeDuplicated));

            //var product = this.Products.Find(command.Id, p => p.Prices);

            //if (product == null)
            //    return Result.Fail(Localization.Get(Resources.Key.Command.ProductNotExisting));

            //if (command.OnShelf && !product.Prices.Any(p => p.Category == Central.Common.PriceCategory.Selling && p.Enabled))
            //    return Result.Fail(Localization.Get(Resources.Key.Command.RequiredSellingPrice));

            //product.Name = command.Name.TrimContiguousSpaces();
            //product.QRCode = command.Code;
            //product.Tags = command.Tags;
            //product.Enabled = command.Enabled;
            //product.OnShelf = command.OnShelf;

            //this.Products.Update(product);
            //this.Products.Context.Commit();
            //this.Products.Context.Dispose();

            return Result.Success(message: Localization.Get(Resources.Key.Command.UpdateSuccess));
        }

        protected override Result Delete(OrderCommand command)
        {
            //var product = this.Products.Find(command.Id);

            //if (product == null)
            //    return Result.Fail(Localization.Get(Resources.Key.Command.ProductNotExisting));

            //this.Products.Remove(product);
            //this.Products.Context.Commit();
            //this.Products.Context.Dispose();

            return Result.Success(message: Localization.Get(Resources.Key.Command.DeleteSuccess));
        }
    }
}
