using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

[ApiController]
public class ApiController : ControllerBase
{
    protected IActionResult Problem(List<IError> errors)
    {
        if (errors[0] is Result_DuplicateEmailError X)
            return Problem(title: X.Message, statusCode: X.StatusCode, detail: errors.ToString());


        var modelStateDictionary = new ModelStateDictionary();
        foreach (var item in errors)
        {
            modelStateDictionary.AddModelError(item.Reasons[0].Message, item.Message);
        }

        return ValidationProblem(modelStateDictionary);

        return Problem(
            statusCode: StatusCodes.Status500InternalServerError,
            detail: errors[0].Message,
            title: String.Join("  ", "Invalid ", errors[0].Reasons[0].Message));
    }
}