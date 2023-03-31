namespace Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    public AuthenticationResult Login(string email, string password)
    {
        return new AuthenticationResult(Guid.NewGuid(), "token!");
    }

    public AuthenticationResult Register(string name, string lastName, string email, string password)
    {
        return new AuthenticationResult(Guid.NewGuid(), "token!");
    }
}