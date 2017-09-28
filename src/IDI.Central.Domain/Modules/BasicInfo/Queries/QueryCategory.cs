using System;
using System.Collections.Generic;
using System.Linq;
using IDI.Central.Domain.Localization;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Infrastructure.Verification.Attributes;

namespace IDI.Central.Domain.Modules.BasicInfo.Queries
{
    public class QueryCategoryCondition : Condition
    {
        [RequiredField]
        public string EnumType { get; set; }
    }

    public class QueryCategory : Query<QueryCategoryCondition, Set<KeyValuePair<int, string>>>
    {
        public override Result<Set<KeyValuePair<int, string>>> Execute(QueryCategoryCondition condition)
        {
            string prefix = condition.EnumType;

            Type enumType = Type.GetType($"IDI.Central.Common.Enums.{condition.EnumType},IDI.Central.Common");

            if (enumType == null)
                return Result.Fail<Set<KeyValuePair<int, string>>>(Localization.Get(Resources.Key.Command.InvalidCategory));

            var collection = Enum.GetValues(enumType).Cast<object>().Select(value =>
            {
                string name = Enum.GetName(enumType, value);
                return new KeyValuePair<int, string>((int)value, Localization.Get(prefix, name));
            });

            return Result.Success(new Set<KeyValuePair<int, string>>(collection));
        }
    }
}
