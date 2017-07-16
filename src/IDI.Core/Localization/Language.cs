using System.Collections.Generic;
using System.ComponentModel;
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
            string key = $"{category}-{name}".ToLower();

            if (items.ContainsKey(key))
                return items[key];

            return name;
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
