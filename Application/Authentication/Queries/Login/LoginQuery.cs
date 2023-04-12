using Application.Authentication.Common;
using FluentResults;
using MediatR;

namespace Application.Authentication.Queries.Login;

public record LoginQuery(string email, string password) : IRequest<Result<AuthenticationResult>>;