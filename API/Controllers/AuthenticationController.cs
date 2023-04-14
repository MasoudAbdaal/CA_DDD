using System.Net;
using Application.Authentication.Commands;
using Application.Authentication.Common;
using Application.Authentication.Queries.Login;
using Contracts.Authentication;
using FluentResults;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers;


[Route("auth")]
// [ErrorHandlingFilterAttributes]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public AuthenticationController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        RegisterCommand command = _mapper.Map<RegisterCommand>(request);

        Result<AuthenticationResult> registerResult = await _mediator.Send(command);

        if (registerResult.IsSuccess)
            return Ok(registerResult.Value);

        return Problem(registerResult.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        LoginQuery query = _mapper.Map<LoginQuery>(request);

        var authResult = await _mediator.Send(query);
        return Ok(authResult.Value);
    }

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