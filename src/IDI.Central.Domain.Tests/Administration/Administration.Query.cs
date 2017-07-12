using System.Linq;
using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Central.Domain.Modules.Administration.Queries;
using IDI.Core.Common;
using IDI.Core.Infrastructure;
using IDI.Core.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IDI.Central.Domain.Tests
{
    public partial class AdministrationTests : IntegrationTests
    {
        [TestMethod]
        public void Administration_SidebarQuery()
        {
            Administration_DataInitializationCommand();

            var query = new SidebarQuery();
            query.Menus = ServiceLocator.GetService<IQueryRepository<Menu>>();
            query.RolePrivilegeRepository = ServiceLocator.GetService<IQueryRepository<RolePrivilege>>();
            query.Users = ServiceLocator.GetService<IQueryRepository<User>>();

            var result = query.Execute(new SidebarQueryCondition { UserName = "administrator" });

            Assert.AreEqual(ResultStatus.Success, result.Status);
            Assert.IsNotNull(result.Data);

            var sidebar = result.Data;

            Assert.IsNotNull(sidebar.Profile);
            Assert.AreEqual("administrator", sidebar.Profile.Name);
            Assert.AreEqual("default.jpg", sidebar.Profile.Photo);
            Assert.AreEqual(1, sidebar.Menus.Count);
            Assert.AreEqual(5, sidebar.Menus.SelectMany(m => m.Subs).Count());
        }
    }
}
