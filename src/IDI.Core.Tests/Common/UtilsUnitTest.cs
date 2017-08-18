using System;
using IDI.Core.Common;
using IDI.Core.Tests.TestUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IDI.Core.Tests.Common
{
    [TestClass]
    [TestCategory(Contants.TestCategory.Common)]
    public class UtilsUnitTest
    {
        [TestMethod]
        public void Utils_NewGuid_Int32()
        {
            Assert.AreEqual("00000001-0000-0000-0000-000000000000", Utils.NewGuid(1).ToString().ToUpper());
            Assert.AreEqual("04C4B400-0000-0000-0000-000000000000", Utils.NewGuid(80000000).ToString().ToUpper());
            Assert.AreEqual("80000000-0000-0000-0000-000000000000", Utils.NewGuid(Int32.MinValue).ToString().ToUpper());
            Assert.AreEqual("7FFFFFFF-0000-0000-0000-000000000000", Utils.NewGuid(Int32.MaxValue).ToString().ToUpper());
        }
    }
}
