using Microsoft.EntityFrameworkCore;

namespace IDI.Core.Domain
{
    public abstract class EntityMapping
    {
        public abstract void Create(ModelBuilder modelBuilder);
    }
}
