using System;
using IDI.Central.Common;
using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.Retailing.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Retailing.Commands
{
    public class ProductPriceCommand : Command
    {
        public Guid Id { get; set; }

        public PriceCategory Category { get; set; }

        public decimal Amount { get; set; }

        public int Grade { get; set; } = 0;

        public DateTime StartDate { get; set; }

        public DateTime DueDate { get; set; }

        public Guid ProductId { get; set; }

        public bool Enabled { get; set; }
    }

    public class ProductPriceCommandHandler : CommandHandler<ProductPriceCommand>
    {
        [Injection]
        public IRepository<ProductPrice> Prices { get; set; }

        protected override Result Create(ProductPriceCommand command)
        {
            if (HasConflict(command))
                return Result.Fail(Localization.Get(Resources.Key.Command.TimeConflict));

            var price = new ProductPrice
            {
                ProductId = command.ProductId,
                Category = command.Category,
                StartDate = command.StartDate,
                DueDate = command.DueDate,
                Amount = command.Amount,
                Grade = command.Grade,
                Enabled = command.Enabled,
            };

            this.Prices.Add(price);
            this.Prices.Context.Commit();
            this.Prices.Context.Dispose();

            return Result.Success(message: Localization.Get(Resources.Key.Command.CreateSuccess));
        }

        protected override Result Update(ProductPriceCommand command)
        {
            if (HasConflict(command))
                return Result.Fail(Localization.Get(Resources.Key.Command.TimeConflict));

            var price = this.Prices.Find(command.Id);

            if (price == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.RecordNotExisting));

            price.Category = command.Category;
            price.StartDate = command.StartDate;
            price.DueDate = command.DueDate;
            price.Amount = command.Amount;
            price.Grade = command.Grade;
            price.Enabled = command.Enabled;

            this.Prices.Update(price);
            this.Prices.Context.Commit();
            this.Prices.Context.Dispose();

            return Result.Success(message: Localization.Get(Resources.Key.Command.UpdateSuccess));
        }

        protected override Result Delete(ProductPriceCommand command)
        {
            var price = this.Prices.Find(command.Id);

            if (price == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.RecordNotExisting));

            this.Prices.Remove(price);
            this.Prices.Context.Commit();
            this.Prices.Context.Dispose();

            return Result.Success(message: Localization.Get(Resources.Key.Command.DeleteSuccess));
        }

        private bool HasConflict(ProductPriceCommand command)
        {
            var price = this.Prices.Find(e => e.ProductId == command.ProductId && e.Category == command.Category && e.Id != command.Id);

            if (price == null)
                return false;

            //（S2 <= E1）AND (S1 <= E2）
            if (price.StartDate <= command.DueDate && command.StartDate <= price.DueDate)
                return true;

            return false;
        }
    }
}
