using System;
using System.Collections.Generic;
using IDI.Central.Tests.Utils;
using IDI.Core.Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IDI.Central.Tests
{
    [TestClass]
    public class IdentityModuleUnitTest
    {
        private const string API_TOKEN = "api/token";

        [TestMethod]
        public void IdentityModule_Can_GetToken_ByPassword()
        {
            var token = HttpUtil.Instance.Post(API_TOKEN, new Dictionary<string, string>
            {
                { "grant_type", "password" },{ "username", "admin" },{ "password", "123456" }
            }).ToDynamic();

            Assert.IsNotNull(token.access_token);
            Assert.IsNotNull(token.token_type);
            Assert.AreEqual(token.token_type.Value, "bearer");
            Assert.IsNotNull(token.expires_in);

            Console.WriteLine(token.ToJson());
        }

        [TestMethod]
        public void IdentityModule_Can_GetToken_ByClientCredentials()
        {
            var token = HttpUtil.Instance.Post(API_TOKEN, new Dictionary<string, string> { { "grant_type", "client_credentials" } }).ToDynamic();

            Assert.IsNotNull(token.access_token);
            Assert.IsNotNull(token.token_type);
            Assert.AreEqual(token.token_type.Value, "bearer");
            Assert.IsNotNull(token.expires_in);

            Console.WriteLine(token.ToJson());
        }
    }
}
