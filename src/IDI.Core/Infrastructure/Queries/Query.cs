using IDI.Core.Common;

namespace IDI.Core.Infrastructure.Queries
{
    public abstract class Query<TCondition, TResult> : IQuery<TCondition, TResult> 
        where TResult : IQueryResult
        where TCondition : Condition
    {
        public abstract Result<TResult> Execute(TCondition condition);
    }
}
