using System.Collections.Generic;

namespace IDI.Core.Localization
{
    public interface ILocalization
    {
        string Get(string prefix, string name);

        string Get(string name);

        List<PackageItem> GetAll(string prefix);
    }
}
