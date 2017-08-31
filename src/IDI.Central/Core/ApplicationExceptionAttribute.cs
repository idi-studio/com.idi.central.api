using IDI.Core.Common;
using IDI.Core.Common.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

namespace IDI.Central.Core
{
    public class ApplicationExceptionAttribute : ExceptionFilterAttribute
    {
        private readonly IHostingEnvironment environment;
        private readonly IModelMetadataProvider metadata;
        private readonly ILogger logger;

        public ApplicationExceptionAttribute(IHostingEnvironment environment, IModelMetadataProvider metadata, ILoggerFactory factory)
        {
            this.environment = environment;
            this.metadata = metadata;
            this.logger = factory.CreateLogger(Constants.LoggerCategory.Error);
        }

        public override void OnException(ExceptionContext context)
        {
            if (!environment.IsDevelopment())
                return;

            this.logger.LogError(context.Exception, context.Exception.Message, context.HttpContext.Request.AsJson());

            context.HttpContext.InternalServerError(context.Exception);

            context.Exception = null;
        }
    }
}
