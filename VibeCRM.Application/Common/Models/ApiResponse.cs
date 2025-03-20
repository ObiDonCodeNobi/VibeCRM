namespace VibeCRM.Application.Common.Models;

/// <summary>
/// Represents a standardized API response format for all API endpoints.
/// </summary>
/// <typeparam name="T">The type of data being returned in the response.</typeparam>
public class ApiResponse<T>
{
    /// <summary>
    /// Gets or sets a value indicating whether the API request was successful.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Gets or sets a message providing additional information about the response.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the data payload of the response.
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// Gets or sets a list of error messages if the request was not successful.
    /// </summary>
    public List<string>? Errors { get; set; }

    /// <summary>
    /// Creates a successful response with the specified data and message.
    /// </summary>
    /// <param name="data">The data to include in the response.</param>
    /// <param name="message">An optional message describing the successful operation.</param>
    /// <returns>A new ApiResponse instance indicating success.</returns>
    public static ApiResponse<T> CreateSuccess(T data, string message = "Operation completed successfully")
    {
        return new ApiResponse<T>
        {
            Success = true,
            Message = message,
            Data = data
        };
    }

    /// <summary>
    /// Creates a failed response with the specified error messages.
    /// </summary>
    /// <param name="message">A message describing the failure.</param>
    /// <param name="errors">An optional list of specific error messages.</param>
    /// <returns>A new ApiResponse instance indicating failure.</returns>
    public static ApiResponse<T> CreateFailure(string message, List<string>? errors = null)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Message = message,
            Errors = errors
        };
    }
}
