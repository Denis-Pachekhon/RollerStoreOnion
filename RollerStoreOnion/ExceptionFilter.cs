using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;
using RollerStore.Core.Exeptions;

namespace RollerStoreOnion
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var statusCode = HttpStatusCode.InternalServerError;

            if (context.Exception is ValidationException)
            {
                statusCode = HttpStatusCode.BadRequest;
            }
            
            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)statusCode;
            context.ExceptionHandled = true;
        }
    }
}
