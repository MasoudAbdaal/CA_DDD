using Domain.Entities;

namespace Application.Services.Authentication;

public record AuthenticationResult
(
    User user,
    string Token
);