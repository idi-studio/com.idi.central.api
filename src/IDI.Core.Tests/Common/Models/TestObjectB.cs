using IDI.Core.Infrastructure.Verification;
using IDI.Core.Infrastructure.Verification.Attributes;

namespace IDI.Core.Tests.Common.Models
{
    public class TestObjectB : IVerifiable
    {
        [StringLength("测试字段", MinLength = 5, MaxLength = 10)]
        public string Field { get; set; }
    }
}
