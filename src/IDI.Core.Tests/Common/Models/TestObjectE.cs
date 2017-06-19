using IDI.Core.Infrastructure.Verification;
using IDI.Core.Infrastructure.Verification.Attributes;

namespace IDI.Core.Tests.Common.Models
{
    internal class TestObjectE : IVerifiable
    {
        [StringLength("测试字段")]
        public string Field { get; set; }
    }
}
