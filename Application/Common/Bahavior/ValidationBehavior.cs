using Application.Authentication.Commands;
using Application.Authentication.Common;
using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.Common.Bahavior;



public class ValidationBehavior<TRequest, TResponse> :
 IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : ResultBase
{
    private readonly IValidator<TRequest>? _validator;

    public ValidationBehavior(IValidator<TRequest>? validator)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var validationsResult = await _validator!.ValidateAsync(request);

        if (validationsResult.IsValid)
            return await next();


        return (dynamic)Result.Fail(validationsResult.Errors.ConvertAll(x => new Error(x.ErrorMessage).CausedBy(x.PropertyName)));
    }
}