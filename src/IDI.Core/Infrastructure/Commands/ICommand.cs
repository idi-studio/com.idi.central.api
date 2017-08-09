using System;

namespace IDI.Core.Infrastructure.Commands
{
    public interface ICommand
    {
        Guid UniqueId { get; }

        int Version { get; }
    }
}
