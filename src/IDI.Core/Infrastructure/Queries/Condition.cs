using System;
using IDI.Core.Infrastructure.Verification;

namespace IDI.Core.Infrastructure.Queries
{
    public abstract class Condition : ICondition, IVerifiable
    {
        public Guid Id { get; private set; }

        public Condition()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
