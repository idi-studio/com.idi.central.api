using System.Collections.Generic;
using System.Linq;
using IDI.Central.Models.Retailing;
using IDI.Core.Common;

namespace IDI.Central.Domain.Common
{
    public static class Extensions
    {
        public static string AsString(this List<Tag> tags)
        {
            return tags.Select(tag => { return $"{tag.Name}:{tag.Value}"; }).JoinToString(",");
        }
    }
}
