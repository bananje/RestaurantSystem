using System.Net;

namespace LuckyFoodSystem.OrdersDelivery.Bll.Results;

public class CommandResult
{
    private CommandResult(
        HttpStatusCode statusCode,
        string message)
    {
        HttpStatusCode = statusCode;
        Message = message;
    }

    private CommandResult(object successObject)
    {
        SuccessObject = successObject;
    }    

    public HttpStatusCode HttpStatusCode { get; private set; }

    public string Message { get; private set; } = string.Empty;

    public object SuccessObject { get; private set; } = string.Empty;

    public static CommandResult Success(HttpStatusCode httpStatusCode = HttpStatusCode.OK, string message = null!)
        => new CommandResult(httpStatusCode, message);
}
