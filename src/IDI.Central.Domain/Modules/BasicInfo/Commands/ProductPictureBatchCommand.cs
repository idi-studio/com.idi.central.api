using System;
using System.Collections.Generic;
using System.Linq;
using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.BasicInfo.AggregateRoots;
using IDI.Central.Models.BasicInfo;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.BasicInfo.Commands
{
    public class ProductPictureBatchCommand : Command
    {
        public Guid ProductId { get; set; }

        public List<ProductPictureInput> Pictures { get; set; }
    }

    public class ProductPictureBatchCommandHandler : CommandHandler<ProductPictureBatchCommand>
    {
        [Injection]
        public IRepository<Product> Products { get; set; }

        [Injection]
        public IRepository<ProductPicture> Pictures { get; set; }

        protected override Result Create(ProductPictureBatchCommand command)
        {
            return Result.Fail(Localization.Get(Resources.Key.Command.OperationNonsupport));
        }

        protected override Result Delete(ProductPictureBatchCommand command)
        {
            return Result.Fail(Localization.Get(Resources.Key.Command.OperationNonsupport));
        }

        protected override Result Update(ProductPictureBatchCommand command)
        {
            var product = this.Products.Include(e => e.Pictures).Find(command.ProductId);

            if (product == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.ProductNotExisting));

            foreach (var item in command.Pictures)
            {
                var picture = product.Pictures.FirstOrDefault(e => e.Id == item.Id);

                if (picture == null)
                    continue;

                picture.Sequence = item.Sequence;
                picture.Category = item.Category;

                this.Pictures.Update(picture);
            }

            this.Pictures.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.UpdateSuccess));
        }
    }
}
