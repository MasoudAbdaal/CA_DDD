using System.Net;

namespace Application.Common.Errors;

public class DuplicateEmailException : Exception, IServiceExceptions
{
    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

    public string ErrorMessage => "Email Already Exist";
    
}