using IDI.Core.Infrastructure.Verification;
using IDI.Core.Infrastructure.Verification.Attributes;

namespace IDI.Core.Tests.Utils.Models
{
    public class TestObjectD : IVerifiable
    {
        [StringLength("测试字段", MinLength = 5)]
        public string Field { get; set; }
    }
}
