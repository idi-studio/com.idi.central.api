using System.Collections.Generic;
using IDI.Core.Common.Enums;
using System.Linq;

namespace IDI.Core.Common.Basetypes
{
    public class Profile : Dictionary<ProfileType, string>
    {
        public ProfileCollection ToCollection()
        {
            var collection = new ProfileCollection();
            collection.AddRange(this.ToList());
            return collection;
        }
    }

    public class ProfileCollection : List<KeyValuePair<ProfileType, string>> { }
}
