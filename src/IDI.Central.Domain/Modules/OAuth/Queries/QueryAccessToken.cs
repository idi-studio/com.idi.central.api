using System;
using System.Collections.Generic;
using IDI.Central.Common;
using IDI.Central.Common.Enums;
using IDI.Central.Models.OAuth;
using IDI.Core.Common;
using IDI.Core.Common.Extensions;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Infrastructure.Utils;
using IDI.Core.Infrastructure.Verification.Attributes;

namespace IDI.Central.Domain.Modules.BasicInfo.Queries
{
    public class QueryAccessTokenCondition : Condition
    {
        [RequiredField]
        public string Code { get; set; }

        public string RedirectUrl { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public OAuthType Type { get; set; }
    }

    public class QueryAccessToken : Query<QueryAccessTokenCondition, AccessTokenModel>
    {
        private readonly Dictionary<OAuthType, Func<QueryAccessTokenCondition, AccessTokenModel>> func = new Dictionary<OAuthType, Func<QueryAccessTokenCondition, AccessTokenModel>>
        {
            { OAuthType.GitHub,GitHub }, { OAuthType.Wechat,Wechat }, { OAuthType.Alipay,Alipay }
        };

        public override Result<AccessTokenModel> Execute(QueryAccessTokenCondition condition)
        {
            var result = func[condition.Type](condition);

            if (result != null)
                return Result.Success(result);


            return Result.Fail<AccessTokenModel>("");
        }

        private static AccessTokenModel GitHub(QueryAccessTokenCondition condition)
        {
            // client_id	    string	Required. The client ID you received from GitHub for your GitHub App.
            // client_secret	string	Required. The client secret you received from GitHub for your GitHub App.
            // code	            string	Required. The code you received as a response to Step 1.
            // redirect_uri	    string	The URL in your application where users are sent after authorization.
            // state	        string	The unguessable random string you provided in Step 1.
            var param = new
            {
                client_id = Configuration.OAuthApplication.GitHub.ClientId,
                client_secret = Configuration.OAuthApplication.GitHub.ClientSecret,
                code = condition.Code,
                redirect_uri = condition.RedirectUrl,
                state = condition.State
            };

            return Http.Instance.Post("https://github.com", "/login/oauth/access_token", param).To<AccessTokenModel>();
        }

        private static AccessTokenModel Wechat(QueryAccessTokenCondition condition)
        {
            throw new NotImplementedException();
        }

        private static AccessTokenModel Alipay(QueryAccessTokenCondition condition)
        {
            throw new NotImplementedException();
        }
    }
}
