using System.Linq;
using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Central.Domain.Modules.Administration.Queries;
using IDI.Core.Common;
using IDI.Core.Infrastructure;
using IDI.Core.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IDI.Central.Domain.Tests
{
    public partial class AdministrationUnitTests : IntegrationTests
    {
        [TestMethod]
        public void Administration_SidebarQuery()
        {
            Administration_DatabaseInitalCommand();

            var query = new QuerySidebar();
            query.Menus = ServiceLocator.GetService<IQueryableRepository<Menu>>();
            query.RolePrivileges = ServiceLocator.GetService<IQueryableRepository<RolePrivilege>>();
            query.Users = ServiceLocator.GetService<IQueryableRepository<User>>();

            var result = query.Execute(new QuerySidebarCondition { UserName = "administrator" });

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
