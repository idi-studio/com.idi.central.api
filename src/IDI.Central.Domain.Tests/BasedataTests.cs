using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Central.Domain.Modules.Administration.Commands;
using IDI.Central.Domain.Modules.Material.AggregateRoots;
using IDI.Central.Domain.Modules.Material.Commands;
using IDI.Central.Domain.Modules.Retailing.AggregateRoots;
using IDI.Central.Domain.Modules.Retailing.Commands;
using IDI.Core.Common;
using IDI.Core.Infrastructure;
using IDI.Core.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IDI.Central.Domain.Tests
{
    [TestClass]
    [TestCategory("Basedata")]
    public class BasedataUnitTests : IntegrationTests
    {
        [TestMethod]
        public void BasedataInital()
        {
            Assert.AreEqual(ResultStatus.Success, DatabaseInital().Status);
            Assert.AreEqual(ResultStatus.Success, MaterialInital().Status);
            Assert.AreEqual(ResultStatus.Success, RetailingInital().Status);
        }

        private Result DatabaseInital()
        {
            var hanlder = new DatabaseInitalCommandHandler();
            hanlder.Localization = new Common.Localization();
            hanlder.Users = ServiceLocator.GetService<IRepository<User>>();
            hanlder.Roles = ServiceLocator.GetService<IRepository<Role>>();
            hanlder.Modules = ServiceLocator.GetService<IRepository<Module>>();
            hanlder.Clients = ServiceLocator.GetService<IRepository<Client>>();

            return hanlder.Execute(new DatabaseInitalCommand());
        }

        private Result RetailingInital()
        {
            var hanlder = new RetailingInitalCommandHandler();
            hanlder.Localization = new Common.Localization();
            hanlder.Customers = ServiceLocator.GetService<IRepository<Customer>>();

            return hanlder.Execute(new RetailingInitalCommand());
        }

        private Result MaterialInital()
        {
            var hanlder = new MaterialInitalCommandHandler();
            hanlder.Localization = new Common.Localization();
            hanlder.Products = ServiceLocator.GetService<IRepository<Product>>();

            return hanlder.Execute(new MaterialInitalCommand());
        }

    }
}
