using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;

namespace IDI.Core.Infrastructure.Messaging
{
    public interface ICommandBus
    {
        Result Send<T>(T command) where T : Command;
    }
}
