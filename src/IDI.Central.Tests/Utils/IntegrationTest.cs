using System.Collections.Generic;
using IDI.Core.Authentication.TokenAuthentication;
using IDI.Core.Common;
using IDI.Core.Common.Extensions;

namespace IDI.Central.Tests.Utils
{
    public abstract class IntegrationTest
    {
        public void SignIn(string username = "administrator", string password = "p@55w0rd")
        {
            var json = Post("api/token", new Dictionary<string, string>
            {
                { "grant_type", Constants.GrantType.Password },{ "username", username },{ "password", password }
            });

            var result = json.To<Result<TokenModel>>();

            if (result != null && result.Status == ResultStatus.Success)
            {
                HttpUtil.Instance.SetToken(result.Data.Token);
            }
        }

        public string Post(string url, Dictionary<string, string> parameters = null)
        {
            return HttpUtil.Instance.Post(url, parameters);
        }

        public string Post(string url, object parameter)
        {
            return HttpUtil.Instance.Post(url, parameter);
        }
    }
}
