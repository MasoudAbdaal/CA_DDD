using Application.Authentication.Commands;
using Application.Authentication.Queries.Login;
using Contracts.Authentication;
using Mapster;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<LoginRequest, LoginQuery>()
        .Map(dest => dest.email, src => src.Email);

        config.NewConfig<RegisterRequest, RegisterCommand>()
        .Map(dest => dest.Email, src => src.Email);
    }
}