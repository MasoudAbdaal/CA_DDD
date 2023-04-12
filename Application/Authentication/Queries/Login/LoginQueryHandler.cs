

using Application.Authentication.Commands;
using Application.Authentication.Common;
using Application.Authentication.Queries.Login;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistance;
using Domain.Entities;
using FluentResults;
using MediatR;

public class LoginQueryHandler : IRequestHandler<LoginQuery, Result<AuthenticationResult>>
{
    private readonly IJWTTokenGenerator _jWTTokenGenerator;
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(IUserRepository userRepository, IJWTTokenGenerator jWTTokenGenerator)
    {
        _userRepository = userRepository;
        _jWTTokenGenerator = jWTTokenGenerator;
    }

    public async Task<Result<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        if (_userRepository.GetUserByEmail(query.email) is not User user)
            throw new Exception("User not found with this email!");

        if (user.Password != query.password)
            throw new Exception("Invalid Credential");

        var token = await Task.Run(() => _jWTTokenGenerator.GenerateToken(user));

        return new AuthenticationResult(user, token);
    }
}