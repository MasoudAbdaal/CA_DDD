using System.Net;
using FluentResults;

public record DuplicateEmailError
{
    public string Message = "Email Already Exist!";
    public HttpStatusCode StatusCode = HttpStatusCode.Conflict;
}

public class Result_DuplicateEmailError : IError
{
    public int StatusCode => (int)HttpStatusCode.Conflict;

    public List<IError> Reasons => throw new NotImplementedException();

    public string Message => "RESULT !!! EMAIL ALREADY EXIST!! ";

    public Dictionary<string, object> Metadata => throw new NotImplementedException();
}