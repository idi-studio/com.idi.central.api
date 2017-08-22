using IDI.Central.Models.Administration;
using IDI.Central.Tests.Utils;
using IDI.Core.Common;
using IDI.Core.Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IDI.Central.Tests
{
    [TestClass]
    [TestCategory("api/roles")]
    public class RolesTests : IntegrationTesting
    {
        [TestInitialize]
        public void Setup()
        {
            SignIn();
        }

        [TestMethod]
        public void Can_Get_Roles()
        {
            var json = Get("api/roles");

            var result = json.To<Result<Set<RoleModel>>>();

            Assert.IsNotNull(result);
            Assert.AreEqual(ResultStatus.Success, result.Status);
            Assert.IsNotNull(result.Data);
        }
    }
}
