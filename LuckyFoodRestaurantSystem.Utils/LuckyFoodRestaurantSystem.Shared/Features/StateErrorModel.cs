using System.Net;

namespace LuckyFoodSystem.Shared.Features;

public class StateErrorModel
{
    public HttpStatusCode ErrorCode { get; private set; }

    public string Reason { get; private set; } = string.Empty;
}
