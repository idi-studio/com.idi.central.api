﻿using IDI.Central.Domain.Modules.Administration.Commands;
using IDI.Core.Common;
using IDI.Core.Infrastructure;
using IDI.Core.Localization;
using IDI.Core.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IDI.Central.Domain.Tests
{
    [TestClass]
    public class InfrastructureUnitTests : IntegrationUnitTest
    {
        [TestMethod]
        public void Should_Initialize_Database_Success()
        {
            var hanlder = new DatabaseInitalCommandHandler();
            hanlder.Localization = new Globalization();
            hanlder.Transaction = Runtime.GetService<ITransaction>();

            var result = hanlder.Execute(new DatabaseInitalCommand());

            Assert.AreEqual(ResultStatus.Success, result.Status);
        }
    }
}
