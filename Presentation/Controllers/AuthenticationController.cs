using System.Net;
using Application.Authentication.Commands;
using Application.Authentication.Common;
using Application.Authentication.Queries.Login;
using Contracts.Authentication;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace Presentation.Controllers;


[Route("auth")]
// [ErrorHandlingFilterAttributes]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediator;

    public AuthenticationController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = new RegisterCommand(request.Name, request.LastName, request.Email, request.Password);


        Result<AuthenticationResult> registerResult = await _mediator.Send(command);

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
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = new LoginQuery(request.Email, request.Password);

        var authResult = await _mediator.Send(query);
        return Ok(authResult.Value);
    }

}