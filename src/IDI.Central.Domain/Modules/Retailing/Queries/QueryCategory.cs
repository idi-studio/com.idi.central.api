﻿using System;
using System.Collections.Generic;
using System.Linq;
using IDI.Central.Domain.Localization;
using IDI.Core.Common;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Localization;

namespace IDI.Central.Domain.Modules.Retailing.Queries
{
    public class QueryCategoryCondition : Condition
    {
        public string EnumType { get; set; }
    }

    public class QueryCategory : Query<QueryCategoryCondition, Set<KeyValuePair<int, string>>>
    {
        [Injection]
        public ILocalization Localization { get; set; }

        public override Result<Set<KeyValuePair<int, string>>> Execute(QueryCategoryCondition condition)
        {
            string prefix = condition.EnumType;

            Type enumType = Type.GetType($"IDI.Central.Common.{condition.EnumType},IDI.Central.Common");

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
