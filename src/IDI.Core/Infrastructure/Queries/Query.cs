using IDI.Core.Common;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Localization;

namespace IDI.Core.Infrastructure.Queries
{
    public abstract class Query<TCondition, TResult> : IQuery<TCondition, TResult> 
        where TResult : IModel
        where TCondition : Condition
    {
        [Injection]
        public ILocalization Localization { get; set; }

        public abstract Result<TResult> Execute(TCondition condition);
    }
}
