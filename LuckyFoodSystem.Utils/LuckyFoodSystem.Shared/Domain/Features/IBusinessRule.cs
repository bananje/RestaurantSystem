namespace LuckyFoodSystem.Shared.Domain.Features
{
    public interface IBusinessRule
    {
        /// <summary>
        /// Исполняется ли бизнес-правило
        /// </summary>
        bool IsBroken();

        /// <summary>
        /// Ошибка исполнения бизнес-правила
        /// </summary>
        string ErrorMessage { get; }
    }
}
