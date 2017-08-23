using System;
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

        public Guid? CustomerId { get; set; }
    }

    public class OrderCommandHandler : CommandHandler<OrderCommand>
    {
        [Injection]
        public IRepository<Order> Orders { get; set; }

        [Injection]
        public IRepository<Customer> Customers { get; set; }

        protected override Result Create(OrderCommand command)
        {
            if (command.CustomerId.HasValue && !this.Customers.Exist(e => e.Id == command.CustomerId))
                return Result.Fail(Localization.Get(Resources.Key.Command.InvalidCustomer));

            DateTime timestamp = DateTime.Now;

            var order = new Order
            {
                CustomerId = command.CustomerId,
                Category = command.Category,
                Date = timestamp,
                Remark = command.Remark,
                Status = OrderStatus.Pending,
                SN = GenerateSerialNumber(command.Category, timestamp)
            };

            this.Orders.Add(order);
            this.Orders.Context.Commit();
            this.Orders.Context.Dispose();

            return Result.Success(message: Localization.Get(Resources.Key.Command.CreateSuccess)).Attach("oid", order.Id);
        }

        protected override Result Update(OrderCommand command)
        {
            var order = this.Orders.Find(command.Id);

            if (order == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.RecordNotExisting));

            order.Remark = command.Remark;

            this.Orders.Update(order);
            this.Orders.Context.Commit();
            this.Orders.Context.Dispose();

            return Result.Success(message: Localization.Get(Resources.Key.Command.UpdateSuccess));
        }

        protected override Result Delete(OrderCommand command)
        {
            var order = this.Orders.Find(command.Id);

            if (order == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.RecordNotExisting));

            this.Orders.Remove(order);
            this.Orders.Context.Commit();
            this.Orders.Context.Dispose();

            return Result.Success(message: Localization.Get(Resources.Key.Command.DeleteSuccess));

            //return Result.Fail(message: Localization.Get(Resources.Key.Command.OperationNonsupport));
        }

        private string GenerateSerialNumber(OrderCategory category, DateTime timestamp)
        {
            string prefix = string.Empty;

            switch (category)
            {
                case OrderCategory.Sales:
                    prefix = "SOO-";
                    break;
                case OrderCategory.Purchase:
                    prefix = "POO-";
                    break;
                case OrderCategory.StockIn:
                    prefix = "SIN-";
                    break;
                case OrderCategory.StockOut:
                    prefix = "SOT-";
                    break;
                default:
                    prefix = string.Empty;
                    break;
            }

            return $"{prefix}{timestamp.ToString("yyMMdd")}{timestamp.TimeOfDay.Ticks.ToString("x8")}".ToUpper();
        }
    }
}
