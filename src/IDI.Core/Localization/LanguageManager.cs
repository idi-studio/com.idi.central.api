using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using IDI.Core.Common;
using IDI.Core.Localization.Packages;

namespace IDI.Core.Localization
{
    public sealed class LanguageManager : Singleton<LanguageManager>, ILocalization
    {
        private List<PackageItem> items;

        public int Count => items.Count;

        private LanguageManager()
        {
            items = new List<PackageItem>();

            Load(new LanguagePackage());
        }

        public enum Language
        {
            [Description("chs")]
            SimplifiedChinese,
            [Description("cht")]
            TraditionalChinese,
            [Description("en")]
            English
        }

        private string GetValue(string prefix, string name, Language category = Language.English)
        {
            var item = items.FirstOrDefault(e => e.Prefix == prefix && e.Name == name && e.Language == category.Description());

            if (item != null)
                return item.Value;

            return name;
        }

        public void Load(Package package)
        {
            foreach (var item in package.Items)
            {
                if (!this.items.Any(e => e.Prefix == item.Prefix && e.Name == item.Name && e.Language == item.Language))
                    this.items.Add(item);
            }
        }

        public string Get(string prefix, string name)
        {
            Language category;

            string culture = (CultureInfo.DefaultThreadCurrentUICulture ?? CultureInfo.CurrentCulture).Name;

            switch (culture)
            {
                case "zh-CN":
                    category = Language.SimplifiedChinese;
                    break;
                default:
                    category = Language.English;
                    break;
            }

            return GetValue(prefix, name, category);
        }

        public string Get(string name)
        {
            return Get(prefix: "default", name: name);
        }

        public List<PackageItem> GetAll(string prefix)
        {
            Language category;

            string culture = (CultureInfo.DefaultThreadCurrentUICulture ?? CultureInfo.CurrentCulture).Name;

            switch (culture)
            {
                case "zh-CN":
                    category = Language.SimplifiedChinese;
                    break;
                default:
                    category = Language.English;
                    break;
            }

            return this.items.Where(e => e.Prefix == prefix && e.Language == category.Description()).ToList();
        }

        public string Get<T>(T value) where T : struct
        {
            var type = value.GetType();

            if (!type.IsEnum)
                throw new ArgumentException("The value must be an enum type.");

            string name = Enum.GetName(type, value);

            return Get(type.Name, name);
        }
    }
}
