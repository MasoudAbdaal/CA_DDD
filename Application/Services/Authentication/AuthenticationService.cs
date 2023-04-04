using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistance;
using Domain.Entities;

namespace Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJWTTokenGenerator _jWTTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJWTTokenGenerator jWTTokenGenerator, IUserRepository userRepository)
    {
        _jWTTokenGenerator = jWTTokenGenerator;
        _userRepository = userRepository;
    }

    public AuthenticationResult Login(string email, string password)
    {
        if (_userRepository.GetUserByEmail(email) is not User user)
            throw new Exception("User not found with this email!");

        if (user.Password != password)
            throw new Exception("Invalid Credential");

        var token = _jWTTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }

    public AuthenticationResult Register(string name, string lastName, string email, string password)
    {
        if (_userRepository.GetUserByEmail(email) is User)
            throw new Exception("User Already EXIST!!");

        var user = new User
        {
            Email = email,
            FirstName = name,
            LastName = lastName,
            Password = password
        };

        var token = _jWTTokenGenerator.GenerateToken(user);
        
        _userRepository.Add(user);

        return new AuthenticationResult(user, token);
    }
}