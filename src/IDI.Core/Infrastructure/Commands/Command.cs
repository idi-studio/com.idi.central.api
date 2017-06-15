using System;
using IDI.Core.Infrastructure.Verification;

namespace IDI.Core.Infrastructure.Commands
{
    public abstract class Command : ICommand, IVerifiable
    {
        public Guid Id { get; private set; }

        public int Version { get; private set; }

        public Command()
        {
            Id = Guid.NewGuid();
            Version = 1;
        }
    }
}
