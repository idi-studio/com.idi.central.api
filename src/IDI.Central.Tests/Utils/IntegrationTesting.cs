﻿using System.Collections.Generic;
using IDI.Core.Authentication.TokenAuthentication;
using IDI.Core.Common;
using IDI.Core.Common.Extensions;

namespace IDI.Central.Tests.Utils
{
    public abstract class IntegrationTesting
    {
        public void SignIn(string username = "administrator", string password = "p@55w0rd")
        {
            var json = Post("api/token", new Dictionary<string, string>
            {
                { "grant_type", Constants.AuthenticationMethod.Password },{ "username", username },{ "password", password }
            });

            var result = json.To<Result<TokenModel>>();

            if (result != null && result.Status == ResultStatus.Success)
            {
                HttpUtil.Instance.SetToken(result.Data.Token);
            }
        }

        public string Post(string url, Dictionary<string, string> parameters)
        {
            return HttpUtil.Instance.Post(url, parameters);
        }

        public string Post(string url, object parameter)
        {
            return HttpUtil.Instance.Post(url, parameter);
        }

        public string Get(string url)
        {
            return HttpUtil.Instance.Get(url);
        }
    }
}
