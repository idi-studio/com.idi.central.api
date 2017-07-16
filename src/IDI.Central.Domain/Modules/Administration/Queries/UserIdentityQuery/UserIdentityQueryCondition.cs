using IDI.Core.Infrastructure.Queries;
using IDI.Core.Infrastructure.Verification.Attributes;

namespace IDI.Central.Domain.Modules.Administration.Queries
{
    public class UserIdentityQueryCondition : Condition
    {
        [RequiredField("username")]
        public string UserName { get; set; }
    }
}
