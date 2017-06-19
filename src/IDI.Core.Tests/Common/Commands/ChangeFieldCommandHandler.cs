using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;

namespace IDI.Core.Tests.Common.Commands
{
    public class ChangeFieldCommandHandler : ICommandHandler<ChangeFieldCommand>
    {
        public Result Execute(ChangeFieldCommand command)
        {
            return new Result { Status = ResultStatus.Success, Message = "执行成功!" };
        }
    }
}
