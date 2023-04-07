using FluentResults;
namespace Application.Services.Authentication;

public interface IAuthenticationService
{
    AuthenticationResult Login(string email, string password);
    Result<AuthenticationResult> Register(string name, string lastName, string email, string password);
    //Use OneOf For ErrorHandling
    // OneOf<AuthenticationResult, DuplicateEmailError> Register(string name, string lastName, string email, string password);
}