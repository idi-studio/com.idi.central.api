using IDI.Core.Infrastructure.Queries;

namespace IDI.Core.Infrastructure.Utils
{
    public interface IQueryBuilder
    {
        IQuery<TCondition, TQueryResult> GetQuery<TCondition, TQueryResult>() where TCondition : Condition
            where TQueryResult : IQueryResult;
    }
}
