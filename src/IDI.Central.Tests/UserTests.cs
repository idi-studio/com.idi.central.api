using IDI.Central.Models.Administration;
using IDI.Central.Tests.Utils;
using IDI.Core.Common;
using IDI.Core.Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IDI.Central.Tests
{
    [TestClass]
    [TestCategory("api/user")]
    public class AccountUnitTests : IntegrationTest
    {
        [TestInitialize]
        public void Setup()
        {
            SignIn();
        }

        [TestMethod]
        public void User_Can_Registration()
        {
            var input = new UserRegistrationInput();

            var json = Post("api/user", input);

            var result= json.To<Result>();

            Assert.IsNotNull(result);
            Assert.AreEqual(ResultStatus.Success, result.Status);
        }
    }
}
