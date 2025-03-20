using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VibeCRM.Application.Common.Interfaces.Services;
using VibeCRM.Application.Common.Models;
using VibeCRM.Application.Common.Models.Authentication;

namespace VibeCRM.Api.Controllers;

/// <summary>
/// Controller for handling authentication-related operations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IJwtService _jwtService;
    private readonly ILogger<AuthController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthController"/> class.
    /// </summary>
    /// <param name="jwtService">The JWT service for token generation and validation.</param>
    /// <param name="logger">The logger for recording authentication activities.</param>
    public AuthController(IJwtService jwtService, ILogger<AuthController> logger)
    {
        _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Authenticates a user and generates a JWT token.
    /// </summary>
    /// <param name="request">The login credentials.</param>
    /// <returns>A JWT token if authentication is successful.</returns>
    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<AuthResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        // TODO: Implement actual user authentication against database
        // This is a placeholder implementation for demonstration purposes

        // For demo purposes, accept any username with password "password"
        if (request.Password != "password")
        {
            _logger.LogWarning("Failed login attempt for user: {Username}", request.Username);
            return Unauthorized(new ApiResponse<object>
            {
                Success = false,
                Message = "Invalid username or password"
            });
        }

        _logger.LogInformation("Successful login for user: {Username}", request.Username);

        // Create claims for the token
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, request.Username),
            new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            // Add roles and other claims as needed
            new Claim(ClaimTypes.Role, "User")
        };

        // Generate tokens
        var token = _jwtService.GenerateToken(claims);
        var refreshToken = _jwtService.GenerateRefreshToken();
        var tokenExpiration = _jwtService.GetTokenExpirationTime();
        var refreshTokenExpiration = _jwtService.GetRefreshTokenExpirationTime();

        // TODO: Store the refresh token in the database associated with the user

        var response = new AuthResponse
        {
            Token = token,
            RefreshToken = refreshToken,
            ExpiresAt = tokenExpiration,
            Username = request.Username
        };

        return Ok(new ApiResponse<AuthResponse>
        {
            Success = true,
            Message = "Authentication successful",
            Data = response
        });
    }

    /// <summary>
    /// Refreshes an expired JWT token using a valid refresh token.
    /// </summary>
    /// <param name="request">The refresh token request.</param>
    /// <returns>A new JWT token if the refresh token is valid.</returns>
    [HttpPost("refresh-token")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<AuthResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
    public IActionResult RefreshToken([FromBody] RefreshTokenRequest request)
    {
        // Validate the JWT token without checking its expiration
        var principal = _jwtService.ValidateToken(request.Token, validateLifetime: false);

        if (principal == null)
        {
            return Unauthorized(new ApiResponse<object>
            {
                Success = false,
                Message = "Invalid token"
            });
        }

        // TODO: Validate the refresh token against the database
        // This is a placeholder implementation

        var username = principal.Identity?.Name;
        if (string.IsNullOrEmpty(username))
        {
            return Unauthorized(new ApiResponse<object>
            {
                Success = false,
                Message = "Invalid token"
            });
        }

        _logger.LogInformation("Refreshing token for user: {Username}", username);

        // Generate new tokens
        var token = _jwtService.GenerateToken(principal.Claims);
        var refreshToken = _jwtService.GenerateRefreshToken();
        var tokenExpiration = _jwtService.GetTokenExpirationTime();
        var refreshTokenExpiration = _jwtService.GetRefreshTokenExpirationTime();

        // TODO: Update the refresh token in the database

        var response = new AuthResponse
        {
            Token = token,
            RefreshToken = refreshToken,
            ExpiresAt = tokenExpiration,
            Username = username
        };

        return Ok(new ApiResponse<AuthResponse>
        {
            Success = true,
            Message = "Token refreshed successfully",
            Data = response
        });
    }
}