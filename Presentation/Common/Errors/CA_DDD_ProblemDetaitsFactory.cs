using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

public class CustomProblemDetails : ProblemDetails
{
    public string? CustomProblemDetailProperty { get; set; }
}

public class CA_DDD_ProblemDetailsFactory : ProblemDetailsFactory
{

    public override ProblemDetails CreateProblemDetails(HttpContext httpContext, int? statusCode = null, string? title = null, string? type = null, string? detail = null, string? instance = null)
    {

        var details = new CustomProblemDetails
        {
            //Type & other properties will return null here and we cannot use default implementatoin of this!
            //    public class MyProblemDetailsFactory : DefaultProblemDetailsFactory
            //!! We couldnt use above code because DefaultProblemDetailsFactory has internal protected level
            Status = statusCode,
            Title = title,
            CustomProblemDetailProperty = "Overrided From CA_DDD_ProblemDetaulsFactory",
            Detail = detail,
            Instance = instance,
            Type = type

        };

        return details;
    }

    public override ValidationProblemDetails CreateValidationProblemDetails(HttpContext httpContext, ModelStateDictionary modelStateDictionary, int? statusCode = null, string? title = null, string? type = null, string? detail = null, string? instance = null)
    {
        throw new NotImplementedException();
    }
}
