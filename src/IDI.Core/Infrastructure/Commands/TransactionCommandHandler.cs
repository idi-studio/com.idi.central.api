﻿using System;
using IDI.Core.Common;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Localization;
using IDI.Core.Localization.Packages;
using IDI.Core.Logging;
using IDI.Core.Repositories;

namespace IDI.Core.Infrastructure.Commands
{
    public abstract class TransactionCommandHandler<T> : ICommandHandler<T> where T : Command
    {
        [Injection]
        public ILocalization Localization { get; set; }

        [Injection]
        public ITransaction Transaction { get; set; }

        [Injection]
        public ILogger Logger { get; set; }

        public Result Execute(T command)
        {
            Result result;

            try
            {
                Executing(command, this.Transaction);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                return Result.Error(message: Localization.Get(Resources.Key.Command.CommandError));
            }

            using (var transaction = Transaction.Begin())
            {
                try
                {
                    result = Execute(command, transaction);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    result = Result.Error(message: Localization.Get(Resources.Key.Command.CommandError));
                    Logger.Error(ex.Message, ex);
                }
            }

            return result;
        }

        protected virtual void Executing(T command, ITransaction transaction) { }

        protected abstract Result Execute(T command, ITransaction transaction);
    }
}
