using System.Linq;
using IDI.Central.Domain.Modules.Retailing.Commands;
using IDI.Central.Domain.Modules.Retailing.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Infrastructure;
using IDI.Core.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IDI.Central.Domain.Localization;

namespace IDI.Central.Domain.Tests
{
    [TestClass]
    [TestCategory("Retailing")]
    public partial class RetailingUnitTests : IntegrationTests
    {
        public RetailingUnitTests()
        {
            reset = false;
        }

        [TestMethod]
        public void Retailing_DataInitCommand()
        {
            var hanlder = new RetailingInitalCommandHandler();
            hanlder.Localization = new Common.Localization();
            hanlder.Products = ServiceLocator.GetService<IRepository<Product>>();
            hanlder.Customers = ServiceLocator.GetService<IRepository<Customer>>();

            var result = hanlder.Execute(new RetailingInitalCommand());

            Assert.AreEqual(ResultStatus.Success, result.Status);
            Assert.AreEqual(Resources.Key.Command.SysDataInitSuccess, result.Message);
        }
    }
}
