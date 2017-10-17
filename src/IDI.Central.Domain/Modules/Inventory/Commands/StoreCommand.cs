using System;
using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.Inventory.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Verification.Attributes;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.BasicInfo.Commands
{
    public class StoreCommand : Command
    {
        public Guid Id { get; set; }

        [RequiredField(Group = VerificationGroup.Create | VerificationGroup.Update)]
        [StringLength(MaxLength = 50, Group = VerificationGroup.Create | VerificationGroup.Update)]
        public string Name { get; set; }

        public bool Active { get; set; }
    }

    public class StoreCommandHandler : CRUDCommandHandler<StoreCommand>
    {
        [Injection]
        public IRepository<Store> Stores { get; set; }

        protected override Result Create(StoreCommand command)
        {
            if (this.Stores.Exist(e => e.Name == command.Name))
                return Result.Fail(Localization.Get(Resources.Key.Command.RecordDuplicated));

            var item = new Store
            {
                Name = command.Name.TrimContiguousSpaces(),
                Active = command.Active
            };

            this.Stores.Add(item);
            this.Stores.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.CreateSuccess));
        }

        protected override Result Update(StoreCommand command)
        {
            var item = this.Stores.Find(e => e.Id == command.Id);

            if (item == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.RecordNotExisting));

            item.Name = command.Name.TrimContiguousSpaces();
            item.Active = command.Active;

            this.Stores.Update(item);
            this.Stores.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.UpdateSuccess));
        }

        protected override Result Delete(StoreCommand command)
        {
            var item = this.Stores.Include(e=>e.Stocks).Find(e => e.Id == command.Id);

            if (item == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.RecordNotExisting));

            if(item.Stocks.Count>0)
                return Result.Fail(message: Localization.Get(Resources.Key.Command.OperationLimited));

            this.Stores.Remove(item);
            this.Stores.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.DeleteSuccess));
        }
    }
}
