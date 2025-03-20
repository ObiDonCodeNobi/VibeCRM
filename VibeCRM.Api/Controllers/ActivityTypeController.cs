using MediatR;
using Microsoft.AspNetCore.Mvc;
using VibeCRM.Application.Common.Models;
using VibeCRM.Application.Features.ActivityType.Commands.CreateActivityType;
using VibeCRM.Application.Features.ActivityType.Commands.DeleteActivityType;
using VibeCRM.Application.Features.ActivityType.Commands.UpdateActivityType;
using VibeCRM.Application.Features.ActivityType.DTOs;
using VibeCRM.Application.Features.ActivityType.Queries.GetActivityTypeById;
using VibeCRM.Application.Features.ActivityType.Queries.GetActivityTypeByOrdinalPosition;
using VibeCRM.Application.Features.ActivityType.Queries.GetActivityTypeByType;
using VibeCRM.Application.Features.ActivityType.Queries.GetAllActivityTypes;
using VibeCRM.Application.Features.ActivityType.Queries.GetDefaultActivityType;

namespace VibeCRM.Api.Controllers;

/// <summary>
/// API endpoints for managing activity type reference data.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ActivityTypeController : ApiControllerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ActivityTypeController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for sending commands and queries.</param>
    /// <param name="logger">The logger instance for logging controller actions.</param>
    public ActivityTypeController(IMediator mediator, ILogger<ActivityTypeController> logger)
        : base(mediator, logger)
    {
    }

    /// <summary>
    /// Creates a new activity type.
    /// </summary>
    /// <param name="command">The activity type creation details.</param>
    /// <returns>The newly created activity type.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<ActivityTypeDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateActivityTypeCommand command)
    {
        _logger.LogInformation("Creating new Activity Type with Type: {Type}", command.Type);

        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, Success(result, "Activity Type created successfully"));
    }

    /// <summary>
    /// Updates an existing activity type.
    /// </summary>
    /// <param name="id">The ID of the activity type to update.</param>
    /// <param name="command">The updated activity type details.</param>
    /// <returns>The updated activity type.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<ActivityTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateActivityTypeCommand command)
    {
        if (id != command.Id)
        {
            return BadRequestResponse<ActivityTypeDto>("ID in the URL does not match the ID in the request body");
        }

        _logger.LogInformation("Updating Activity Type with ID: {Id}", id);

        var result = await _mediator.Send(command);

        return Ok(Success(result, "Activity Type updated successfully"));
    }

    /// <summary>
    /// Deletes an activity type by its ID.
    /// </summary>
    /// <param name="id">The ID of the activity type to delete.</param>
    /// <returns>A success response if deletion was successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting Activity Type with ID: {Id}", id);

        var command = new DeleteActivityTypeCommand { Id = id };
        var result = await _mediator.Send(command);

        return Ok(Success(result, "Activity Type deleted successfully"));
    }

    /// <summary>
    /// Gets an activity type by its ID.
    /// </summary>
    /// <param name="id">The ID of the activity type to retrieve.</param>
    /// <returns>The activity type details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<ActivityTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Getting Activity Type with ID: {Id}", id);

        var query = new GetActivityTypeByIdQuery { Id = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<ActivityTypeDto>($"Activity Type with ID {id} not found");
        }

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a list of all activity types.
    /// </summary>
    /// <returns>A list of all activity types.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ActivityTypeDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all Activity Types");

        var query = new GetAllActivityTypesQuery();
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets activity types by type name.
    /// </summary>
    /// <param name="type">The type name to search for.</param>
    /// <returns>A list of activity types matching the specified type name.</returns>
    [HttpGet("bytype/{type}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ActivityTypeDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByType(string type)
    {
        _logger.LogInformation("Getting Activity Types with Type: {Type}", type);

        var query = new GetActivityTypeByTypeQuery { Type = type };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets activity types by ordinal position.
    /// </summary>
    /// <param name="position">The ordinal position to search for.</param>
    /// <returns>A list of activity types with the specified ordinal position.</returns>
    [HttpGet("byposition/{position:int}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ActivityTypeDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByOrdinalPosition(int position)
    {
        _logger.LogInformation("Getting Activity Types with Ordinal Position: {Position}", position);

        var query = new GetActivityTypeByOrdinalPositionQuery { OrdinalPosition = position };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets the default activity type.
    /// </summary>
    /// <returns>The default activity type.</returns>
    [HttpGet("default")]
    [ProducesResponseType(typeof(ApiResponse<ActivityTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDefault()
    {
        _logger.LogInformation("Getting default Activity Type");

        var query = new GetDefaultActivityTypeQuery();
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<ActivityTypeDto>("Default Activity Type not found");
        }

        return Ok(Success(result));
    }
}