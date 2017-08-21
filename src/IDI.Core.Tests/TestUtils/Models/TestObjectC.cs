using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.Verification.Attributes;

namespace IDI.Core.Tests.TestUtils.Models
{
    public class TestObjectC : Command
    {
        [StringLength("测试字段", MaxLength = 10)]
        public string Field { get; set; }
    }
}
