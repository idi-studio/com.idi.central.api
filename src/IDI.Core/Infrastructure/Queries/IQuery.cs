using IDI.Core.Common;

namespace IDI.Core.Infrastructure.Queries
{
    public interface IQuery<TCondition, TQueryResult> 
        where TQueryResult : IModel 
        where TCondition : Condition
    {
        Result<TQueryResult> Execute(TCondition condition);
    }
}
