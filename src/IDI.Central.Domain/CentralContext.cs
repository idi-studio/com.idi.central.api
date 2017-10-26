using IDI.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace IDI.Central.Domain
{
    public class CentralContext : DomainContext
    {
        public CentralContext(DbContextOptions options) : base(options) { }
    }
}
