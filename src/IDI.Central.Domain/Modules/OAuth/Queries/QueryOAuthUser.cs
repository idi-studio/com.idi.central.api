using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using IDI.Central.Common.Enums;
using IDI.Central.Domain.Localization;
using IDI.Central.Models.OAuth;
using IDI.Core.Common;
using IDI.Core.Common.Extensions;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Infrastructure.Utils;
using IDI.Core.Infrastructure.Verification.Attributes;

namespace IDI.Central.Domain.Modules.BasicInfo.Queries
{
    public class QueryOAuthUserCondition : Condition
    {
        [RequiredField]
        public string AccessToken { get; set; }

        public OAuthType Type { get; set; }
    }

    public class QueryOAuthUser : Query<QueryOAuthUserCondition, OAuthUserModel>
    {
        private readonly Dictionary<OAuthType, Func<QueryOAuthUserCondition, OAuthUserModel>> func = new Dictionary<OAuthType, Func<QueryOAuthUserCondition, OAuthUserModel>>
        {
            { OAuthType.GitHub,GitHub }, { OAuthType.Wechat,Wechat }, { OAuthType.Alipay,Alipay }
        };

        public override Result<OAuthUserModel> Execute(QueryOAuthUserCondition condition)
        {
            var result = func[condition.Type](condition);

            if (result != null)
                return Result.Success(result);

            return Result.Fail<OAuthUserModel>(Localization.Get(Resources.Key.Command.RetrieveUserInfoFail));
        }

        private static GitHubUserModel GitHub(QueryOAuthUserCondition condition)
        {
            return Http.Instance.Get("https://api.github.com", $"/user?access_token={condition.AccessToken}", header =>
            {
                header.UserAgent.Add(new ProductInfoHeaderValue("Chrome", "61.0.3163.100"));
                header.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }).To<GitHubUserModel>();
        }

        private static OAuthUserModel Wechat(QueryOAuthUserCondition condition)
        {
            throw new NotImplementedException();
        }

        private static OAuthUserModel Alipay(QueryOAuthUserCondition condition)
        {
            throw new NotImplementedException();
        }
    }
}
