using System;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure.Verification;

namespace IDI.Core.Infrastructure.Queries
{
    public abstract class Condition : ICondition, IVerifiable
    {
        public Guid UniqueId { get; private set; }

        public ValidationGroup Group { get; set; } = ValidationGroup.Default;

        public Condition()
        {
            this.UniqueId = Guid.NewGuid();
        }
    }
}
