using System.Net;
using System.Text.Json;
using VibeCRM.Application.Common.Exceptions;
using VibeCRM.Shared.Models;

namespace VibeCRM.Api.Middleware
{
    /// <summary>
    /// Middleware for handling exceptions globally across the API.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IWebHostEnvironment _environment;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHandlingMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="environment">The hosting environment information.</param>
        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger,
            IWebHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        /// <summary>
        /// Invokes the middleware.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "An unhandled exception occurred");
                await HandleExceptionAsync(context, exception);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = new ApiResponse<object>
            {
                Success = false,
                Message = "An error occurred while processing your request.",
                Data = null,
                Errors = new List<string>()
            };

            var statusCode = HttpStatusCode.InternalServerError;

            switch (exception)
            {
                case FluentValidation.ValidationException validationException:
                    statusCode = HttpStatusCode.BadRequest;
                    response.Message = "Validation failed";
                    response.Errors = validationException.Errors.Select(e => e.ErrorMessage).ToList();
                    break;

                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    response.Message = notFoundException.Message;
                    break;

                case BadRequestException badRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    response.Message = badRequestException.Message;
                    break;

                case UnauthorizedAccessException:
                    statusCode = HttpStatusCode.Unauthorized;
                    response.Message = "Unauthorized access";
                    break;

                default:
                    // For security reasons, don't expose detailed error information in production
                    if (_environment.IsDevelopment())
                    {
                        response.Message = exception.Message;
                        response.Errors = new List<string> { exception.StackTrace ?? "No stack trace available" };
                    }
                    else
                    {
                        response.Errors.Add("An unexpected error occurred. Please try again later.");
                    }
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(response, options);
            await context.Response.WriteAsync(json);
        }
    }
}