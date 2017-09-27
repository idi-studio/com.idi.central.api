using System;
using System.Collections.Generic;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Infrastructure.Utils;
using IDI.Core.Infrastructure.Verification;

namespace IDI.Core.Infrastructure.Messaging
{
    public interface IQuerier
    {
        Result<TQueryResult> Execute<TCondition, TQueryResult>(TCondition condition = default(TCondition)) where TCondition : Condition
            where TQueryResult : IModel;
    }

    public class Querier : IQuerier
    {
        private readonly IQueryBuilder _queryBuilder;

        public Querier(IQueryBuilder queryBuilder)
        {
            _queryBuilder = queryBuilder;
        }

        public Result<TQueryResut> Execute<TCondition, TQueryResut>(TCondition condition) where TCondition : Condition
            where TQueryResut : IModel
        {
            condition = condition ?? Activator.CreateInstance<TCondition>();

            if (condition == null)
                return Result.Error<TQueryResut>("查询条件不能为空!");

            List<string> errors;

            if (!condition.IsValid(out errors))
                return Result.Fail<TQueryResut>("查询条件验证失败!").Attach("errors", errors);

            var query = _queryBuilder.GetQuery<TCondition, TQueryResut>();

            if (query == null)
                return Result.Error<TQueryResut>("未找到任何相关查询器!");

            return query.Execute(condition);
        }
    }
}
