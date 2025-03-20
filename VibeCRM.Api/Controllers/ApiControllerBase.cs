using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Common.Models;

namespace VibeCRM.Api.Controllers;

/// <summary>
/// Base controller class that provides common functionality for all API controllers.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public abstract class ApiControllerBase : ControllerBase
{
    /// <summary>
    /// The mediator instance for sending commands and queries.
    /// </summary>
    protected readonly IMediator _mediator;
    
    /// <summary>
    /// The logger instance for logging controller actions.
    /// </summary>
    protected readonly Microsoft.Extensions.Logging.ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiControllerBase"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for sending commands and queries.</param>
    /// <param name="logger">The logger instance for logging controller actions.</param>
    protected ApiControllerBase(IMediator mediator, Microsoft.Extensions.Logging.ILogger logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Creates a successful response with the specified data and message.
    /// </summary>
    /// <typeparam name="T">The type of data being returned.</typeparam>
    /// <param name="data">The data to include in the response.</param>
    /// <param name="message">An optional message describing the successful operation.</param>
    /// <returns>A new ApiResponse instance indicating success.</returns>
    protected ApiResponse<T> Success<T>(T data, string message = "Operation completed successfully")
    {
        return ApiResponse<T>.CreateSuccess(data, message);
    }

    /// <summary>
    /// Creates a failed response with the specified error messages.
    /// </summary>
    /// <typeparam name="T">The type of data being returned.</typeparam>
    /// <param name="message">A message describing the failure.</param>
    /// <param name="errors">An optional list of specific error messages.</param>
    /// <returns>A new ApiResponse instance indicating failure.</returns>
    protected ApiResponse<T> Failure<T>(string message, List<string>? errors = null)
    {
        return ApiResponse<T>.CreateFailure(message, errors);
    }

    /// <summary>
    /// Creates a not found response with the specified message.
    /// </summary>
    /// <typeparam name="T">The type of data being returned.</typeparam>
    /// <param name="message">A message describing what was not found.</param>
    /// <returns>A NotFoundObjectResult containing an ApiResponse.</returns>
    protected IActionResult NotFoundResponse<T>(string message = "Resource not found")
    {
        return NotFound(ApiResponse<T>.CreateFailure(message));
    }

    /// <summary>
    /// Creates a bad request response with the specified message and errors.
    /// </summary>
    /// <typeparam name="T">The type of data being returned.</typeparam>
    /// <param name="message">A message describing the bad request.</param>
    /// <param name="errors">An optional list of specific error messages.</param>
    /// <returns>A BadRequestObjectResult containing an ApiResponse.</returns>
    protected IActionResult BadRequestResponse<T>(string message = "Invalid request", List<string>? errors = null)
    {
        return BadRequest(ApiResponse<T>.CreateFailure(message, errors));
    }
}
