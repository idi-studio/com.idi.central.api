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

namespace IDI.Central.Domain.Modules.Logistics.Commands
{
    public class DeliverCommand : Command
    {
        public Guid Id { get; set; }

        [RequiredField(Group = VerificationGroup.Create | VerificationGroup.Update)]
        [StringLength(MaxLength = 50, Group = VerificationGroup.Create | VerificationGroup.Update)]
        public string CourierNo { get; set; }

        public DeliverStatus Status { get; set; }

        [RequiredField(Group = VerificationGroup.Create | VerificationGroup.Update)]
        public string Details { get; set; }

        public Guid OrderId { get; set; }
    }

    public class DeliverCommandHandler : CommandHandler<DeliverCommand>
    {
        [Injection]
        public IRepository<Order> Orders { get; set; }

        [Injection]
        public IRepository<Deliver> Delivers { get; set; }

        protected override Result Create(DeliverCommand command)
        {
            if (this.Orders.Exist(e => e.Id == command.OrderId))
                return Result.Fail(Localization.Get(Resources.Key.Command.InvalidOrder));

            var deliver = new Deliver
            {
                CourierNo = command.CourierNo,
                Status = DeliverStatus.TakingExpress,
                Details = command.Details,
                OrderId = command.OrderId
            };

            this.Delivers.Add(deliver);
            this.Delivers.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.CreateSuccess));
        }

        protected override Result Update(DeliverCommand command)
        {
            if (this.Orders.Exist(e => e.Id == command.OrderId))
                return Result.Fail(Localization.Get(Resources.Key.Command.InvalidOrder));

            var deliver = this.Delivers.Find(command.Id);

            if (deliver == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.RecordNotExisting));

            deliver.Details = command.Details;
            deliver.Status = command.Status;

            this.Delivers.Update(deliver);
            this.Delivers.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.UpdateSuccess));
        }

        protected override Result Delete(DeliverCommand command)
        {
            var deliver = this.Delivers.Find(command.Id);

            if (deliver == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.RecordNotExisting));

            this.Delivers.Remove(deliver);
            this.Delivers.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.DeleteSuccess));
        }
    }
}
