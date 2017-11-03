using System.Collections.Generic;
using System.Globalization;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.Verification;
using IDI.Core.Infrastructure.Verification.Attributes;
using IDI.Core.Tests.TestUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IDI.Core.Tests.Infrastructure
{
    [TestClass]
    [TestCategory(Contants.TestCategory.Verification)]
    public class RequiredFieldVerificationUnitTests
    {
        #region TestObjects
        public class TestObjectA : Command
        {
            [RequiredField(DisplayName = "测试字段")]
            public string Field { get; set; }
        }
        #endregion

        [TestInitialize]
        public void Setup()
        {
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("zh-CN");
        }

        [TestMethod]
        public void It_Should_Not_Be_Pass_When_EmptyValue()
        {
            var model = new TestObjectA { Field = string.Empty };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsFalse(result);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("'测试字段'必填!", errors[0]);
        }

        [TestMethod]
        public void It_Should_Not_Be_Pass_When_NullValue()
        {
            var model = new TestObjectA { Field = null };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsFalse(result);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("'测试字段'必填!", errors[0]);
        }

        [TestMethod]
        public void It_Should_Be_Pass_When_HasValue()
        {
            var model = new TestObjectA { Field = "test" };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsTrue(result);
            Assert.AreEqual(0, errors.Count);
        }
    }
}
