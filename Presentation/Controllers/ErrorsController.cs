using System.Linq.Expressions;
using Application.Common.Errors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        IExceptionHandlerFeature exception = HttpContext.Features.Get<IExceptionHandlerFeature>()!;

        var (statusCode, message) = exception.Error switch
        {
            IServiceExceptions serviceExceptions => ((int)serviceExceptions.StatusCode, serviceExceptions.ErrorMessage),
            _ => (StatusCodes.Status500InternalServerError, "Unhandled Error!!")
        };

        return Problem(title: message, statusCode: statusCode);
    }
}