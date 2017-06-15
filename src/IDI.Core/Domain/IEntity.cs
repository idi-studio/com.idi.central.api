using System;

namespace IDI.Core.Domain
{
    public interface IEntity
    {
        Guid Id { get; set; }

        int Version { get; set; }

        string CreatedBy { get; set; }

        DateTime CreatedTime { get; set; }

        string LastUpdatedBy { get; set; }

        DateTime LastUpdatedTime { get; set; }
    }
}
