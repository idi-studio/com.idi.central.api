using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Central.Domain.Modules.Administration.Commands;
using IDI.Central.Domain.Modules.BasicInfo.AggregateRoots;
using IDI.Central.Domain.Modules.BasicInfo.Commands;
using IDI.Central.Domain.Modules.Sales.AggregateRoots;
using IDI.Central.Domain.Modules.Sales.Commands;
using IDI.Core.Common;
using IDI.Core.Infrastructure;
using IDI.Core.Localization;
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
            Runtime.UseAuthorization<ApplicationAuthorization>();

            Assert.AreEqual(ResultStatus.Success, DatabaseInital().Status);
            Assert.AreEqual(ResultStatus.Success, BasicInfoInital().Status);
            Assert.AreEqual(ResultStatus.Success, SalesInital().Status);
        }

        private Result DatabaseInital()
        {
            var hanlder = new DatabaseInitalCommandHandler();
            hanlder.Localization = new Globalization();
            hanlder.Users = Runtime.GetService<IRepository<User>>();
            hanlder.Roles = Runtime.GetService<IRepository<Role>>();
            hanlder.Permissions = Runtime.GetService<IRepository<Permission>>();
            //hanlder.Modules = Runtime.GetService<IRepository<Module>>();
            hanlder.Clients = Runtime.GetService<IRepository<Client>>();

            return hanlder.Execute(new DatabaseInitalCommand());
        }

        private Result SalesInital()
        {
            var hanlder = new SalesInitalCommandHandler();
            hanlder.Localization = new Globalization();
            hanlder.Customers = Runtime.GetService<IRepository<Customer>>();

            return hanlder.Execute(new SalesInitalCommand());
        }

        private Result BasicInfoInital()
        {
            var hanlder = new BasicInfoInitialCommandHandler();
            hanlder.Localization = new Globalization();
            hanlder.Products = Runtime.GetService<IRepository<Product>>();

            return hanlder.Execute(new BasicInfoInitialCommand());
        }

    }
}
