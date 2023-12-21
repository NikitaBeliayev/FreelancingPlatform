namespace Infrastructure.Authentication
{
    public sealed class JwtOptions
    {
        public string Issuer { get; set; } = string.Empty;
        public int RefreshTokenLifeTimeInDays { get; set; } = default(int);

        public int AccessTokenLifeTimeInMinutes { get; set; } = default(int);
        public string SecretKey { get; set; } = string.Empty;
    }
}
