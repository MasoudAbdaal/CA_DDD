using Application.Authentication.Common;
using FluentResults;
using MediatR;

namespace Application.Authentication.Commands
{
    public record RegisterCommand
    (
        string Name,
        string LastName,
        string Email,
        string Password) : IRequest<Result<AuthenticationResult>>;
}