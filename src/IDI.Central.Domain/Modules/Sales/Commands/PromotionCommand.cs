using System;
using IDI.Central.Common.JsonTypes;
using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.BasicInfo.AggregateRoots;
using IDI.Central.Domain.Modules.Sales.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Common.Extensions;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.Verification.Attributes;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Sales.Commands
{
    public class PromotionCommand : Command
    {
        public Guid Id { get; set; }

        [StringLength(MaxLength = 30, Group = ValidationGroup.Create | ValidationGroup.Update)]
        public string Subject { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public PromotionPrice Price { get; set; } = new PromotionPrice();

        public bool Enabled { get; set; }

        public Guid ProductId { get; set; }
    }

    public class PromotionCommandHandler : CRUDTransactionCommandHandler<PromotionCommand>
    {
        protected override Result Create(PromotionCommand command, ITransaction transaction)
        {
            if (!transaction.Source<Product>().Exist(e => e.Id == command.ProductId))
                return Result.Fail(Localization.Get(Resources.Key.Command.ProductNotExisting));

            var promotion = new Promotion
            {
                Subject = command.Subject,
                Price = command.Price.ToJson(),
                ProductId = command.ProductId,
                StartTime = command.StartTime,
                EndTime = command.EndTime,
                Enabled = command.Enabled
            };

            transaction.Add(promotion);
            transaction.Commit();

            return Result.Success(Localization.Get(Resources.Key.Command.CreateSuccess));
        }

        protected override Result Update(PromotionCommand command, ITransaction transaction)
        {
            if (!transaction.Source<Product>().Exist(e => e.Id == command.ProductId))
                return Result.Fail(Localization.Get(Resources.Key.Command.ProductNotExisting));

            var promotion = transaction.Source<Promotion>().Find(command.Id);

            if (promotion == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.RecordNotExisting));

            promotion.Subject = command.Subject;
            promotion.Price = command.Price.ToJson();
            promotion.ProductId = command.ProductId;
            promotion.StartTime = command.StartTime;
            promotion.EndTime = command.EndTime;
            promotion.Enabled = command.Enabled;

            transaction.Update(promotion);
            transaction.Commit();

            return Result.Success(Localization.Get(Resources.Key.Command.UpdateSuccess));
        }

        protected override Result Delete(PromotionCommand command, ITransaction transaction)
        {
            var promotion = transaction.Source<Promotion>().Find(command.Id);

            if (promotion == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.RecordNotExisting));

            transaction.Remove(promotion);
            transaction.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.DeleteSuccess));
        }
    }
}
