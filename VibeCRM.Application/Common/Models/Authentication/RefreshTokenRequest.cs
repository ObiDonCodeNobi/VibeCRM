namespace VibeCRM.Application.Common.Models.Authentication;

/// <summary>
/// Represents a request to refresh an expired JWT token using a refresh token.
/// </summary>
public class RefreshTokenRequest
{
    /// <summary>
    /// Gets or sets the expired JWT token.
    /// </summary>
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the refresh token used to generate a new JWT token.
    /// </summary>
    public string RefreshToken { get; set; } = string.Empty;
}