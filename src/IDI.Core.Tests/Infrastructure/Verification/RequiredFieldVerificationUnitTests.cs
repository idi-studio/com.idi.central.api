using System.Collections.Generic;
using IDI.Core.Infrastructure.Verification;
using IDI.Core.Tests.Common;
using IDI.Core.Tests.Common.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IDI.Core.Tests.Infrastructure
{
    [TestClass]
    [TestCategory(Contants.TestCategory.Verification)]
    public class RequiredFieldVerificationUnitTests
    {
        [TestMethod]
        public void TestRequiredFieldVerification_Fail_ForEmptyValue()
        {
            var model = new TestObjectA { Field = string.Empty };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsFalse(result);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("'测试字段'必填!", errors[0]);
        }

        [TestMethod]
        public void TestRequiredFieldVerification_Fail_ForNullValue()
        {
            var model = new TestObjectA { Field = null };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsFalse(result);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("'测试字段'必填!", errors[0]);
        }

        [TestMethod]
        public void TestRequiredFieldVerification_Success_ForValue()
        {
            var model = new TestObjectA { Field = "test" };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsTrue(result);
            Assert.AreEqual(0, errors.Count);
        }
    }
}
