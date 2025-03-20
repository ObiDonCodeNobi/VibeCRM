using MediatR;
using Microsoft.AspNetCore.Mvc;
using VibeCRM.Application.Common.Models;
using VibeCRM.Application.Features.ActivityStatus.Commands.CreateActivityStatus;
using VibeCRM.Application.Features.ActivityStatus.Commands.DeleteActivityStatus;
using VibeCRM.Application.Features.ActivityStatus.Commands.UpdateActivityStatus;
using VibeCRM.Application.Features.ActivityStatus.DTOs;
using VibeCRM.Application.Features.ActivityStatus.Queries.GetActivityStatusById;
using VibeCRM.Application.Features.ActivityStatus.Queries.GetActivityStatusByOrdinalPosition;
using VibeCRM.Application.Features.ActivityStatus.Queries.GetActivityStatusByStatus;
using VibeCRM.Application.Features.ActivityStatus.Queries.GetAllActivityStatuses;
using VibeCRM.Application.Features.ActivityStatus.Queries.GetCompletedActivityStatuses;
using VibeCRM.Application.Features.ActivityStatus.Queries.GetDefaultActivityStatus;

namespace VibeCRM.Api.Controllers;

/// <summary>
/// API endpoints for managing activity status reference data.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ActivityStatusController : ApiControllerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ActivityStatusController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for sending commands and queries.</param>
    /// <param name="logger">The logger instance for logging controller actions.</param>
    public ActivityStatusController(IMediator mediator, ILogger<ActivityStatusController> logger)
        : base(mediator, logger)
    {
    }

    /// <summary>
    /// Creates a new activity status.
    /// </summary>
    /// <param name="command">The activity status creation details.</param>
    /// <returns>The newly created activity status.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<ActivityStatusDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateActivityStatusCommand command)
    {
        _logger.LogInformation("Creating new Activity Status with Status: {Status}", command.Status);

        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = result }, Success(result, "Activity Status created successfully"));
    }

    /// <summary>
    /// Updates an existing activity status.
    /// </summary>
    /// <param name="id">The ID of the activity status to update.</param>
    /// <param name="command">The updated activity status details.</param>
    /// <returns>The updated activity status.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<ActivityStatusDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateActivityStatusCommand command)
    {
        if (command.Id != id)
        {
            return BadRequestResponse<ActivityStatusDto>("ID in the URL does not match the ID in the request body");
        }

        _logger.LogInformation("Updating Activity Status with ID: {Id}", id);

        var result = await _mediator.Send(command);

        return Ok(Success(result, "Activity Status updated successfully"));
    }

    /// <summary>
    /// Deletes an activity status by its ID.
    /// </summary>
    /// <param name="id">The ID of the activity status to delete.</param>
    /// <returns>A success response if deletion was successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting Activity Status with ID: {Id}", id);

        var command = new DeleteActivityStatusCommand { Id = id };
        var result = await _mediator.Send(command);

        return Ok(Success(result, "Activity Status deleted successfully"));
    }

    /// <summary>
    /// Gets an activity status by its ID.
    /// </summary>
    /// <param name="id">The ID of the activity status to retrieve.</param>
    /// <returns>The activity status details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<ActivityStatusDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Getting Activity Status with ID: {Id}", id);

        var query = new GetActivityStatusByIdQuery { Id = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<ActivityStatusDto>($"Activity Status with ID {id} not found");
        }

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a list of all activity statuses.
    /// </summary>
    /// <returns>A list of all activity statuses.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ActivityStatusDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all Activity Statuses");

        var query = new GetAllActivityStatusesQuery();
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets activity statuses by status name.
    /// </summary>
    /// <param name="status">The status name to search for.</param>
    /// <returns>A list of activity statuses matching the specified status name.</returns>
    [HttpGet("bystatus/{status}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ActivityStatusDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByStatus(string status)
    {
        _logger.LogInformation("Getting Activity Statuses with Status: {Status}", status);

        var query = new GetActivityStatusByStatusQuery { Status = status };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets activity statuses by ordinal position.
    /// </summary>
    /// <param name="position">The ordinal position to search for.</param>
    /// <returns>A list of activity statuses with the specified ordinal position.</returns>
    [HttpGet("byposition/{position:int}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ActivityStatusDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByOrdinalPosition(int position)
    {
        _logger.LogInformation("Getting Activity Statuses with Ordinal Position: {Position}", position);

        var query = new GetActivityStatusByOrdinalPositionQuery();
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets the default activity status.
    /// </summary>
    /// <returns>The default activity status.</returns>
    [HttpGet("default")]
    [ProducesResponseType(typeof(ApiResponse<ActivityStatusDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDefault()
    {
        _logger.LogInformation("Getting default Activity Status");

        var query = new GetDefaultActivityStatusQuery();
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<ActivityStatusDto>("Default Activity Status not found");
        }

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets all completed activity statuses.
    /// </summary>
    /// <returns>A list of all completed activity statuses.</returns>
    [HttpGet("completed")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ActivityStatusDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCompleted()
    {
        _logger.LogInformation("Getting all completed Activity Statuses");

        var query = new GetCompletedActivityStatusesQuery();
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }
}