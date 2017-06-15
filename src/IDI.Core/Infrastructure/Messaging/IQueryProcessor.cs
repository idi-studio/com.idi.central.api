using IDI.Core.Common;
using IDI.Core.Infrastructure.Queries;

namespace IDI.Core.Infrastructure.Messaging
{
    public interface IQueryProcessor
    {
        Result<TQueryResult> Execute<TCondition, TQueryResult>(TCondition condition) where TCondition : Condition
            where TQueryResult : IQueryResult;
    }
}
