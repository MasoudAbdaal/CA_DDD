using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;

public class ErrorHandlingFilterAttributes : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var exception = context.Exception;

        var problemDetauls = new ProblemDetails()
        {
            Type = "Go to RFC PAge 500!",
            Title = "An error happened",
            Status = (int)HttpStatusCode.InternalServerError,


        };
        context.Result = new ObjectResult(problemDetauls);
        // context.Result = new ObjectResult(new { Error = exception.Message, Context = context.HttpContext.ToString() })
        // {
        //     StatusCode = StatusCodes.Status501NotImplemented,

        // };
        context.ExceptionHandled = true;
    }
}