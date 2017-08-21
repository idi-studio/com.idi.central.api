using System.Collections.Generic;
using IDI.Core.Common;
using IDI.Core.Infrastructure;
using IDI.Core.Tests.TestUtils;
using IDI.Core.Tests.TestUtils.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IDI.Core.Tests.Infrastructure.Commands
{
    [TestClass]
    [TestCategory(Contants.TestCategory.Command)]
    public class CommandUnitTests
    {
        [TestMethod]
        public void TestCommandBus_Execute_Fail_ForNullParameter()
        {
            var result = ServiceLocator.CommandBus.Send(default(TestCommand));

            Assert.AreEqual(ResultStatus.Error, result.Status);
            Assert.AreEqual("命令参数不能为空!", result.Message);
        }

        [TestMethod]
        public void TestCommandBus_Execute_Fail_ForInvalidParameter()
        {
            var command = new TestCommand(field: "");
            var result = ServiceLocator.CommandBus.Send(command);

            Assert.AreEqual(ResultStatus.Fail, result.Status);
            Assert.AreEqual("命令参数验证失败!", result.Message);
            Assert.AreEqual(1, result.Details.Count);

            var info = result.Details["info"] as List<string>;

            Assert.AreEqual(2, info.Count);
            Assert.AreEqual("'测试字段'必填!", info[0]);
            Assert.AreEqual("'测试字段'最多5到10个字符!", info[1]);
        }

        [TestMethod]
        public void TestCommandBus_Execute_Fail_ForValidParameter_WithoutCommandHandler()
        {
            var command = new TestCommand(field: "123456");
            var result = ServiceLocator.CommandBus.Send(command);

            Assert.AreEqual(ResultStatus.Error, result.Status);
            Assert.AreEqual("未找到任何相关命令处理器!", result.Message);
            Assert.AreEqual(1, result.Details.Count);
            Assert.IsTrue(result.Details.ContainsKey("info"));

            var info = result.Details["info"] as List<string>;

            Assert.AreEqual(0, info.Count);  
        }

        [TestMethod]
        public void TestCommandBus_Execute_Success_ForValidParameter_WithCommandHandler()
        {
            var command = new ChangeFieldCommand(field: "123456");
            var result = ServiceLocator.CommandBus.Send(command);

            Assert.AreEqual(ResultStatus.Success, result.Status);
            Assert.AreEqual("执行成功!", result.Message);
            Assert.AreEqual(0, result.Details.Count);
        }
    }
}
