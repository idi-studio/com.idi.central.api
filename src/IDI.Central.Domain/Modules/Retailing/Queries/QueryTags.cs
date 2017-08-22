using System.Linq;
using IDI.Central.Domain.Localization;
using IDI.Central.Models.Retailing;
using IDI.Core.Common;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Localization;

namespace IDI.Central.Domain.Modules.Retailing.Queries
{
    public class QueryTagsCondition : Condition { }

    public class QueryTags : Query<QueryTagsCondition, Set<Tag>>
    {
        [Injection]
        public ILocalization Localization { get; set; }

        public override Result<Set<Tag>> Execute(QueryTagsCondition condition)
        {
            var items = this.Localization.GetAll(Resources.Prefix.TAG).Select(item => new Tag { Key = item.Name, Name = item.Value, Value = string.Empty });

            return Result.Success(new Set<Tag>(items));
        }
    }
}
