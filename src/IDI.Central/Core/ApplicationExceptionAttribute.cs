using IDI.Core.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace IDI.Central.Core
{
    public class ApplicationExceptionAttribute : ExceptionFilterAttribute
    {
        private readonly IHostingEnvironment environment;
        private readonly IModelMetadataProvider metadata;
        private readonly ILogger logger;

        public ApplicationExceptionAttribute(IHostingEnvironment environment, IModelMetadataProvider metadata, ILogger logger)
        {
            this.environment = environment;
            this.metadata = metadata;
            this.logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            if (!environment.IsDevelopment())
                return;

            logger.Error(context.Exception.Message, context.Exception);
            //this.logger.LogError(context.Exception, context.Exception.Message, context.HttpContext.Request.AsJson());

            context.HttpContext.InternalServerError(context.Exception);

            context.Exception = null;
        }
    }
}
