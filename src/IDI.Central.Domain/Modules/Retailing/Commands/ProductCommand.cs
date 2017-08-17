using System;
using System.Linq;
using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.Retailing.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Verification.Attributes;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Retailing.Commands
{
    public class ProductCommand : Command
    {
        public Guid Id { get; set; }

        [RequiredField(Resources.Key.DisplayName.ProductName, Group = Constants.VerificationGroup.CreateOrUpdate)]
        [StringLength(Resources.Key.DisplayName.ProductName, MaxLength = 50, Group = Constants.VerificationGroup.CreateOrUpdate)]
        public string Name { get; set; }

        [RequiredField(Resources.Key.DisplayName.ProductCode, Group = Constants.VerificationGroup.CreateOrUpdate)]
        [StringLength(Resources.Key.DisplayName.ProductCode, MaxLength = 50, Group = Constants.VerificationGroup.CreateOrUpdate)]
        public string Code { get; set; }

        [RequiredField(Resources.Key.DisplayName.ProductTags, Group = Constants.VerificationGroup.CreateOrUpdate)]
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
            if (this.Products.Exist(e => e.Code == command.Code))
                return Result.Fail(Localization.Get(Resources.Key.Command.ProductCodeDuplicated));

            var product = new Product
            {
                Name = command.Name.TrimContiguousSpaces(),
                Code = command.Code,
                Tags = command.Tags,
                Enabled = command.Enabled,
                OnShelf = false
            };

            this.Products.Add(product);
            this.Products.Context.Commit();
            this.Products.Context.Dispose();

            return Result.Success(message: Localization.Get(Resources.Key.Command.CreateSuccess));
        }

        protected override Result Update(ProductCommand command)
        {
            if (this.Products.Exist(e => e.Code == command.Code && e.Id != command.Id))
                return Result.Fail(Localization.Get(Resources.Key.Command.ProductCodeDuplicated));

            var product = this.Products.Find(command.Id, p => p.Prices);

            if (product == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.ProductNotExisting));

            if (command.OnShelf && product.Prices.Any(p => p.Category == Central.Common.PriceCategory.Selling))
                return Result.Fail(Localization.Get(Resources.Key.Command.RequiredSellingPrice));

            product.Name = command.Name.TrimContiguousSpaces();
            product.Code = command.Code;
            product.Tags = command.Tags;
            product.Enabled = command.Enabled;
            product.OnShelf = command.OnShelf;

            this.Products.Update(product);
            this.Products.Context.Commit();
            this.Products.Context.Dispose();

            return Result.Success(message: Localization.Get(Resources.Key.Command.UpdateSuccess));
        }

        protected override Result Delete(ProductCommand command)
        {
            var product = this.Products.Find(command.Id);

            if (product == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.ProductNotExisting));

            this.Products.Remove(product);
            this.Products.Context.Commit();
            this.Products.Context.Dispose();

            return Result.Success(message: Localization.Get(Resources.Key.Command.DeleteSuccess));
        }
    }
}
