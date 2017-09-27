using IDI.Central.Domain;
using IDI.Core.Authentication;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IDI.Central.Tests
{
    [TestClass]
    [TestCategory("Authorization")]
    public class AuthorizationUnitTest
    {
        private IAuthorization authorization;

        [TestInitialize]
        public void Setup()
        {
            authorization = new ApplicationAuthorization();
        }

        [TestMethod]
        public void Should_HasPermissions_After_Initialization()
        {
            Assert.IsTrue(authorization.Permissions.Count > 0);
        }
    }
}
