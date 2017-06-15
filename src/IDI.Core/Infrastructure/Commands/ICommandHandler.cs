using IDI.Core.Common;

namespace IDI.Core.Infrastructure.Commands
{
    public interface ICommandHandler<T> where T : Command
    {
        Result Execute(T command);
    }
}
