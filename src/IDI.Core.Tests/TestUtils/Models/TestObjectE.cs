using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.Verification.Attributes;

namespace IDI.Core.Tests.TestUtils.Models
{
    internal class TestObjectE : Command
    {
        [StringLength(DisplayName = "测试字段")]
        public string Field { get; set; }
    }
}
