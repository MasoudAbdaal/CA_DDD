using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Authentication;

public class JWTTokenGenerator : IJWTTokenGenerator
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly JWTSettings _jWTSettings;

    public JWTTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JWTSettings> jwtOptions)
    {
        _dateTimeProvider = dateTimeProvider;
        _jWTSettings = jwtOptions.Value;
    }

    public string GenerateToken(Guid userId, string firstName, string lastName)
    {
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jWTSettings.Secret)), SecurityAlgorithms.HmacSha256);

        var claims = new[]{
            new Claim(JwtRegisteredClaimNames.Sub,userId.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName,firstName),
            new Claim(JwtRegisteredClaimNames.FamilyName,lastName),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
        };

        var securityToken = new JwtSecurityToken(
            issuer: _jWTSettings.Issuer,
            expires: _dateTimeProvider.UTCNow.AddMinutes(_jWTSettings.ExpiryMinutes),
            claims: claims,
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}