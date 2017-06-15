using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace IDI.Core.Common
{
    public static class QueryableExtension
    {
        private static ConcurrentDictionary<string, LambdaExpression> cache = new ConcurrentDictionary<string, LambdaExpression>();

        public static IOrderedQueryable<T> SortBy<T>(this IQueryable<T> queryable, IEnumerable<SortPredicate<T>> sortPredicates)
        {
            int index = 0;

            IOrderedQueryable<T> orderedQueryable = null;

            foreach (var sortPredicate in sortPredicates)
            {
                dynamic keySelector = GetLambdaExpression(sortPredicate.Predicate);

                if (index == 0)
                    orderedQueryable = sortPredicate.Direction == SortOrder.Asc ? Queryable.OrderBy(queryable, keySelector) : Queryable.OrderByDescending(queryable, keySelector);
                else
                    orderedQueryable = sortPredicate.Direction == SortOrder.Asc ? Queryable.ThenBy(orderedQueryable, keySelector) : Queryable.ThenByDescending(orderedQueryable, keySelector);

                index += 1;
            }

            return orderedQueryable;
        }

        private static LambdaExpression GetLambdaExpression<T>(Expression<Func<T, dynamic>> sortPredicate)
        {
            var propertyName = "";

            if (sortPredicate.Body is MemberExpression)
                propertyName = (sortPredicate.Body as MemberExpression).Member.Name;

            if (sortPredicate.Body is UnaryExpression)
                propertyName = ((sortPredicate.Body as UnaryExpression).Operand as MemberExpression).Member.Name;

            if (!cache.ContainsKey(propertyName))
            {
                var param = Expression.Parameter(typeof(T));

                var body = Expression.Property(param, propertyName);

                cache.AddOrUpdate(propertyName, Expression.Lambda(body, param), (key, oldValue) => oldValue);
            }

            return cache[propertyName];
        }
    }
}
