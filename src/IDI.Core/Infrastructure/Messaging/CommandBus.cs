using System;
using System.Collections.Generic;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.Utils;
using IDI.Core.Infrastructure.Verification;
using IDI.Core.Localization;
using IDI.Core.Localization.Packages;
using IDI.Core.Logging;

namespace IDI.Core.Infrastructure.Messaging
{
    public class CommandBus : ICommandBus
    {
        private readonly ILogger logger;
        private readonly ILocalization localization;
        private readonly ICommandHandlerFactory factory;

        public CommandBus(ICommandHandlerFactory factory, ILogger logger, ILocalization localization)
        {
            this.factory = factory;
            this.logger = logger;
            this.localization = localization;
        }

        public Result Send<T>(T command) where T : Command
        {
            if (command == null)
                return Result.Error(localization.Get(Resources.Key.Command.UnknownCommand));

            List<string> errors;

            if (!command.IsValid(out errors))
                return Result.Fail(localization.Get(Resources.Key.Command.CommandWithInvalidParameter)).Attach("errors", errors);

            var handler = factory.GetHandler<T>();

            if (handler == null)
                return Result.Error(localization.Get(Resources.Key.Command.CommandInvalid));

            try
            {
                logger.Info(command);

                return handler.Execute(command);
            }
            catch (Exception exception)
            {
                logger.Error(localization.Get(Resources.Key.Command.CommandExecutedWithError), exception);
                return Result.Error(localization.Get(Resources.Key.Command.CommandExecutedWithError));
            }

        }
    }
}
