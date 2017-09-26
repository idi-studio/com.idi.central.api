using IDI.Central.Models.Administration;
using IDI.Central.Tests.Utils;
using IDI.Core.Common;
using IDI.Core.Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IDI.Central.Tests
{
    [TestClass]
    [TestCategory("api/users")]
    public class AccountUnitTests : IntegrationTesting
    {
        [TestInitialize]
        public void Setup()
        {
            SignIn();
        }

        [TestMethod]
        public void Should_Registration_Success()
        {
            var input = new UserRegistrationInput();

            var json = Post("api/users", input);

            var result= json.To<Result>();

            Assert.IsNotNull(result);
            Assert.AreEqual(ResultStatus.Success, result.Status);
        }
    }
}
