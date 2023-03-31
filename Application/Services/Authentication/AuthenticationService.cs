using Application.Common.Interfaces.Authentication;

namespace Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJWTTokenGenerator _jWTTokenGenerator;

    public AuthenticationService(IJWTTokenGenerator jWTTokenGenerator)
    {
        _jWTTokenGenerator = jWTTokenGenerator;
    }

    public AuthenticationResult Login(string email, string password)
    {
        return new AuthenticationResult(Guid.NewGuid(), "token!");
    }

    public AuthenticationResult Register(string name, string lastName, string email, string password)
    {
        Guid userId = Guid.NewGuid();
        var token = _jWTTokenGenerator.GenerateToken(userId, name, lastName);

        return new AuthenticationResult(userId, token);
    }
}