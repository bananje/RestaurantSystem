using ErrorOr;
using FluentValidation;
using MediatR;

namespace LuckyFoodSystem.Application.Common.Behaviors
{
    public class ValidationBehaivor<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IErrorOr
    {
        private readonly IValidator<TRequest>? _validator;
        public ValidationBehaivor(IValidator<TRequest>? validator = null)
        {
            _validator = validator;
        }       

        public async Task<TResponse> Handle(TRequest request, 
                                      RequestHandlerDelegate<TResponse> next, 
                                      CancellationToken cancellationToken)
        {
            if (_validator is null)
            {
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
}
