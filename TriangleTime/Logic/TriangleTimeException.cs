using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
//using System.Web.Http.Filters;

namespace TriangleTime.Logic
{
    public class TriangleTimeException : Exception
    {
        public TriangleTimeException(HttpStatusCode statusCode, string errorCode, string errorDescription) : base(
            $"{errorCode}::{errorDescription}")
        {
            StatusCode = statusCode;
        }

        public TriangleTimeException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; }
    }

    public class TriangleTimeExceptionFilterAttribute : ExceptionFilterAttribute
    {
        //public override void OnException(HttpActionExecutedContext context)
        //{
        //    var exception = context.Exception as TriangleTimeException;
        //    if (exception != null)
        //    {
        //        context.Response = context.Request.CreateErrorResponse(
        //            exception.StatusCode, exception.Message);
        //    }
        //}
    }


}

