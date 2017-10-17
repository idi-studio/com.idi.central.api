using System;
using IDI.Central.Common.Enums;
using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.Sales.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Verification.Attributes;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Sales.Commands
{
    public class OrderCommand : Command
    {
        public Guid Id { get; set; }

        public OrderCategory Category { get; set; }

        public OrderStatus Status { get; set; }

        [StringLength(MaxLength = 200, Group = VerificationGroup.Create | VerificationGroup.Update)]
        public string Remark { get; set; }

        public Guid? CustomerId { get; set; }
    }

    public class OrderCommandHandler : CRUDCommandHandler<OrderCommand>
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
            this.Orders.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.CreateSuccess)).Attach("oid", order.Id);
        }

        protected override Result Update(OrderCommand command)
        {
            if (command.CustomerId.HasValue && !this.Customers.Exist(e => e.Id == command.CustomerId))
                return Result.Fail(Localization.Get(Resources.Key.Command.InvalidCustomer));

            var order = this.Orders.Include(e => e.Items).Include(e => e.Customer).Find(command.Id);

            if (order == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.RecordNotExisting));

            return Handle(order, command);
        }

        protected override Result Delete(OrderCommand command)
        {
            var order = this.Orders.Find(command.Id);

            if (order == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.RecordNotExisting));

            this.Orders.Remove(order);
            this.Orders.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.DeleteSuccess));
        }

        private Result Handle(Order order, OrderCommand command)
        {
            var result = Result.Fail(message: Localization.Get(Resources.Key.Command.OperationNonsupport));

            Save(order, command, ref result);

            Confirm(order, command, ref result);

            return result;
        }

        private void Save(Order order, OrderCommand command, ref Result result)
        {
            if (!(order.Status == OrderStatus.Pending && command.Status == OrderStatus.Pending))
                return;

            if (command.CustomerId.HasValue)
                order.CustomerId = command.CustomerId;

            order.Remark = command.Remark;

            this.Orders.Update(order);
            this.Orders.Commit();

            result = Result.Success(message: Localization.Get(Resources.Key.Command.UpdateSuccess));
        }

        private void Confirm(Order order, OrderCommand command, ref Result result)
        {
            if (!(order.Status == OrderStatus.Pending && command.Status == OrderStatus.Confirmed))
                return;

            if (!(order.HasItems() && order.HasCustomer()))
            {
                result = Result.Fail(message: Localization.Get(Resources.Key.Command.IncompletedOrder));
                return;
            }

            order.Status = OrderStatus.Confirmed;

            this.Orders.Update(order);
            this.Orders.Commit();

            result = Result.Success(message: Localization.Get(Resources.Key.Command.OrderConfirmed));
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
