using IDI.Core.Infrastructure.Queries;

namespace IDI.Central.Models.Administration
{
    public class UserIdentity : IModel
    {
        public string NameIdentifier { get; set; }

        public string Name { get; set; }

        public string Roles { get; set; }

        public string Gender { get; set; }
    }
}
