using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Bll.Common.Behaivors;

public class ValidationBehaivor<TRequest, TResponse> :
     IPipelineBehavior<TRequest, TResponse>
     where TRequest : IRequest<TResponse>
     where TResponse : IErrorOr
{
    private readonly IValidator<TRequest>? _validator;

    private readonly ILogger<ValidationBehaivor<TRequest, TResponse>> _logger;

    public ValidationBehaivor(
        ILogger<ValidationBehaivor<TRequest, TResponse>> logger,
        IValidator<TRequest>? validator = null)
    {
        _validator = validator;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request,
                                  RequestHandlerDelegate<TResponse> next,
                                  CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Исполнение {typeof(TRequest).Name}");

        if (_validator is null)
        {
            _logger.LogError($"Валидатор {typeof(IValidator).Name} недоступен");

            return await next();
        }

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid)
        {
            return await next();
        }

        var errors = validationResult.Errors
                     .ConvertAll(validFailure => Error.Validation(validFailure.PropertyName, validFailure.ErrorMessage));

        return (dynamic)errors;
    }
}
