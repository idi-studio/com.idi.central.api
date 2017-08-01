using IDI.Core.Localization;
using IDI.Core.Tests.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IDI.Core.Tests.Localization
{
    [TestClass]
    [TestCategory(Contants.TestCategory.Language)]
    public class LanguageUnitTests
    {
        [TestMethod]
        public void Language_Can_Load_PackageCore()
        {
            var language = LanguageManager.Instance;

            Assert.IsTrue(language.Count > 0);
        }
    }
}
