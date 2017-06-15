using IDI.Core.Common;

namespace IDI.Core.Infrastructure.Queries
{
    public interface IQuery<TCondition, TQueryResult> 
        where TQueryResult : IQueryResult 
        where TCondition : Condition
    {
        Result<TQueryResult> Execute(TCondition condition);
    }
}
