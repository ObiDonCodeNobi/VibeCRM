using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using VibeCRM.Application.Common.Interfaces.Services;
using VibeCRM.Application.Common.Models.Authentication;

namespace VibeCRM.Infrastructure.Services;

/// <summary>
/// Service for generating and validating JWT tokens.
/// </summary>
public class JwtService : IJwtService
{
    private readonly JwtSettings _jwtSettings;
    private readonly SigningCredentials _signingCredentials;
    private readonly TokenValidationParameters _tokenValidationParameters;

    /// <summary>
    /// Initializes a new instance of the <see cref="JwtService"/> class.
    /// </summary>
    /// <param name="jwtSettings">The JWT configuration settings.</param>
    public JwtService(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;

        var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);
        var securityKey = new SymmetricSecurityKey(key);
        _signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        _tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _jwtSettings.Issuer,
            ValidAudience = _jwtSettings.Audience,
            IssuerSigningKey = securityKey,
            ClockSkew = TimeSpan.Zero
        };
    }

    /// <summary>
    /// Generates a JWT token for a user based on their claims.
    /// </summary>
    /// <param name="claims">The claims to include in the token.</param>
    /// <returns>The generated JWT token string.</returns>
    public string GenerateToken(IEnumerable<Claim> claims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = GetTokenExpirationTime(),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials = _signingCredentials
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    /// <summary>
    /// Generates a refresh token for extending session validity.
    /// </summary>
    /// <returns>A randomly generated refresh token string.</returns>
    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    /// <summary>
    /// Validates a JWT token and returns the principal if valid.
    /// </summary>
    /// <param name="token">The JWT token to validate.</param>
    /// <param name="validateLifetime">Whether to validate the token's lifetime.</param>
    /// <returns>The ClaimsPrincipal if the token is valid, null otherwise.</returns>
    public ClaimsPrincipal? ValidateToken(string token, bool validateLifetime = true)
    {
        if (string.IsNullOrEmpty(token))
            return null;

        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            var parameters = _tokenValidationParameters.Clone();
            parameters.ValidateLifetime = validateLifetime;

            var principal = tokenHandler.ValidateToken(token, parameters, out var validatedToken);

            if (validatedToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                return null;
            }

            return principal;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Gets the expiration date of a JWT token.
    /// </summary>
    /// <returns>The expiration date of newly generated tokens.</returns>
    public DateTime GetTokenExpirationTime()
    {
        return DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes);
    }

    /// <summary>
    /// Gets the expiration date for refresh tokens.
    /// </summary>
    /// <returns>The expiration date for newly generated refresh tokens.</returns>
    public DateTime GetRefreshTokenExpirationTime()
    {
        return DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryInDays);
    }
}