using Domain.Entities;
namespace Application.Common.Interfaces.Authentication;

public interface IJWTTokenGenerator
{
    string GenerateToken(User user);
}