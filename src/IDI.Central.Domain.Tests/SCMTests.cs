using System.Collections.Generic;
using IDI.Central.Domain.Common;
using IDI.Central.Domain.Modules.Identity.AggregateRoots;
using IDI.Central.Domain.Modules.Identity.Queries;
using IDI.Core.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IDI.Central.Domain.Tests
{
    [TestClass]
    public partial class IdentityTests : IntegrationTests
    {
        [TestMethod]
        public void Identity_Extension_Menu_IsAuthorized()
        {
            var menu = new Menu { ModuleId = Utils.NewGuid(1), Code = "1" };

            Assert.IsFalse(menu.IsAuthorized(new List<Privilege>()));
            Assert.IsFalse(menu.IsAuthorized(null));
            Assert.IsFalse(menu.IsAuthorized(new List<Privilege> { new Privilege { ModuleId = Utils.NewGuid(1), Code = "0" } }));
            Assert.IsFalse(menu.IsAuthorized(new List<Privilege> { new Privilege { ModuleId = Utils.NewGuid(2), Code = "1" } }));
            Assert.IsTrue(menu.IsAuthorized(new List<Privilege> { new Privilege { ModuleId = Utils.NewGuid(1), Code = "1", PrivilegeType = PrivilegeType.View } }));
        }
    }
}
