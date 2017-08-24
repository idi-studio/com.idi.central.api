﻿using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.Verification.Attributes;

namespace IDI.Core.Tests.TestUtils.Models
{
    public class TestObjectD : Command
    {
        [StringLength("测试字段", MinLength = 5)]
        public string Field { get; set; }
    }
}