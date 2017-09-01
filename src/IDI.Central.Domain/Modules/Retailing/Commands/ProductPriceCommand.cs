using System;
using System.Linq;
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

        public int GradeFrom { get; set; }

        public int GradeTo { get; set; }

        public DateTime PeriodStart { get; set; }

        public DateTime PeriodEnd { get; set; }

        public Guid ProductId { get; set; }

        public bool Enabled { get; set; }
    }

    public class ProductPriceCommandHandler : CommandHandler<ProductPriceCommand>
    {
        private readonly DateTime min = new DateTime(2010, 1, 1);
        private readonly DateTime max = new DateTime(2030, 12, 31);

        [Injection]
        public IRepository<ProductPrice> Prices { get; set; }

        protected override Result Create(ProductPriceCommand command)
        {
            if (HasConflict(command))
                return Result.Fail(Localization.Get(Resources.Key.Command.TimeConflict));

            if (IsDuplicated(command))
                return Result.Fail(Localization.Get(Resources.Key.Command.RecordDuplicated));

            if (!HasTerm(command.Category))
            {
                command.PeriodStart = this.min;
                command.PeriodEnd = this.max;
            }

            var price = new ProductPrice
            {
                ProductId = command.ProductId,
                Category = command.Category,
                PeriodStart = command.PeriodStart.Date,
                PeriodEnd = command.PeriodEnd.Date.AddTicks(new TimeSpan(23, 59, 59).Ticks),
                Amount = command.Amount,
                GradeFrom = command.GradeFrom,
                GradeTo = command.GradeTo,
                Enabled = command.Enabled,
            };

            this.Prices.Add(price);
            this.Prices.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.CreateSuccess));
        }

        protected override Result Update(ProductPriceCommand command)
        {
            if (HasConflict(command))
                return Result.Fail(Localization.Get(Resources.Key.Command.TimeConflict));

            if (IsDuplicated(command))
                return Result.Fail(Localization.Get(Resources.Key.Command.RecordDuplicated));

            var price = this.Prices.Find(command.Id);

            if (price == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.RecordNotExisting));

            if (!HasTerm(command.Category))
            {
                command.PeriodStart = this.min;
                command.PeriodEnd = this.max;
            }

            price.Category = command.Category;
            price.PeriodStart = command.PeriodStart.Date;
            price.PeriodEnd = command.PeriodEnd.Date.AddTicks(new TimeSpan(23, 59, 59).Ticks);
            price.Amount = command.Amount;
            price.GradeFrom = command.GradeFrom;
            price.GradeTo = command.GradeTo;
            price.Enabled = command.Enabled;

            this.Prices.Update(price);
            this.Prices.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.UpdateSuccess));
        }

        protected override Result Delete(ProductPriceCommand command)
        {
            var price = this.Prices.Find(command.Id);

            if (price == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.RecordNotExisting));

            this.Prices.Remove(price);
            this.Prices.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.DeleteSuccess));
        }

        private bool HasConflict(ProductPriceCommand command)
        {
            if (!HasTerm(command.Category))
                return false;

            var prices = this.Prices.Get(e => e.ProductId == command.ProductId && e.Category == command.Category && e.Id != command.Id);

            if (prices.Count == 0)
                return false;

            //（S2 <= E1）AND (S1 <= E2）
            prices = prices.Where(price => price.PeriodStart <= command.PeriodEnd && command.PeriodStart <= price.PeriodEnd).ToList();
            prices = prices.Where(price => price.GradeFrom <= command.GradeTo && command.GradeFrom <= price.GradeTo).ToList();

            return prices.Count > 0;
        }

        private bool IsDuplicated(ProductPriceCommand command)
        {
            if (HasTerm(command.Category))
                return false;

            return this.Prices.Exist(e => e.ProductId == command.ProductId && e.Category == command.Category && e.Id != command.Id);
        }

        private bool HasTerm(PriceCategory category)
        {
            switch (category)
            {
                case PriceCategory.Discount:
                    return true;
                default:
                    return false;
            }
        }
    }
}
