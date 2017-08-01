using IDI.Core.Common.Basetypes;
using IDI.Core.Common.Enums;
using IDI.Core.Tests.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IDI.Core.Tests.Common
{
    [TestClass]
    [TestCategory(Contants.TestCategory.Common)]
    public class BasetypesUnitTests
    {
        [TestMethod]
        public void Test_Profile()
        {
            var profile = new Profile { { ProfileType.Color, "Black" }, { ProfileType.Year, "2017" } };

            Assert.AreEqual(2, profile.Count);
            Assert.AreEqual(2, profile.ToCollection().Count);
        }
    }
}
