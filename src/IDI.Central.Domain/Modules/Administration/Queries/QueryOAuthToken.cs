using System;
using System.Collections.Generic;
using IDI.Central.Common;
using IDI.Central.Common.Enums;
using IDI.Central.Domain.Localization;
using IDI.Central.Models.OAuth;
using IDI.Core.Common;
using IDI.Core.Common.Extensions;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Infrastructure.Utils;
using IDI.Core.Infrastructure.Verification.Attributes;

namespace IDI.Central.Domain.Modules.Administration.Queries
{
    public class QueryOAuthTokenCondition : Condition
    {
        [RequiredField]
        public string Code { get; set; }

        public string RedirectUri { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public OAuthType Type { get; set; }
    }

    public class QueryOAuthToken : Query<QueryOAuthTokenCondition, OAuthTokenModel>
    {
        private readonly Dictionary<OAuthType, Func<QueryOAuthTokenCondition, OAuthTokenModel>> func = new Dictionary<OAuthType, Func<QueryOAuthTokenCondition, OAuthTokenModel>>
        {
            { OAuthType.GitHub,GitHub }, { OAuthType.Wechat,Wechat }, { OAuthType.Alipay,Alipay }
        };

        public override Result<OAuthTokenModel> Execute(QueryOAuthTokenCondition condition)
        {
            var result = func[condition.Type](condition);

            if (result != null && !result.AccessToken.IsNull())
                return Result.Success(result);

            if (result != null && result.AccessToken.IsNull())
                return Result.Fail<OAuthTokenModel>(result.ErrorDesc);

            return Result.Fail<OAuthTokenModel>(Localization.Get(Resources.Key.Command.AuthFail));
        }

        private static GitHubTokenModel GitHub(QueryOAuthTokenCondition condition)
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
                redirect_uri = condition.RedirectUri,
                state = condition.State
            };

            return Http.Instance.Post("https://github.com", "/login/oauth/access_token", param).To<GitHubTokenModel>();
        }

        private static OAuthTokenModel Wechat(QueryOAuthTokenCondition condition)
        {
            throw new NotImplementedException();
        }

        private static OAuthTokenModel Alipay(QueryOAuthTokenCondition condition)
        {
            throw new NotImplementedException();
        }
    }
}
