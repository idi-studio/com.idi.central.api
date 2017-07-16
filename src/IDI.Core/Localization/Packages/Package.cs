using System.Collections.Generic;
using System.IO;
using System.Reflection;
using IDI.Core.Common.Extensions;

namespace IDI.Core.Localization
{
    public abstract class Package
    {
        public List<PackageItem> Items { get; private set; } = new List<PackageItem>();

        public Package(string assemblyName, string packageName)
        {
            var assembly = Assembly.Load(new AssemblyName(assemblyName));

            if (assembly == null)
                return;

            using (Stream stream = assembly.GetManifestResourceStream(packageName))
            {
                if (stream == null)
                    return;

                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();
                    this.Items = json.To<List<PackageItem>>();
                }
            }

        }
    }
}
