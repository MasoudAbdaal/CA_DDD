using FluentValidation;

namespace Application.Authentication.Commands;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{

    public RegisterCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}