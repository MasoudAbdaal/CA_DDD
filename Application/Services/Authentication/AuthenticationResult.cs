namespace Application.Services.Authentication;

public record AuthenticationResult
(
    Guid Id,
    string Token
);