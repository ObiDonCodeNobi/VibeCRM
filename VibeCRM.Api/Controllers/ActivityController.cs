using MediatR;
using Microsoft.AspNetCore.Mvc;
using VibeCRM.Application.Common.Models;
using VibeCRM.Application.Features.Activity.Commands.CreateActivity;
using VibeCRM.Application.Features.Activity.Commands.DeleteActivity;
using VibeCRM.Application.Features.Activity.Commands.UpdateActivity;
using VibeCRM.Application.Features.Activity.DTOs;
using VibeCRM.Application.Features.Activity.Queries.GetActivityById;
using VibeCRM.Application.Features.Activity.Queries.GetAllActivities;

namespace VibeCRM.Api.Controllers;

/// <summary>
/// API endpoints for managing activity resources.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ActivityController : ApiControllerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ActivityController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for sending commands and queries.</param>
    /// <param name="logger">The logger instance for logging controller actions.</param>
    public ActivityController(IMediator mediator, ILogger<ActivityController> logger)
        : base(mediator, logger)
    {
    }

    /// <summary>
    /// Creates a new activity.
    /// </summary>
    /// <param name="command">The activity creation details.</param>
    /// <returns>The newly created activity.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<ActivityDetailsDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateActivityCommand command)
    {
        _logger.LogInformation("Creating new Activity with Subject: {Subject}", command.Subject);

        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = result.ActivityId }, Success(result, "Activity created successfully"));
    }

    /// <summary>
    /// Updates an existing activity.
    /// </summary>
    /// <param name="id">The ID of the activity to update.</param>
    /// <param name="command">The updated activity details.</param>
    /// <returns>The updated activity.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<ActivityDetailsDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateActivityCommand command)
    {
        if (id != command.ActivityId)
        {
            return BadRequestResponse<ActivityDetailsDto>("ID in the URL does not match the ID in the request body");
        }

        _logger.LogInformation("Updating Activity with ID: {Id}", id);

        var result = await _mediator.Send(command);

        return Ok(Success(result, "Activity updated successfully"));
    }

    /// <summary>
    /// Deletes an activity by its ID.
    /// </summary>
    /// <param name="id">The ID of the activity to delete.</param>
    /// <returns>A success response if deletion was successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting Activity with ID: {Id}", id);

        var command = new DeleteActivityCommand
        {
            ActivityId = id,
            ModifiedBy = Guid.Parse(User.Identity?.Name ?? Guid.Empty.ToString())
        };
        var result = await _mediator.Send(command);

        return Ok(Success(result, "Activity deleted successfully"));
    }

    /// <summary>
    /// Gets an activity by its ID.
    /// </summary>
    /// <param name="id">The ID of the activity to retrieve.</param>
    /// <returns>The activity details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<ActivityDetailsDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Getting Activity with ID: {Id}", id);

        var query = new GetActivityByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<ActivityDetailsDto>($"Activity with ID {id} not found");
        }

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a list of all activities.
    /// </summary>
    /// <returns>A list of all activities.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ActivityListDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all Activities");

        var query = new GetAllActivitiesQuery();
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }
}