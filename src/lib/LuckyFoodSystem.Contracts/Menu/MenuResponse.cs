namespace LuckyFoodSystem.Contracts.Menu
{
    public record MenuResponse(
            List<MenuResponseObject> Response);

    public record MenuResponseObject(
            Guid MenuId,
            string Name,
            string CategoryName,
            List<string> Files);
}
