using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using IDI.Core.Common;
using IDI.Core.Localization.Packages;

namespace IDI.Core.Localization
{
    public sealed class Language : Singleton<Language>
    {
        private Dictionary<string, string> items;

        public int Count => items.Count;

        private Language()
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

        public string Get(string name, Category category = Category.English)
        {
            string key = $"{name}-{category.Description()}".ToLower();

            if (items.ContainsKey(key))
                return items[key];

            return name;
        }

        public string GetByCulture(string name)
        {
            Category category;

            string culture = CultureInfo.CurrentCulture.Name;

            switch (culture)
            {
                case "zh-CN":
                    category = Category.SimplifiedChinese;
                    break;
                default:
                    category = Category.English;
                    break;
            }

            return Get(name, category);
        }

        public void Load(Package package)
        {
            foreach (var item in package.Items)
            {
                string key = $"{item.Name}-{item.Category}";

                if (!this.items.ContainsKey(key))
                    this.items.Add(key, item.Value);
            }
        }
    }
}
