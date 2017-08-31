using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.Verification.Attributes;

namespace IDI.Core.Tests.TestUtils.Models
{
    public class TestObjectD : Command
    {
        [StringLength(DisplayName = "测试字段", MinLength = 5)]
        public string Field { get; set; }
    }
}
