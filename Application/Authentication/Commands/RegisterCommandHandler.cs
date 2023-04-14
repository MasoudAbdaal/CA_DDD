

using Application.Authentication.Commands;
using Application.Authentication.Common;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistance;
using Domain.Entities;
using FluentResults;
using MediatR;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<AuthenticationResult>>
{
    private readonly IJWTTokenGenerator _jWTTokenGenerator;
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(IUserRepository userRepository, IJWTTokenGenerator jWTTokenGenerator)
    {
        _userRepository = userRepository;
        _jWTTokenGenerator = jWTTokenGenerator;
    }

    public async Task<Result<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        if (_userRepository.GetUserByEmail(command.Email) is User)
            return Result.Fail<AuthenticationResult>(new[]
            {
                new Result_DuplicateEmailError(),
                new Result_DuplicateEmailError(),
                new Result_DuplicateEmailError()
            });

        var user = new User
        {
            Email = command.Email,
            FirstName = command.Name,
            LastName = command.LastName,
            Password = command.Password
        };

        var token = _jWTTokenGenerator.GenerateToken(user);
        _userRepository.Add(user);

        var result = new AuthenticationResult(user, token);

        return await Task.FromResult<Result<AuthenticationResult>>(result);
    }
}