using System;
using IDI.Core.Infrastructure.Verification;

namespace IDI.Core.Infrastructure.Commands
{
    public abstract class Command : ICommand, IVerifiable
    {
        public Guid UniqueId { get; private set; }

        public int Version { get; private set; }

        public Command()
        {
            UniqueId = Guid.NewGuid();
            Version = 1;
        }
    }
}
