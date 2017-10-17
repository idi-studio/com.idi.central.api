using System;

namespace IDI.Core.Domain
{
    public interface IEntity
    {
        Guid Id { get; set; }

        int Version { get; set; }

        string CreatedBy { get; set; }

        DateTime CreatedAt { get; set; }

        string LastUpdatedBy { get; set; }

        DateTime LastUpdatedAt { get; set; }

        Guid TransactionId { get; set; }
    }
}
