using System.Linq;
using IDI.Central.Common.JsonTypes;
using IDI.Central.Domain.Localization;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Queries;

namespace IDI.Central.Domain.Modules.BasicInfo.Queries
{
    public class QueryTagSetCondition : Condition { }

    public class QueryTagSet : Query<QueryTagSetCondition, Set<Tag>>
    {
        public override Result<Set<Tag>> Execute(QueryTagSetCondition condition)
        {
            var items = this.Localization.GetAll(Resources.Prefix.TAG).Select(item => new Tag { Key = item.Name, Name = item.Value, Value = string.Empty });

            return Result.Success(new Set<Tag>(items));
        }
    }
}
