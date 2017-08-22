using IDI.Core.Infrastructure.Queries;

namespace IDI.Central.Models.Administration
{
    public class UserIdentity : IQueryResult
    {
        public string NameIdentifier { get; set; }

        public string Name { get; set; }

        public string Role { get; set; }

        public string Gender { get; set; }
    }
}
