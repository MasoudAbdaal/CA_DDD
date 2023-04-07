using System.Net;

namespace Application.Common.Errors;

public interface IServiceExceptions
{
    public HttpStatusCode StatusCode { get; }
    public string ErrorMessage { get;}
}