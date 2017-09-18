using System;
using System.Linq;
using IDI.Central.Common.Enums;
using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.Material.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Verification.Attributes;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Material.Commands
{
    public class ProductCommand : Command
    {
        public Guid Id { get; set; }

        [RequiredField(Group = VerificationGroup.Create | VerificationGroup.Update)]
        [StringLength(MaxLength = 50, Group = VerificationGroup.Create | VerificationGroup.Update)]
        public string Name { get; set; }

        [RequiredField(Group = VerificationGroup.Create | VerificationGroup.Update)]
        [StringLength(MaxLength = 50, Group = VerificationGroup.Create | VerificationGroup.Update)]
        public string QRCode { get; set; }

        [RequiredField(Group = VerificationGroup.Create | VerificationGroup.Update)]
        public string Tags { get; set; }

        public bool Enabled { get; set; }

        public bool OnShelf { get; set; }
    }

    public class ProductCommandHandler : CommandHandler<ProductCommand>
    {
        [Injection]
        public IRepository<Product> Products { get; set; }

        protected override Result Create(ProductCommand command)
        {
            if (this.Products.Exist(e => e.QRCode == command.QRCode))
                return Result.Fail(Localization.Get(Resources.Key.Command.ProductCodeDuplicated));

            var product = new Product
            {
                Name = command.Name.TrimContiguousSpaces(),
                QRCode = command.QRCode,
                Tags = command.Tags,
                Enabled = command.Enabled,
                OnShelf = false
            };

            this.Products.Add(product);
            this.Products.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.CreateSuccess));
        }

        protected override Result Update(ProductCommand command)
        {
            if (this.Products.Exist(e => e.QRCode == command.QRCode && e.Id != command.Id))
                return Result.Fail(Localization.Get(Resources.Key.Command.ProductCodeDuplicated));

            var product = this.Products.Include(p => p.Prices).Find(command.Id);

            if (product == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.ProductNotExisting));

            if (command.OnShelf && !product.Prices.Any(p => p.Category == PriceCategory.Selling && p.Enabled))
                return Result.Fail(Localization.Get(Resources.Key.Command.RequiredSellingPrice));

            product.Name = command.Name.TrimContiguousSpaces();
            product.QRCode = command.QRCode;
            product.Tags = command.Tags;
            product.Enabled = command.Enabled;
            product.OnShelf = command.OnShelf;

            this.Products.Update(product);
            this.Products.Commit();
            //this.Products.Context.Dispose();

            return Result.Success(message: Localization.Get(Resources.Key.Command.UpdateSuccess));
        }

        protected override Result Delete(ProductCommand command)
        {
            var product = this.Products.Find(command.Id);

            if (product == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.ProductNotExisting));

            this.Products.Remove(product);
            this.Products.Commit();
            //this.Products.Context.Dispose();

            return Result.Success(message: Localization.Get(Resources.Key.Command.DeleteSuccess));
        }
    }
}
