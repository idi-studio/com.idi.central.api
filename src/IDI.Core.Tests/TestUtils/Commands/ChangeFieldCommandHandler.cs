using System;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Localization;

namespace IDI.Core.Tests.TestUtils.Commands
{
    public class ChangeFieldCommandHandler : ICommandHandler<ChangeFieldCommand>
    {
        public ILocalization Localization { get; set; }

        public Result Execute(ChangeFieldCommand command)
        {
            return new Result { Status = ResultStatus.Success, Message = "执行成功!" };
        }
    }
}
