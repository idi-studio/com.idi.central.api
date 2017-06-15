using System;
using System.Linq.Expressions;

namespace IDI.Core.Common
{
    public class AscBy<T> : SortPredicate<T>
    {
        public AscBy(Expression<Func<T, dynamic>> predicate) : base(predicate, SortOrder.Asc) { }
    }

    public class DescBy<T> : SortPredicate<T>
    {
        public DescBy(Expression<Func<T, dynamic>> predicate) : base(predicate, SortOrder.Desc) { }
    }

    public abstract class SortPredicate<T>
    {
        public Expression<Func<T, dynamic>> Predicate { get; private set; }

        public SortOrder Direction { get; private set; }

        public SortPredicate(Expression<Func<T, dynamic>> predicate, SortOrder direction)
        {
            this.Predicate = predicate;
            this.Direction = direction;
        }
    }
}
