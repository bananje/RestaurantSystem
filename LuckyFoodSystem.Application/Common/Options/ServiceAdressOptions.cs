namespace LuckyFoodSystem.Application.Common.Options
{
    public class ServiceAdressOptions
    {
        public const string SectionName = nameof(ServiceAdressOptions);
        public string IdentityServer { get; set; } = null!;
        public string UserManagementService { get; set; } = null!;
    }
}
