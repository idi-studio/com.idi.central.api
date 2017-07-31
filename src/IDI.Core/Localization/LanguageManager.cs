using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using IDI.Core.Common;
using IDI.Core.Localization.Packages;

namespace IDI.Core.Localization
{
    public sealed class LanguageManager : Singleton<LanguageManager>, ILocalization
    {
        private Dictionary<string, string> items;

        public int Count => items.Count;

        private LanguageManager()
        {
            items = new Dictionary<string, string>();

            Load(new PackageCore());
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

        private string GetValue(string prefix, string name, Category category = Category.English)
        {
            string key = $"{prefix}-{name}-{category.Description()}".ToLower();

            if (items.ContainsKey(key))
                return items[key];

            return name;
        }

        public void Load(Package package)
        {
            foreach (var item in package.Items)
            {
                string key = $"{item.Prefix}-{item.Name}-{item.Language}";

                if (!this.items.ContainsKey(key))
                    this.items.Add(key, item.Value);
            }
        }

        public string Get(string prefix, string name)
        {
            Category category;

            string culture = (CultureInfo.DefaultThreadCurrentUICulture ?? CultureInfo.CurrentCulture).Name;

            switch (culture)
            {
                case "zh-CN":
                    category = Category.SimplifiedChinese;
                    break;
                default:
                    category = Category.English;
                    break;
            }

            return GetValue(prefix, name, category);
        }

        public string Get(string name)
        {
            return Get(prefix: "default", name: name);
        }
    }
}
