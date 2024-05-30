using ErrorOr;
using LuckyFoodSystem.Orders.Bll.Results;
using MediatR;
using Microsoft.Extensions.Logging;

public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;

    public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var response = await next();

        if (response is ErrorOr<CommandResult> result)
        {
            if (result.IsError)
            {
                foreach (var error in result.Errors)
                {
                    _logger.LogError($"Error: {error.Code} - {error.Description}");
                }
            }
            else
            {
                var commandResult = result.Value;
                _logger.LogInformation($"Command Result Message: {commandResult.Message}");
            }
        }

        return response;
    }
}

