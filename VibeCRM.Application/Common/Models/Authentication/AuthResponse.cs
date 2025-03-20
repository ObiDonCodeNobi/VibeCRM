namespace VibeCRM.Application.Common.Models.Authentication;

/// <summary>
/// Represents the response returned after successful authentication.
/// </summary>
public class AuthResponse
{
    /// <summary>
    /// Gets or sets the JWT token for API authorization.
    /// </summary>
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the refresh token used to obtain a new JWT token when the current one expires.
    /// </summary>
    public string RefreshToken { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the expiration date and time of the JWT token.
    /// </summary>
    public DateTime ExpiresAt { get; set; }

    /// <summary>
    /// Gets or sets the username of the authenticated user.
    /// </summary>
    public string Username { get; set; } = string.Empty;
}
