using System;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.Verification.Attributes;

namespace IDI.Core.Tests.TestUtils.Commands
{
    public class TestCommand : Command
    {
        [RequiredField("测试字段")]
        [StringLength("测试字段", MinLength = 5, MaxLength = 10)]
        public string Field { get; set; }

        public TestCommand(string field)
        {
            this.Field = field;
        }
    }
}
