using IDI.Core.Infrastructure.Queries;
using IDI.Core.Infrastructure.Verification.Attributes;

namespace IDI.Central.Domain.Modules.SCM.Conditions
{
    public class UserIdentityQueryCondition : Condition
    {
        [RequiredField("用户名")]
        public string UserName { get; set; }
    }
}
