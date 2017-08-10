using System;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure.Verification;

namespace IDI.Core.Infrastructure.Commands
{
    public abstract class Command : ICommand, IVerifiable
    {
        public Guid UniqueId { get; private set; }

        public CommandMode Mode { get; set; } = CommandMode.None;

        public int Version { get; private set; }

        public Command()
        {
            UniqueId = Guid.NewGuid();
            Version = 1;
        }
    }
}
