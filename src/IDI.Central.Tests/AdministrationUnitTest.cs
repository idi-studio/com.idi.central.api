using System;
using System.Collections.Generic;
using IDI.Central.Tests.Utils;
using IDI.Core.Authentication.TokenAuthentication;
using IDI.Core.Common;
using IDI.Core.Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IDI.Central.Tests
{
    [TestClass]
    public class AdministrationUnitTest
    {
        private const string API_TOKEN = "api/token";

        [TestMethod]
        public void Administration_Can_GetToken_ByPassword()
        {
            var json = HttpUtil.Instance.Post(API_TOKEN, new Dictionary<string, string>
            {
                { "grant_type", "password" },{ "username", "admin" },{ "password", "p@55w0rd" }
            });

            Assert.IsNotNull(json);

            Console.WriteLine(json);

            var result = json.To<Result<TokenModel>>();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(result.Status, ResultStatus.Success);
            Assert.AreEqual(result.Data.TokenType, "bearer");
            Assert.IsTrue(result.Data.ExpiresIn > 0);
        }

        [TestMethod]
        public void Administration_Can_GetToken_ByClientCredentials()
        {
            var json = HttpUtil.Instance.Post(API_TOKEN, new Dictionary<string, string> { { "grant_type", Constants.GrantType.ClientCredentials } });

            Assert.IsNotNull(json);

            Console.WriteLine(json);

            var result = json.To<Result<TokenModel>>();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(result.Status, ResultStatus.Success);
            Assert.AreEqual(result.Data.TokenType, Constants.TokenType.Bearer);
            Assert.IsTrue(result.Data.ExpiresIn > 0);
        }
    }
}
