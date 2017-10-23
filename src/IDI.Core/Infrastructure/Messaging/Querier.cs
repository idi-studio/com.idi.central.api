using System;
using System.Collections.Generic;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Infrastructure.Utils;
using IDI.Core.Infrastructure.Verification;
using IDI.Core.Localization;
using IDI.Core.Localization.Packages;
using IDI.Core.Logging;

namespace IDI.Core.Infrastructure.Messaging
{
    public interface IQuerier
    {
        Result<TQueryResult> Execute<TCondition, TQueryResult>(TCondition condition = default(TCondition)) where TCondition : Condition
            where TQueryResult : IModel;
    }

    public class Querier : IQuerier
    {
        private readonly IQueryBuilder builder;
        private readonly ILocalization localization;
        private readonly ILogger logger;

        public Querier(IQueryBuilder builder, ILocalization localization, ILogger logger)
        {
            this.builder = builder;
            this.localization = localization;
            this.logger = logger;
        }

        public Result<TQueryResut> Execute<TCondition, TQueryResut>(TCondition condition) where TCondition : Condition
            where TQueryResut : IModel
        {
            condition = condition ?? Activator.CreateInstance<TCondition>();

            if (condition == null)
                return Result.Error<TQueryResut>(message: localization.Get(Resources.Key.Querier.QuerierConditionCannotBeNull));

            List<string> errors;

            if (!condition.IsValid(out errors))
                return Result.Fail<TQueryResut>(message: localization.Get(Resources.Key.Querier.QuerierWithInvalidCondition)).Attach("errors", errors);

            try
            {
                var query = builder.GetQuery<TCondition, TQueryResut>();

                if (query == null)
                    return Result.Error<TQueryResut>(message: localization.Get(Resources.Key.Querier.QuerierCannotFound));

                return query.Execute(condition);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
                return Result.Error<TQueryResut>(message: localization.Get(Resources.Key.Querier.QuerierError));
            }
        }
    }
}
