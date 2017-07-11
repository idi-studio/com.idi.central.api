using System.Linq;
using IDI.Central.Domain.Modules.Identity.AggregateRoots;
using IDI.Central.Domain.Modules.Identity.Conditions;
using IDI.Central.Domain.Modules.Identity.Queries;
using IDI.Core.Common;
using IDI.Core.Infrastructure;
using IDI.Core.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IDI.Central.Domain.Tests
{
    public partial class IdentityTests : IntegrationTests
    {
        [TestMethod]
        public void Identity_Query_Sidebar()
        {
            Identity_Command_Initialize();

            var query = new SidebarQuery();
            query.MenuRepository = ServiceLocator.GetService<IQueryRepository<Menu>>();
            query.RolePrivilegeRepository = ServiceLocator.GetService<IQueryRepository<RolePrivilege>>();
            query.UserRepository = ServiceLocator.GetService<IQueryRepository<User>>();

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
