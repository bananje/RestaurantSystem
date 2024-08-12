namespace LuckyFoodSystem.Shared.Domain.Features;

public interface IAsyncBusinessRule
{
    string ErrorMessage { get; }
}

public interface IAsyncBusinessRule<T> : IAsyncBusinessRule where T : class
{
    public Task<bool> IsBrokenAsync(T data);
}
