using System.Collections.Generic;
using IDI.Core.Infrastructure.Queries;

namespace IDI.Core.Common
{
    public class Set<T> : List<T>, IModel
    {
        public Set() : base() { }
        public Set(IEnumerable<T> collection) : base(collection) { }
    }
}
