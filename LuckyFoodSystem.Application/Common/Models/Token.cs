using Newtonsoft.Json;

namespace LuckyFoodSystem.Application.Common.Models
{
    public class Token
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; } = null!;

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; } = null!;

        [JsonProperty("scope")]
        public string Scope { get; set; } = null!;
    }
}
