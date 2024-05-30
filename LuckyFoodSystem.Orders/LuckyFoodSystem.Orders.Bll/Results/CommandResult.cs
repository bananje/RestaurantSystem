using System.Net;

namespace LuckyFoodSystem.Orders.Bll.Results;

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

    public string Message { get; private set; }

    public object SuccessObject { get; private set; }

    public static CommandResult Success(string message = null!)
        => new CommandResult(HttpStatusCode.OK, message);

    public static CommandResult Failure(string message = null!)
        => new CommandResult(HttpStatusCode.BadRequest, message);
}
