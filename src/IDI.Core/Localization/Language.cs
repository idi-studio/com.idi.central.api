using System.Collections.Generic;
using System.ComponentModel;
using IDI.Core.Common;

namespace IDI.Core.Localization
{
    public sealed class Language : Singleton<Language>
    {
        private Dictionary<string, string> packages;

        private Language()
        {
            packages = new Dictionary<string, string>();
        }

        public enum Category
        {
            [Description("chs")]
            SimplifiedChinese,
            [Description("cht")]
            TraditionalChinese,
            [Description("en")]
            English
        }

        public string Get(string name, Category category = Category.English)
        {
            string key = $"{category}-{name}".ToLower();

            if (packages.ContainsKey(key))
                return packages[key];

            return name;
        }

        public void Load(Package package)
        {

        }
    }
}
