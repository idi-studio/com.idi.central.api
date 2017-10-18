﻿using System;
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
            if (command.Mode == CommandMode.Create)
            {
                return Create(command);
            }
            else if (command.Mode == CommandMode.Update)
            {
                return Update(command);
            }
            else if (command.Mode == CommandMode.Delete)
            {
                return Delete(command);
            }
            else if (command.Mode == CommandMode.Upload)
            {
                return Upload(command);
            }

            return Result.Fail(message: Localization.Get(Resources.Key.Command.CommandNonsupport));
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