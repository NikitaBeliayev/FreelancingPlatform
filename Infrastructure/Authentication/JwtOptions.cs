namespace Infrastructure.Authentication
{
    public sealed class JwtOptions
    {
        public string Issuer { get; set; } = string.Empty;
        public int RefreshTokenLifeTimeInDays { get; set; } = default;

        public int AccessTokenLifeTimeInMinutes { get; set; } = default;
        public string SecretKey { get; set; } = string.Empty;
    }
}
