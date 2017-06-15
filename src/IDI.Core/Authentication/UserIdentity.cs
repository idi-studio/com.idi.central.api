using IDI.Core.Common;
using IDI.Core.Infrastructure.Queries;

namespace IDI.Core.Authentication
{
    public class UserIdentity : IQueryResult
    {
        public string NameIdentifier { get; set; }
       
        public string Name { get; set; }

        public string Role { get; set; }

        public Gender Gender { get; set; }
    }
}
