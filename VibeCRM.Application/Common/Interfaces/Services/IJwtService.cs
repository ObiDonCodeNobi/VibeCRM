using System.Security.Claims;

namespace VibeCRM.Application.Common.Interfaces.Services;

/// <summary>
/// Interface for JWT token generation and validation services.
/// </summary>
public interface IJwtService
{
    /// <summary>
    /// Generates a JWT token for a user based on their claims.
    /// </summary>
    /// <param name="claims">The claims to include in the token.</param>
    /// <returns>The generated JWT token string.</returns>
    string GenerateToken(IEnumerable<Claim> claims);

    /// <summary>
    /// Generates a refresh token for extending session validity.
    /// </summary>
    /// <returns>A randomly generated refresh token string.</returns>
    string GenerateRefreshToken();

    /// <summary>
    /// Validates a JWT token and returns the principal if valid.
    /// </summary>
    /// <param name="token">The JWT token to validate.</param>
    /// <param name="validateLifetime">Whether to validate the token's lifetime.</param>
    /// <returns>The ClaimsPrincipal if the token is valid, null otherwise.</returns>
    ClaimsPrincipal? ValidateToken(string token, bool validateLifetime = true);

    /// <summary>
    /// Gets the expiration date of a JWT token.
    /// </summary>
    /// <returns>The expiration date of newly generated tokens.</returns>
    DateTime GetTokenExpirationTime();

    /// <summary>
    /// Gets the expiration date for refresh tokens.
    /// </summary>
    /// <returns>The expiration date for newly generated refresh tokens.</returns>
    DateTime GetRefreshTokenExpirationTime();
}
