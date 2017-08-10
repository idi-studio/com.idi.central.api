using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.Verification.Attributes;

namespace IDI.Core.Tests.Utils.Models
{
    internal class TestObjectE : Command
    {
        [StringLength("测试字段")]
        public string Field { get; set; }
    }
}
