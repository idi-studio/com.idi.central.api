using System;
using System.Linq;
using IDI.Central.Common.Enums;
using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.BasicInfo.AggregateRoots;
using IDI.Central.Domain.Modules.Inventory.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Verification.Attributes;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.BasicInfo.Commands
{
    public class ProductCommand : Command
    {
        public Guid Id { get; set; }

        [RequiredField(Group = VerificationGroup.Create | VerificationGroup.Update)]
        [StringLength(MaxLength = 50, Group = VerificationGroup.Create | VerificationGroup.Update)]
        public string Name { get; set; }

        [RequiredField(Group = VerificationGroup.Create | VerificationGroup.Update)]
        public string Tags { get; set; }

        public bool Enabled { get; set; }

        public bool OnShelf { get; set; }

        public Guid StroeId { get; set; }

        [DecimalRange(Minimum = 0.01, Maximum = int.MaxValue, Group = VerificationGroup.Create | VerificationGroup.Update)]
        public decimal SKU { get; set; }

        [DecimalRange(Minimum = 0, Maximum = int.MaxValue, Group = VerificationGroup.Create | VerificationGroup.Update)]
        public decimal SafetyStock { get; set; }

        [RequiredField(Group = VerificationGroup.Create | VerificationGroup.Update)]
        [StringLength(MaxLength = 10, Group = VerificationGroup.Create | VerificationGroup.Update)]
        public string Uint { get; set; }

        [RequiredField(Group = VerificationGroup.Create | VerificationGroup.Update)]
        [StringLength(MaxLength = 10, Group = VerificationGroup.Create | VerificationGroup.Update)]
        public string BinCode { get; set; }
    }

    public class ProductCommandHandler : CRUDCommandHandler<ProductCommand>
    {
        [Injection]
        public IRepository<Product> Products { get; set; }

        [Injection]
        public IRepository<Store> Stores { get; set; }

        protected override Result Create(ProductCommand command)
        {
            var name = command.Name.TrimContiguousSpaces();

            if (this.Products.Exist(e => e.Name == name))
                return Result.Fail(Localization.Get(Resources.Key.Command.RecordDuplicated));

            if (!this.Stores.Exist(e => e.Id == command.StroeId))
                return Result.Fail(Localization.Get(Resources.Key.Command.InvalidStore));

            var product = new Product
            {
                Name = name,
                QRCode = Guid.NewGuid().AsCode(),
                Tags = command.Tags,
                Enabled = command.Enabled,
                OnShelf = false
            };

            product.Stock = new ProductStock
            {
                ProductId = product.Id,
                StoreId = command.StroeId,
                SafetyStock = command.SafetyStock,
                SKU = command.SKU,
                BinCode = command.BinCode,
                Unit = command.Uint
            };

            this.Products.Add(product);
            this.Products.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.CreateSuccess));
        }

        protected override Result Update(ProductCommand command)
        {
            var name = command.Name.TrimContiguousSpaces();

            if (this.Products.Exist(e => e.Name == name))
                return Result.Fail(Localization.Get(Resources.Key.Command.RecordDuplicated));

            if (!this.Stores.Exist(e => e.Id == command.StroeId))
                return Result.Fail(Localization.Get(Resources.Key.Command.InvalidStore));

            var product = this.Products.Include(e => e.Stock).Include(p => p.Prices).Find(command.Id);

            if (product == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.ProductNotExisting));

            if (command.OnShelf && !product.Prices.Any(p => p.Category == PriceCategory.Selling && p.Enabled))
                return Result.Fail(Localization.Get(Resources.Key.Command.RequiredSellingPrice));

            product.Name = name;
            product.Tags = command.Tags;
            product.Enabled = command.Enabled;
            product.OnShelf = command.OnShelf;
            product.Stock.StoreId = command.StroeId;
            product.Stock.SafetyStock = command.SafetyStock;
            product.Stock.SKU = command.SKU;
            product.Stock.BinCode = command.BinCode;
            product.Stock.Unit = command.Uint;

            this.Products.Update(product);
            this.Products.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.UpdateSuccess));
        }

        protected override Result Delete(ProductCommand command)
        {
            var product = this.Products.Find(command.Id);

            if (product == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.ProductNotExisting));

            this.Products.Remove(product);
            this.Products.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.DeleteSuccess));
        }
    }
}
