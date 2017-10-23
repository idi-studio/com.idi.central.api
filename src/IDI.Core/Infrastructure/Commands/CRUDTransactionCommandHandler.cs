using System;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Localization;
using IDI.Core.Localization.Packages;
using IDI.Core.Logging;
using IDI.Core.Repositories;

namespace IDI.Core.Infrastructure.Commands
{
    public abstract class CRUDTransactionCommandHandler<T> : ICommandHandler<T> where T : Command
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

            using (var transaction = Transaction.Begin())
            {
                try
                {
                    if (command.Mode == CommandMode.Create)
                    {
                        result = Create(command, transaction);
                    }
                    else if (command.Mode == CommandMode.Update)
                    {
                        result = Update(command, transaction);
                    }
                    else if (command.Mode == CommandMode.Delete)
                    {
                        result = Delete(command, transaction);
                    }
                    else if (command.Mode == CommandMode.Upload)
                    {
                        result = Upload(command, transaction);
                    }
                    else
                    {
                        result = Result.Fail(message: Localization.Get(Resources.Key.Command.CommandNonsupport));
                    }
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

        protected abstract Result Create(T command, ITransaction transaction);

        protected abstract Result Update(T command, ITransaction transaction);

        protected abstract Result Delete(T command, ITransaction transaction);

        protected virtual Result Upload(T command, ITransaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
