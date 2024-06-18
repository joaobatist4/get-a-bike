using FluentResults;
using FluentValidation;
using MediatR;

namespace GetABike.Infra;

public class ValidatorPipeline<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : class
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var validationResult = validators
            .Select(v => v.Validate(request))
            .FirstOrDefault();

        if (validationResult is { IsValid: false, Errors: var errors })
        {
            var response = (TResponse)Activator.CreateInstance(typeof(TResponse))!;
            var method = response.GetType().GetMethod(nameof(Result.WithError), new[] { typeof(string) })!;
            return (TResponse)method.Invoke(response, [errors[0].ErrorMessage])!;
        }

        return await next();
    }
}