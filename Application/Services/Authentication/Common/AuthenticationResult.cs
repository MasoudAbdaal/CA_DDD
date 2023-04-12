using Domain.Entities;

namespace Application.Services.Authentication.Common;

public record AuthenticationResult
(
    User user,
    string Token
);