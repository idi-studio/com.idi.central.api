using IDI.Central.Core;
using IDI.Central.Domain.Modules.Retailing.Queries;
using IDI.Central.Models.Retailing;
using IDI.Core.Common;
using IDI.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/cust"), ApplicationAuthorize]
    public class CustomerController : Controller
    {
        [HttpGet("list")]
        public Result<Set<CustomerModel>> List()
        {
            return ServiceLocator.QueryProcessor.Execute<QueryCustomerSetCondition, Set<CustomerModel>>();
        }
    }
}
