﻿using IDI.Core.Infrastructure.Verification;
using IDI.Core.Infrastructure.Verification.Attributes;

namespace IDI.Core.Tests.Utils.Models
{
    public class TestObjectA : IVerifiable
    {
        [RequiredField("测试字段")]
        public string Field { get; set; }
    }
}