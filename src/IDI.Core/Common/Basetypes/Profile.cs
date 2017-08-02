using System.Collections.Generic;
using System.Linq;

namespace IDI.Core.Common.Basetypes
{
    public class Profile : Dictionary<string, string>
    {
        public ProfileCollection ToCollection()
        {
            var collection = new ProfileCollection();
            collection.AddRange(this.ToList());
            return collection;
        }

        public override string ToString()
        {
            return this.Select(item => $"{item.Key}:{item.Value}").JoinToString(",");
        }
    }

    public class ProfileCollection : List<KeyValuePair<string, string>> { }
}
