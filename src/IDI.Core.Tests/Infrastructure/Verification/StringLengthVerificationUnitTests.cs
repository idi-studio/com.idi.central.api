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
    public class StringLengthVerificationUnitTests
    {
        #region TestObjects
        public class TestObjectB : Command
        {
            [StringLength(DisplayName = "测试字段", MinLength = 5, MaxLength = 10)]
            public string Field { get; set; }
        }
        public class TestObjectC : Command
        {
            [StringLength(DisplayName = "测试字段", MaxLength = 10)]
            public string Field { get; set; }
        }
        public class TestObjectD : Command
        {
            [StringLength(DisplayName = "测试字段", MinLength = 5)]
            public string Field { get; set; }
        }
        #endregion

        [TestInitialize]
        public void Setup()
        {
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("zh-CN");
        }

        [TestMethod]
        public void It_Should_Not_Be_Pass_When_RangeLengthLimite_LessThanMinLength()
        {
            var model = new TestObjectB { Field = "1234" };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsFalse(result);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("'测试字段'最多5到10个字符!", errors[0]);
        }

        [TestMethod]
        public void It_Should_Not_Be_Pass_When_RangeLengthLimite_GreaterThanMaxLength()
        {
            var model = new TestObjectB { Field = "12345678901" };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsFalse(result);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("'测试字段'最多5到10个字符!", errors[0]);
        }

        [TestMethod]
        public void It_Should_Be_Pass_When_RangeLengthLimite_InRange()
        {
            var model = new TestObjectB { Field = "123456789" };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsTrue(result);
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void It_Should_Be_Pass_When_RangeLengthLimite_EqualToMinLength()
        {
            var model = new TestObjectB { Field = "12345" };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsTrue(result);
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void It_Should_Be_Pass_When_RangeLengthLimite_EqualToMaxLength()
        {
            var model = new TestObjectB { Field = "1234567890" };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsTrue(result);
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void It_Should_Be_Pass_When_MaxLengthLimite_NullValue()
        {
            var model = new TestObjectC { Field = null };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsTrue(result);
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void It_Should_Be_Pass_When_MaxLengthLimite_EmptyValue()
        {
            var model = new TestObjectC { Field = "" };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsTrue(result);
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void It_Should_Be_Pass_When_MaxLengthLimite_LessThanToMaxLength()
        {
            var model = new TestObjectC { Field = "123456789" };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsTrue(result);
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void It_Should_Be_Pass_When_MaxLengthLimite_EqualToMaxLength()
        {
            var model = new TestObjectC { Field = "1234567890" };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsTrue(result);
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void It_Should_Not_Be_Pass_When_MaxLengthLimite_GreaterThanMaxLength()
        {
            var model = new TestObjectC { Field = "12345678901" };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsFalse(result);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("'测试字段'最多10个字符!", errors[0]);
        }

        [TestMethod]
        public void It_Should_Be_Pass_When_MinLengthLimite_EqualToMinLength()
        {
            var model = new TestObjectD { Field = "12345" };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsTrue(result);
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void It_Should_Be_Pass_When_MinLengthLimite_GreaterThanMinLength()
        {
            var model = new TestObjectD { Field = "123456" };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsTrue(result);
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void It_Should_Not_Be_Pass_When_MinLengthLimite_LessThanMinLength()
        {
            var model = new TestObjectD { Field = "1234" };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsFalse(result);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("'测试字段'最少5个字符!", errors[0]);
        }

        [TestMethod]
        public void It_Should_Not_Be_Pass_When_MinLengthLimite_EmptyValue()
        {
            var model = new TestObjectD { Field = "" };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsFalse(result);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("'测试字段'最少5个字符!", errors[0]);
        }

        [TestMethod]
        public void It_Should_Not_Be_Pass_When_MinLengthLimite_NullValue()
        {
            var model = new TestObjectD { Field = null };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsFalse(result);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("'测试字段'最少5个字符!", errors[0]);
        }
    }
}
