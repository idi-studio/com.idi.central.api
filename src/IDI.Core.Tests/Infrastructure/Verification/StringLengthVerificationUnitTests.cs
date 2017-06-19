using System.Collections.Generic;
using IDI.Core.Infrastructure.Verification;
using IDI.Core.Tests.Common;
using IDI.Core.Tests.Common.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IDI.Core.Tests.Infrastructure
{
    [TestClass]
    [TestCategory(Contants.TestCategory.Verification)]
    public class StringLengthVerificationUnitTests
    {
        [TestMethod]
        public void TestStringLengthVerification_Fail_ForRangeLengthLimite_LessThanMinLength()
        {
            var model = new TestObjectB { Field = "1234" };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsFalse(result);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("'测试字段'最长5-10个字符!", errors[0]);
        }

        [TestMethod]
        public void TestStringLengthVerification_Fail_ForRangeLengthLimite_GreaterThanMaxLength()
        {
            var model = new TestObjectB { Field = "12345678901" };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsFalse(result);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("'测试字段'最长5-10个字符!", errors[0]);
        }

        [TestMethod]
        public void TestStringLengthVerification_Success_ForRangeLengthLimite_InRange()
        {
            var model = new TestObjectB { Field = "123456789" };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsTrue(result);
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void TestStringLengthVerification_Success_ForRangeLengthLimite_EqualToMinLength()
        {
            var model = new TestObjectB { Field = "12345" };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsTrue(result);
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void TestStringLengthVerification_Success_ForRangeLengthLimite_EqualToMaxLength()
        {
            var model = new TestObjectB { Field = "1234567890" };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsTrue(result);
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void TestStringLengthVerification_Success_ForMaxLengthLimite_NullValue()
        {
            var model = new TestObjectC { Field = null };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsTrue(result);
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void TestStringLengthVerification_Success_ForMaxLengthLimite_EmptyValue()
        {
            var model = new TestObjectC { Field = "" };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsTrue(result);
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void TestStringLengthVerification_Success_ForMaxLengthLimite_LessThanToMaxLength()
        {
            var model = new TestObjectC { Field = "123456789" };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsTrue(result);
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void TestStringLengthVerification_Success_ForMaxLengthLimite_EqualToMaxLength()
        {
            var model = new TestObjectC { Field = "1234567890" };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsTrue(result);
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void TestStringLengthVerification_Fail_ForMaxLengthLimite_GreaterThanMaxLength()
        {
            var model = new TestObjectC { Field = "12345678901" };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsFalse(result);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("'测试字段'最长10个字符!", errors[0]);
        }

        [TestMethod]
        public void TestStringLengthVerification_Success_ForMinLengthLimite_EqualToMinLength()
        {
            var model = new TestObjectD { Field = "12345" };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsTrue(result);
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void TestStringLengthVerification_Success_ForMinLengthLimite_GreaterThanMinLength()
        {
            var model = new TestObjectD { Field = "123456" };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsTrue(result);
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void TestStringLengthVerification_Fail_ForMinLengthLimite_LessThanMinLength()
        {
            var model = new TestObjectD { Field = "1234" };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsFalse(result);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("'测试字段'最小5个字符!", errors[0]);
        }

        [TestMethod]
        public void TestStringLengthVerification_Fail_ForMinLengthLimite_EmptyValue()
        {
            var model = new TestObjectD { Field = "" };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsFalse(result);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("'测试字段'最小5个字符!", errors[0]);
        }

        [TestMethod]
        public void TestStringLengthVerification_Fail_ForMinLengthLimite_NullValue()
        {
            var model = new TestObjectD { Field = null };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsFalse(result);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("'测试字段'最小5个字符!", errors[0]);
        }
    }
}
