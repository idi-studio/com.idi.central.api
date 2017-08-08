using System.Collections.Generic;
using IDI.Core.Infrastructure.Queries;

namespace IDI.Core.Common.Basetypes
{
    public class Collection<T> : List<T>, IQueryResult
    {
        public Collection() : base() { }
        public Collection(IEnumerable<T> collection) : base(collection) { }
    }
}
