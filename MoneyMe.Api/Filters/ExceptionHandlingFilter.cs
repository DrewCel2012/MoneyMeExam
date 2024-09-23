using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MoneyMe.Common.Helpers;
using MoneyMe.Model;
using System.Net;

namespace MoneyMe.Api.Filters
{
    public sealed class ExceptionHandlingFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionHandlingFilter> _logger;

        public ExceptionHandlingFilter(ILogger<ExceptionHandlingFilter> logger)
        {
            _logger = logger;
        }


        public void OnException(ExceptionContext context)
        {
            ExceptionDetails exceptionDetail = new()
            {
                DateTime = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"),
                HasError = true,
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = context.Exception.Message,
                StackTrace = context.Exception.StackTrace
            };

            context.HttpContext.Response.StatusCode = exceptionDetail.StatusCode;
            context.Result = new JsonResult(exceptionDetail);
            context.ExceptionHandled = true;


            //_logger.LogError(exceptionDetail.ToString());
            //_logger.LogError(context.Exception, context.Exception.Message);
            LogHelper.LogException(context.Exception);
        }
    }
}
