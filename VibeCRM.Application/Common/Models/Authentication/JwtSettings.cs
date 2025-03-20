namespace VibeCRM.Application.Common.Models.Authentication;

/// <summary>
/// Represents the JWT authentication settings used by the application.
/// </summary>
public class JwtSettings
{
    /// <summary>
    /// Gets or sets the secret key used to sign JWT tokens.
    /// </summary>
    public string SecretKey { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the issuer of the JWT token.
    /// </summary>
    public string Issuer { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the audience of the JWT token.
    /// </summary>
    public string Audience { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the expiry time in minutes for JWT tokens.
    /// </summary>
    public int ExpiryInMinutes { get; set; } = 60;

    /// <summary>
    /// Gets or sets the expiry time in days for refresh tokens.
    /// </summary>
    public int RefreshTokenExpiryInDays { get; set; } = 7;
}