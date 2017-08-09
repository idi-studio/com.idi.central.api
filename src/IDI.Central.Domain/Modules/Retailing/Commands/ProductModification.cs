using System;
using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.Retailing.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Verification.Attributes;
using IDI.Core.Localization;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Retailing.Commands
{
    public class ProductModificationCommand : Command
    {
        public Guid Id { get; set; }

        [RequiredField(Resources.Key.DisplayName.ProductName)]
        [StringLength(Resources.Key.DisplayName.ProductName, MaxLength = 50)]
        public string Name { get; set; }

        [RequiredField(Resources.Key.DisplayName.ProductCode)]
        [StringLength(Resources.Key.DisplayName.ProductCode, MaxLength = 50)]
        public string Code { get; set; }

        [RequiredField(Resources.Key.DisplayName.ProductTags)]
        public string Tags { get; set; }

        public bool Enabled { get; set; }
    }

    public class ProductModificationCommandHandler : ICommandHandler<ProductModificationCommand>
    {
        [Injection]
        public ILocalization Localization { get; set; }

        [Injection]
        public IRepository<Product> Products { get; set; }

        public Result Execute(ProductModificationCommand command)
        {
            if (this.Products.Exist(e => e.Code == command.Code && e.Id != command.Id))
                return Result.Fail(Localization.Get(Resources.Key.Command.ProductCodeDuplicated));

            var product = this.Products.Find(command.Id);

            if (product == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.ProductNotExisting));

            product.Name = command.Name;
            product.Code = command.Code;
            product.Tags = command.Tags;
            product.Enabled = command.Enabled;

            this.Products.Update(product);
            this.Products.Context.Commit();
            this.Products.Context.Dispose();

            return Result.Success(message: Localization.Get(Resources.Key.Command.ModificationSuccess));
        }
    }
}
