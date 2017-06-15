using System;

namespace IDI.Core.Infrastructure.Commands
{
    public interface ICommand
    {
        Guid Id { get; }

        int Version { get; }
    }
}
