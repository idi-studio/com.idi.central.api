using System;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Localization;
using IDI.Core.Localization.Packages;
using IDI.Core.Logging;

namespace IDI.Core.Infrastructure.Commands
{
    public abstract class CRUDCommandHandler<T> : ICommandHandler<T> where T : Command
    {
        [Injection]
        public ILocalization Localization { get; set; }

        [Injection]
        public ILogger Logger { get; set; }

        public Result Execute(T command)
        {
            Result result;

            try
            {
                switch (command.Mode)
                {
                    case CommandMode.Create:
                        result = Create(command);
                        break;
                    case CommandMode.Update:
                        result = Update(command);
                        break;
                    case CommandMode.Delete:
                        result = Delete(command);
                        break;
                    case CommandMode.Upload:
                        result = Upload(command);
                        break;
                    default:
                        result = Result.Fail(message: Localization.Get(Resources.Key.Command.CommandNonsupport));
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                result = Result.Error(message: Localization.Get(Resources.Key.Command.CommandError));
            }

            return result;
        }

        protected abstract Result Create(T command);

        protected abstract Result Update(T command);

        protected abstract Result Delete(T command);

        protected virtual Result Upload(T command)
        {
            throw new NotImplementedException();
        }
    }
}
