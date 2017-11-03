using System;
using System.Collections.Generic;
using System.Globalization;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.Verification;
using IDI.Core.Infrastructure.Verification.Attributes;
using IDI.Core.Tests.TestUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IDI.Core.Tests.Infrastructure
{
    [TestClass]
    [TestCategory(Contants.TestCategory.Verification)]
    public class CompareVerificationUnitTests
    {
        #region TestObjects
        public class EqualToModel : Command
        {
            [Compare(CompareMethod.EqualTo, "EndTime")]
            public DateTime StartTime { get; set; }

            public DateTime EndTime { get; set; }
        }
        public class LessThanModel : Command
        {
            [Compare(CompareMethod.LessThan, "EndTime")]
            public DateTime StartTime { get; set; }

            public DateTime EndTime { get; set; }
        }
        public class LessThanOrEqualToModel : Command
        {
            [Compare(CompareMethod.LessThanOrEqualTo, "EndTime")]
            public DateTime StartTime { get; set; }

            public DateTime EndTime { get; set; }
        }
        public class GreaterThanModel : Command
        {
            [Compare(CompareMethod.GreaterThan, "EndTime")]
            public DateTime StartTime { get; set; }

            public DateTime EndTime { get; set; }
        }
        public class GreaterThanOrEqualToModel : Command
        {
            [Compare(CompareMethod.GreaterThanOrEqualTo, "EndTime")]
            public DateTime StartTime { get; set; }

            public DateTime EndTime { get; set; }
        }
        #endregion

        private DateTime current = DateTime.Now;

        [TestInitialize]
        public void Setup()
        {
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("zh-CN");
        }

        [TestMethod]
        public void It_Should_Be_Pass_When_StartTime_EqualTo_EndTime()
        {
            var model = new EqualToModel { StartTime = current.Date, EndTime = current.Date };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsTrue(result);
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void It_Should_Not_Be_Pass_When_StartTime_NotEqualTo_EndTime()
        {
            var model = new EqualToModel { StartTime = current.Date, EndTime = current.Date.AddDays(1) };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsFalse(result);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("'StartTime'应等于'EndTime'.", errors[0]);
        }

        [TestMethod]
        public void It_Should_Be_Pass_When_StartTime_LessThan_EndTime()
        {
            var model = new LessThanModel { StartTime = current.Date.AddDays(-1), EndTime = current.Date };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsTrue(result);
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void It_Should_Not_Be_Pass_When_StartTime_NotLessThan_EndTime()
        {
            var model = new LessThanModel { StartTime = current.Date.AddDays(1), EndTime = current.Date };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsFalse(result);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("'StartTime'应小于'EndTime'.", errors[0]);
        }

        [TestMethod]
        public void It_Should_Be_Pass_When_StartTime_LessThanOrEqualTo_EndTime()
        {
            var model = new LessThanOrEqualToModel { StartTime = current.Date.AddDays(-1), EndTime = current.Date };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsTrue(result);
            Assert.AreEqual(0, errors.Count);

            model = new LessThanOrEqualToModel { StartTime = current.Date, EndTime = current.Date };

            result = model.IsValid(out errors);

            Assert.IsTrue(result);
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void It_Should_Not_Be_Pass_When_StartTime_NotLessThanOrEqualTo_EndTime()
        {
            var model = new LessThanOrEqualToModel { StartTime = current.Date, EndTime = current.Date.AddDays(-1) };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsFalse(result);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("'StartTime'应小于或等于'EndTime'.", errors[0]);
        }

        [TestMethod]
        public void It_Should_Be_Pass_When_StartTime_GreaterThan_EndTime()
        {
            var model = new GreaterThanModel { StartTime = current.Date, EndTime = current.Date.AddDays(-1) };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsTrue(result);
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void It_Should_Not_Be_Pass_When_StartTime_NotGreaterThan_EndTime()
        {
            var model = new GreaterThanModel { StartTime = current.Date, EndTime = current.Date.AddDays(1) };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsFalse(result);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("'StartTime'应大于'EndTime'.", errors[0]);
        }

        [TestMethod]
        public void It_Should_Be_Pass_When_StartTime_GreaterThanOrEqualTo_EndTime()
        {
            var model = new GreaterThanOrEqualToModel { StartTime = current.Date, EndTime = current.Date.AddDays(-1) };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsTrue(result);
            Assert.AreEqual(0, errors.Count);

            model = new GreaterThanOrEqualToModel { StartTime = current.Date, EndTime = current.Date };

            result = model.IsValid(out errors);

            Assert.IsTrue(result);
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void It_Should_Not_Be_Pass_When_StartTime_NotGreaterThanOrEqualTo_EndTime()
        {
            var model = new GreaterThanOrEqualToModel { StartTime = current.Date.AddDays(-1), EndTime = current.Date };

            List<string> errors;

            var result = model.IsValid(out errors);

            Assert.IsFalse(result);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("'StartTime'应大于或等于'EndTime'.", errors[0]);
        }
    }
}
