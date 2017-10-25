using System;
using System.Collections.Generic;
using IDI.Central.Common.Enums;
using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.BasicInfo;
using IDI.Central.Domain.Modules.BasicInfo.AggregateRoots;
using IDI.Central.Domain.Modules.Inventory.AggregateRoots;
using IDI.Central.Domain.Modules.Sales.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure.Commands;
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

    public class OrderCommandHandler : CRUDTransactionCommandHandler<OrderCommand>
    {
        protected override Result Create(OrderCommand command, ITransaction transaction)
        {
            if (command.CustomerId.HasValue && !transaction.Source<Customer>().Exist(e => e.Id == command.CustomerId))
                return Result.Fail(Localization.Get(Resources.Key.Command.InvalidCustomer));

            DateTime timestamp = DateTime.Now;

            var order = new Order
            {
                CustomerId = command.CustomerId,
                Category = command.Category,
                Date = timestamp,
                Remark = command.Remark,
                Status = OrderStatus.Created,
                SN = GenerateSerialNumber(command.Category, timestamp)
            };

            transaction.Add(order);
            transaction.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.CreateSuccess)).Attach("oid", order.Id);
        }

        protected override Result Update(OrderCommand command, ITransaction transaction)
        {
            if (command.CustomerId.HasValue && !transaction.Source<Customer>().Exist(e => e.Id == command.CustomerId))
                return Result.Fail(Localization.Get(Resources.Key.Command.InvalidCustomer));

            var order = transaction.Source<Order>().Include(e => e.Customer).Find(command.Id);

            if (order == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.RecordNotExisting));

            return Handle(order, command, transaction);
        }

        protected override Result Delete(OrderCommand command, ITransaction transaction)
        {
            var order = transaction.Source<Order>().Find(command.Id);

            if (order == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.RecordNotExisting));

            transaction.Remove(order);
            transaction.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.DeleteSuccess));
        }

        private Result Handle(Order order, OrderCommand command, ITransaction transaction)
        {
            if (order.Status == OrderStatus.Created)
            {
                return Save(order, command, transaction);
            }
            else if (order.Status == OrderStatus.Pending && command.Status == OrderStatus.Confirmed)
            {
                return Confirm(order, command, transaction);
            }
            else if (order.Status == OrderStatus.Confirmed && command.Status == OrderStatus.Cancelled)
            {
                return Cancel(order, command, transaction);
            }
            else
            {
                return Result.Fail(message: Localization.Get(Resources.Key.Command.OperationNonsupport));
            };
        }

        private Result Save(Order order, OrderCommand command, ITransaction transaction)
        {
            if (!command.CustomerId.HasValue)
                return Result.Fail(Localization.Get(Resources.Key.Command.IncompletedOrder));

            order.Status = OrderStatus.Pending;
            order.CustomerId = command.CustomerId;
            order.Remark = command.Remark;

            transaction.Update(order);
            transaction.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.UpdateSuccess));
        }

        private Result Confirm(Order order, OrderCommand command, ITransaction transaction)
        {
            order = transaction.Source<Order>().Include(e => e.Customer).Include(e => e.Items).Find(command.Id);

            if (!(order.HasItems() && order.HasCustomer()))
                return Result.Fail(message: Localization.Get(Resources.Key.Command.IncompletedOrder));

            foreach (var item in order.Items)
            {
                var product = transaction.Source<Product>().Include(e => e.Stocks).Include(e => e.Stock).Find(item.ProductId);

                var remain = 0M;
                var trans = new List<StockTransaction>();

                if (product.Reserve(item.Quantity, product.Stock.BinCode, out remain, out trans))
                {
                    transaction.UpdateRange(product.Stocks);
                    transaction.AddRange(trans);
                }
                else
                {
                    transaction.Rollback();
                    return Result.Fail(message: Localization.Get(Resources.Key.Command.ProductOutOfStock).ToFormat(product.Name));
                }
            }

            order.Status = OrderStatus.Confirmed;

            transaction.Update(order);
            transaction.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.OrderConfirmed));
        }

        private Result Cancel(Order order, OrderCommand command, ITransaction transaction)
        {
            order = transaction.Source<Order>().Include(e => e.Customer).Include(e => e.Items).Find(command.Id);

            if (order.Status == OrderStatus.Confirmed)
            {
                foreach (var item in order.Items)
                {
                    var product = transaction.Source<Product>().Include(e => e.Stocks).Include(e => e.Stock).Find(item.ProductId);

                    var remain = 0M;
                    var trans = new List<StockTransaction>();

                    if (product.Release(item.Quantity, product.Stock.BinCode, out remain, out trans))
                    {
                        transaction.UpdateRange(product.Stocks);
                        transaction.AddRange(trans);
                    }
                    else
                    {
                        transaction.Rollback();
                        return Result.Fail(message: Localization.Get(Resources.Key.Command.ProductStockReleaseFail).ToFormat(product.Name));
                    }
                }
            }

            order.Status = OrderStatus.Cancelled;

            transaction.Update(order);
            transaction.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.OrderConfirmed));
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
