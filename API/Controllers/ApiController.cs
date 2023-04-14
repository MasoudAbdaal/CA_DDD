using FluentResults;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class ApiController : ControllerBase
{
    protected IActionResult Problem(List<IError> errors)
    {
        if (errors[0] is Result_DuplicateEmailError X)
            return Problem(title: X.Message, statusCode: X.StatusCode, detail: errors.ToString());


        return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: errors.ToString());
    }
}