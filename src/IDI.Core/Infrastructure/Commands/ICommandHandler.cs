using IDI.Core.Common;
using IDI.Core.Localization;

namespace IDI.Core.Infrastructure.Commands
{
    public interface ICommandHandler<T> where T : Command
    {
        ILocalization Localization { get; set; }

        Result Execute(T command);
    }
}
