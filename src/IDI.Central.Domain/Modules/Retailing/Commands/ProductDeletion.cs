using System;
using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.Retailing.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Localization;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Retailing.Commands
{
    public class ProductDeletionCommand : Command
    {
        public Guid Id { get; set; }
    }

    public class ProductDeletionCommandHandler : ICommandHandler<ProductDeletionCommand>
    {
        [Injection]
        public ILocalization Localization { get; set; }

        [Injection]
        public IRepository<Product> Products { get; set; }

        public Result Execute(ProductDeletionCommand command)
        {
            var product = this.Products.Find(command.Id);

            if (product==null)
                return Result.Fail(Localization.Get(Resources.Key.Command.ProductNotExisting));

            this.Products.Remove(product);
            this.Products.Context.Commit();
            this.Products.Context.Dispose();

            return Result.Success(message: Localization.Get(Resources.Key.Command.DeletionSuccess));
        }
    }
}
