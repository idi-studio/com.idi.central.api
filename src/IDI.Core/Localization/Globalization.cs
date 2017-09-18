using System.Collections.Generic;
using IDI.Core.Localization.Packages;

namespace IDI.Core.Localization
{
    public class Globalization : ILocalization
    {
        public string Get(string name)
        {
            return LanguageManager.Instance.Get(Resources.Prefix.COMMAND, name);
        }

        public string Get(string prefix, string name)
        {
            return LanguageManager.Instance.Get(prefix, name);
        }

        public string Get<T>(T value) where T : struct
        {
            return LanguageManager.Instance.Get(value);
        }

        public List<PackageItem> GetAll(string prefix)
        {
            return LanguageManager.Instance.GetAll(prefix);
        }
    }
}
