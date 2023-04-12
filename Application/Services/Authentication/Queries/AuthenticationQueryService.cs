using System.Diagnostics;
using Application.Authentication.Common;
using Application.Common.Errors;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistance;
using Domain.Entities;
using FluentResults;
using OneOf;

namespace Application.Services.Authentication.Queries;

[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public class AuthenticationQueryService : IAuthenticationQueryService
{
    private readonly IJWTTokenGenerator _jWTTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationQueryService(IJWTTokenGenerator jWTTokenGenerator, IUserRepository userRepository)
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

    // public OneOf<AuthenticationResult, DuplicateEmailError> Register(string name, string lastName, string email, string password)
    // {
    //     if (_userRepository.GetUserByEmail(email) is User)
    //         return new DuplicateEmailError();

    //     var user = new User
    //     {
    //         Email = email,
    //         FirstName = name,
    //         LastName = lastName,
    //         Password = password
    //     };

    //     var token = _jWTTokenGenerator.GenerateToken(user);

    //     _userRepository.Add(user);

    //     return new AuthenticationResult(user, token);
    // }

    // Result<AuthenticationResult> IAuthenticationCommandService.Register(string name, string lastName, string email, string password)
    // {
    //     if (_userRepository.GetUserByEmail(email) is User)
    //         return Result.Fail<AuthenticationResult>(new[]
    //         {
    //             new Result_DuplicateEmailError(),
    //             new Result_DuplicateEmailError(),
    //             new Result_DuplicateEmailError()
    //         });

    //     var user = new User
    //     {
    //         Email = email,
    //         FirstName = name,
    //         LastName = lastName,
    //         Password = password
    //     };

    //     var token = _jWTTokenGenerator.GenerateToken(user);

    //     _userRepository.Add(user);

    //     return new AuthenticationResult(user, token);
    // }

    private string GetDebuggerDisplay() => ToString()!;
}