using Application.Services.Authentication.Common;
using FluentResults;
namespace Application.Common.Interfaces.Authentication;

public interface IAuthenticationCommandService
{
    Result<AuthenticationResult> Register(string name, string lastName, string email, string password);
    // AuthenticationResult Login(string email, string password);
    //Use OneOf For ErrorHandling
    // OneOf<AuthenticationResult, DuplicateEmailError> Register(string name, string lastName, string email, string password);
}