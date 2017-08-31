using IDI.Central.Domain.Modules.Administration.Queries;
using IDI.Central.Models.Administration;
using IDI.Central.Core;
using IDI.Core.Common;
using IDI.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/user/profile"), ApplicationAuthorize]
    public class UserProfileController : Controller
    {
        // GET api/user/profile/{username}
        [HttpGet("{username}")]
        public Result<MyProfile> Get(string username)
        {
            var condition = new QueryMyProfileCondition { UserName = username };

            return ServiceLocator.QueryProcessor.Execute<QueryMyProfileCondition, MyProfile>(condition);
        }
    }
}
