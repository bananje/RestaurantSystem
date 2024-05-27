namespace LuckyFoodSystem.Orders.Bll.Results;

public class CommandResult
{
    private CommandResult(object successObject)
    {
        SuccessObject = successObject;
    }    

    public object SuccessObject { get; private set; }
}
