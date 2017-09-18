using System.Linq;
using IDI.Central.Domain.Modules.Sales.Commands;
using IDI.Central.Domain.Modules.Sales.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Infrastructure;
using IDI.Core.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IDI.Central.Domain.Localization;

namespace IDI.Central.Domain.Tests
{
    //[TestClass]
    //[TestCategory("Sales")]
    //public partial class SalesUnitTests : IntegrationTests
    //{
    //    public SalesUnitTests()
    //    {
    //        reset = false;
    //    }

    //    [TestMethod]
    //    public void Sales_DataInitCommand()
    //    {
    //        var hanlder = new SalesInitalCommandHandler();
    //        hanlder.Localization = new Common.Localization();
    //        hanlder.Products = ServiceLocator.GetService<IRepository<Product>>();
    //        hanlder.Customers = ServiceLocator.GetService<IRepository<Customer>>();

    //        var result = hanlder.Execute(new SalesInitalCommand());

    //        Assert.AreEqual(ResultStatus.Success, result.Status);
    //        Assert.AreEqual(Resources.Key.Command.SysDataInitSuccess, result.Message);
    //    }
    //}
}
