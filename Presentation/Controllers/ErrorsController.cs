using System.Linq.Expressions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        IExceptionHandlerFeature exception = HttpContext.Features.Get<IExceptionHandlerFeature>()!;

        return Problem(title: exception?.Error.Message);
    }
}