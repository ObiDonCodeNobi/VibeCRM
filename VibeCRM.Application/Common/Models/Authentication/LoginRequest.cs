namespace VibeCRM.Application.Common.Models.Authentication;

/// <summary>
/// Represents a user login request containing credentials.
/// </summary>
public class LoginRequest
{
    /// <summary>
    /// Gets or sets the username for authentication.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the password for authentication.
    /// </summary>
    public string Password { get; set; } = string.Empty;
}
