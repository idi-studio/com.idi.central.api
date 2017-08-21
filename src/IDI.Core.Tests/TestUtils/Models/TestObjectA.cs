using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.Verification.Attributes;

namespace IDI.Core.Tests.TestUtils.Models
{
    public class TestObjectA : Command
    {
        [RequiredField("测试字段")]
        public string Field { get; set; }

    }
}
