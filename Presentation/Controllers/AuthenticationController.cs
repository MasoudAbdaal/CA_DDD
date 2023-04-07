using System.Net;
using Application.Services.Authentication;
using Contracts.Authentication;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using OneOf;

namespace Presentation.Controllers;


[Route("auth")]
// [ErrorHandlingFilterAttributes]
public class AuthenticationController : ApiController
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {


        Result<AuthenticationResult> registerResult = _authenticationService.Register(request.Name, request.LastName, request.Email, request.Password);

        if (registerResult.IsSuccess)
            return Ok(registerResult.Value);

        // if (registerResult.Errors[0] is Result_DuplicateEmailError)
        //     return Problem(title: registerResult.Errors[0].Message, statusCode: StatusCodes.Status409Conflict);

        return Problem(registerResult.Errors);


        // OneOf<AuthenticationResult, DuplicateEmailError> registerResult = _authenticationService.Register(request.Name, request.LastName, request.Email, request.Password);

        // return registerResult.Match(
        //     authResult => Ok(authResult),
        //     error => Problem(statusCode: ((int)error.StatusCode), title: error.Message)
        // );
        //Not Very Clean way!
        // if (registerResult.IsT0)
        //     return Ok(registerResult.AsT0);

        // return Problem(statusCode: ((int)registerResult.AsT1.StatusCode), title: registerResult.AsT1.Message);

    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var authResult = _authenticationService.Login(request.Email, request.Password);
        return Ok(authResult);
    }

}