namespace LuckyFoodSystem.Application.Common.Options
{
    public class IdentityServerApiOptions
    {
        public const string SectionName = nameof(IdentityServerApiOptions);
        public string ClientId { get; set; } = null!;
        public string ClientSecret { get; set; } = null!;
        public string Scope { get; set; } = null!;
        public string GrantType { get; set; } = null!;
    }
}
