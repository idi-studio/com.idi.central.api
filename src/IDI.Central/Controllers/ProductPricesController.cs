using System;
using IDI.Central.Domain.Modules.Retailing.Queries;
using IDI.Central.Models.Retailing;
using IDI.Central.Providers;
using IDI.Core.Common;
using IDI.Core.Common.Basetypes;
using IDI.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/product/prices"), ApplicationAuthorize]
    public class ProductPricesController : Controller
    {
        // GET: api/product/prices/{id}
        [HttpGet("{id}")]
        public Result<Collection<ProductPriceModel>> Get(Guid id)
        {
            var condition = new QueryProductPricesCondition { ProductId = id };

            return ServiceLocator.QueryProcessor.Execute<QueryProductPricesCondition, Collection<ProductPriceModel>>();
        }
    }
}
