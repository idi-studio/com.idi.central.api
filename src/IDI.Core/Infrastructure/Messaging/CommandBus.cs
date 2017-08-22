using System.Collections.Generic;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.Utils;
using IDI.Core.Infrastructure.Verification;

namespace IDI.Core.Infrastructure.Messaging
{
    public class CommandBus : ICommandBus
    {
        private readonly ICommandHandlerFactory _commandHandlerFactory;

        public CommandBus(ICommandHandlerFactory commandHandlerFactory)
        {
            _commandHandlerFactory = commandHandlerFactory;
        }

        public Result Send<T>(T command) where T : Command
        {
            if (command == null)
                return Result.Error("命令参数不能为空!");

            List<string> errors;

            if (!command.IsValid(out errors))
                return Result.Fail("命令参数验证失败!").Attach("errors", errors);

            var handler = _commandHandlerFactory.GetHandler<T>();

            if (handler == null)
                return Result.Error("未找到任何相关命令处理器!").Attach("errors", errors);

            return handler.Execute(command);
        }
    }
}
