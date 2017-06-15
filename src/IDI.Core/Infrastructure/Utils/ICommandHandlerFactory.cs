using IDI.Core.Infrastructure.Commands;

namespace IDI.Core.Infrastructure.Utils
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler<T> GetHandler<T>() where T : Command;
    }
}
