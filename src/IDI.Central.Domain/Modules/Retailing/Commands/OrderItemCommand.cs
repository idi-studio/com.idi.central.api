using System;
using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.Retailing.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Retailing.Commands
{
    public class OrderItemCommand : Command
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal? NewUnitPrice { get; set; }

        public decimal Quantity { get; set; }
    }

    public class OrderItemCommandHandler : CommandHandler<OrderItemCommand>
    {
        [Injection]
        public IRepository<Product> Products { get; set; }

        [Injection]
        public IRepository<Order> Orders { get; set; }

        [Injection]
        public IRepository<OrderItem> OrderItems { get; set; }

        protected override Result Create(OrderItemCommand command)
        {
            var product = this.Products.Find(command.ProductId);

            if (!product.Valid())
                return Result.Fail(message: Localization.Get(Resources.Key.Command.InvalidProduct));

            var order = this.Orders.Find(command.OrderId);

            if (!order.AllowModifyItem())
                return Result.Fail(message: Localization.Get(Resources.Key.Command.OperationNonsupport));

            var item = new OrderItem
            {
                OrderId = command.OrderId,
                ProductId = command.ProductId,
                Quantity = command.Quantity,
                UnitPrice = command.UnitPrice,
                NewUnitPrice = command.NewUnitPrice,
            };

            this.OrderItems.Add(item);
            this.OrderItems.Context.Commit();
            this.OrderItems.Context.Dispose();

            return Result.Success(message: Localization.Get(Resources.Key.Command.CreateSuccess));
        }

        protected override Result Update(OrderItemCommand command)
        {
            var product = this.Products.Find(command.ProductId);

            if (!product.Valid())
                return Result.Fail(message: Localization.Get(Resources.Key.Command.InvalidProduct));

            var order = this.Orders.Find(command.OrderId);

            if (!order.AllowModifyItem())
                return Result.Fail(message: Localization.Get(Resources.Key.Command.OperationNonsupport));

            var item = this.OrderItems.Find(e => e.Id == command.Id && e.OrderId == command.OrderId);

            if (item == null)
                return Result.Fail(message: Localization.Get(Resources.Key.Command.RecordNotExisting));

            item.ProductId = command.ProductId;
            item.Quantity = command.Quantity;
            item.UnitPrice = command.UnitPrice;
            item.NewUnitPrice = command.NewUnitPrice;

            this.OrderItems.Update(item);
            this.OrderItems.Context.Commit();
            this.OrderItems.Context.Dispose();

            return Result.Success(message: Localization.Get(Resources.Key.Command.UpdateSuccess));
        }

        protected override Result Delete(OrderItemCommand command)
        {
            var order = this.Orders.Find(command.OrderId);

            if (!order.AllowModifyItem())
                return Result.Fail(message: Localization.Get(Resources.Key.Command.OperationNonsupport));

            var item = this.OrderItems.Find(command.Id);

            if (item == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.ProductNotExisting));

            this.OrderItems.Remove(item);
            this.OrderItems.Context.Commit();
            this.OrderItems.Context.Dispose();

            return Result.Success(message: Localization.Get(Resources.Key.Command.DeleteSuccess));
        }
    }
}
