using System.Collections.Generic;
using IDI.Central.Domain.Localization;
using IDI.Core.Localization;

namespace IDI.Central.Domain.Common
{
    public class Localization : ILocalization
    {
        public string Get(string name)
        {
            return LanguageManager.Instance.Get(Resources.Prefix.COMMAND, name);
        }

        public string Get(string prefix, string name)
        {
            return LanguageManager.Instance.Get(prefix, name);
        }

        public List<PackageItem> GetAll(string prefix)
        {
            return LanguageManager.Instance.GetAll(prefix);
        }
    }
}
