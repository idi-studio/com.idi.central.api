using System.Linq;
using IDI.Central.Domain.Localization;
using IDI.Central.Models.Retailing;
using IDI.Core.Common;
using IDI.Core.Common.Basetypes;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Localization;

namespace IDI.Central.Domain.Modules.Retailing.Queries
{
    public class QueryTagCondition : Condition { }

    public class QueryTag : Query<QueryTagCondition, Collection<Tag>>
    {
        [Injection]
        public ILocalization Localization { get; set; }

        public override Result<Collection<Tag>> Execute(QueryTagCondition condition)
        {
            var items = this.Localization.GetAll(Resources.Prefix.TAG).Select(item => new Tag { Key = item.Name, Name = item.Value, Value = string.Empty });

            return Result.Success(new Collection<Tag>(items));
        }
    }
}
