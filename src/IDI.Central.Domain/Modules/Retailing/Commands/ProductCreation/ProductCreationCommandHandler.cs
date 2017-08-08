using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.Retailing.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Localization;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Retailing.Commands
{
    public class ProductCreationCommandHandler : ICommandHandler<ProductCreationCommand>
    {
        [Injection]
        public ILocalization Localization { get; set; }

        [Injection]
        public IRepository<Product> Products { get; set; }

        public Result Execute(ProductCreationCommand command)
        {
            if (this.Products.Exist(e => e.Code == command.Code))
                return Result.Fail(Localization.Get(Resources.Key.Command.ProductCodeDuplicated));

            var product = new Product
            {
                Name = command.Name,
                Code = command.Code,
                Tags = command.Tags,
                Enabled = false,
            };

            this.Products.Add(product);
            this.Products.Context.Commit();
            this.Products.Context.Dispose();

            return Result.Success(message: Localization.Get(Resources.Key.Command.CreationSuccess));
        }
    }
}
