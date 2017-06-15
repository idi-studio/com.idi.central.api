using Microsoft.EntityFrameworkCore;

namespace IDI.Core.Repositories.EFCore
{
    public interface IEFCoreRepositoryContext : IRepositoryContext
    {
       DbContext Context { get; }
    }
}
