using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using IDI.Core.Infrastructure.Queries;

namespace IDI.Core.Infrastructure.Utils
{
    public class QueryBuilder : IQueryBuilder
    {
        public IQuery<TCondition, TQueryResult> GetQuery<TCondition, TQueryResult>() where TCondition : Condition
            where TQueryResult : IQueryResult
        {
            var queries = GetQueryTypes<TCondition, TQueryResult>().ToList();

            return queries.Select(type => (IQuery<TCondition, TQueryResult>)type.CreateInstance()).FirstOrDefault();
        }

        private IEnumerable<Type> GetQueryTypes<TCondition, TQueryResult>() where TCondition : Condition
             where TQueryResult : IQueryResult
        {
            var queries = typeof(TCondition).GetTypeInfo().Assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(a => a.GetTypeInfo().IsGenericType && a.GetGenericTypeDefinition() == typeof(IQuery<,>)))
                .Where(t => t.GetInterfaces().Any(a => a.GetGenericArguments().Any(p1 => p1 == typeof(TCondition)) && a.GetGenericArguments().Any(p2 => p2 == typeof(TQueryResult)))).ToList();

            return queries;
        }
    }
}
