using System.Collections.Generic;
using IDI.Core.Common;
using IDI.Core.Infrastructure;
using IDI.Core.Infrastructure.Messaging;
using IDI.Core.Tests.TestUtils;
using IDI.Core.Tests.TestUtils.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IDI.Core.Tests.Infrastructure.Commands
{
    [TestClass]
    [TestCategory(Contants.TestCategory.Command)]
    public class CommandUnitTests
    {
        public ICommandBus CommandBus { get; private set; }

        public CommandUnitTests()
        {
            this.CommandBus = Runtime.GetService<ICommandBus>();
        }

        [TestMethod]
        public void TestCommandBus_Execute_Fail_ForNullParameter()
        {
            var result = this.CommandBus.Send(default(TestCommand));

            Assert.AreEqual(ResultStatus.Error, result.Status);
            Assert.AreEqual("命令参数不能为空!", result.Message);
        }

        [TestMethod]
        public void TestCommandBus_Execute_Fail_ForInvalidParameter()
        {
            var command = new TestCommand(field: "");
            var result = this.CommandBus.Send(command);

            Assert.AreEqual(ResultStatus.Fail, result.Status);
            Assert.AreEqual("命令参数验证失败!", result.Message);
            Assert.AreEqual(1, result.Details.Count);

            var errors = result.Details["errors"] as List<string>;

            Assert.AreEqual(2, errors.Count);
            Assert.AreEqual("'测试字段'必填!", errors[0]);
            Assert.AreEqual("'测试字段'最多5到10个字符!", errors[1]);
        }

        [TestMethod]
        public void TestCommandBus_Execute_Fail_ForValidParameter_WithoutCommandHandler()
        {
            var command = new TestCommand(field: "123456");
            var result = this.CommandBus.Send(command);

            Assert.AreEqual(ResultStatus.Error, result.Status);
            Assert.AreEqual("未找到任何相关命令处理器!", result.Message);
            Assert.AreEqual(1, result.Details.Count);
            Assert.IsTrue(result.Details.ContainsKey("errors"));

            var errors = result.Details["errors"] as List<string>;

            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void TestCommandBus_Execute_Success_ForValidParameter_WithCommandHandler()
        {
            var command = new ChangeFieldCommand(field: "123456");
            var result = this.CommandBus.Send(command);

            Assert.AreEqual(ResultStatus.Success, result.Status);
            Assert.AreEqual("执行成功!", result.Message);
            Assert.AreEqual(0, result.Details.Count);
        }
    }
}
