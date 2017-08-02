using IDI.Core.Common.Basetypes;
using IDI.Core.Localization.Packages;
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
            var profile = new Profile { { Resources.Key.ProfileType.Color, "Black" }, { Resources.Key.ProfileType.Year, "2017" } };

            Assert.AreEqual(2, profile.Count);
            Assert.AreEqual(2, profile.ToCollection().Count);
            Assert.AreEqual($"{Resources.Key.ProfileType.Color}:Black,{Resources.Key.ProfileType.Year}:2017", profile.ToString());
        }
    }
}
