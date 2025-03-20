using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Common.Models;
using VibeCRM.Application.Features.CallDirection.Commands.CreateCallDirection;
using VibeCRM.Application.Features.CallDirection.Commands.DeleteCallDirection;
using VibeCRM.Application.Features.CallDirection.Commands.UpdateCallDirection;
using VibeCRM.Application.Features.CallDirection.DTOs;
using VibeCRM.Application.Features.CallDirection.Queries.GetAllCallDirections;
using VibeCRM.Application.Features.CallDirection.Queries.GetCallDirectionByDirection;
using VibeCRM.Application.Features.CallDirection.Queries.GetCallDirectionById;
using VibeCRM.Application.Features.CallDirection.Queries.GetCallDirectionsByOrdinalPosition;
using VibeCRM.Application.Features.CallDirection.Queries.GetDefaultCallDirection;

namespace VibeCRM.Api.Controllers;

/// <summary>
/// API endpoints for managing call direction reference data.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CallDirectionController : ApiControllerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CallDirectionController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for sending commands and queries.</param>
    /// <param name="logger">The logger instance for logging controller actions.</param>
    public CallDirectionController(IMediator mediator, ILogger<CallDirectionController> logger)
        : base(mediator, logger)
    {
    }

    /// <summary>
    /// Creates a new call direction.
    /// </summary>
    /// <param name="command">The call direction creation details.</param>
    /// <returns>The newly created call direction.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<CallDirectionDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateCallDirectionCommand command)
    {
        _logger.LogInformation("Creating new Call Direction with Direction: {Direction}", command.Direction);
        
        var result = await _mediator.Send(command);
        
        return CreatedAtAction(nameof(GetById), new { id = result }, Success(result, "Call Direction created successfully"));
    }

    /// <summary>
    /// Updates an existing call direction.
    /// </summary>
    /// <param name="id">The ID of the call direction to update.</param>
    /// <param name="command">The updated call direction details.</param>
    /// <returns>The updated call direction.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<CallDirectionDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCallDirectionCommand command)
    {
        if (command.Id != id)
        {
            return BadRequestResponse<CallDirectionDto>("ID in the URL does not match the ID in the request body");
        }

        _logger.LogInformation("Updating Call Direction with ID: {Id}", id);
        
        var result = await _mediator.Send(command);
        
        return Ok(Success(result, "Call Direction updated successfully"));
    }

    /// <summary>
    /// Deletes a call direction by its ID.
    /// </summary>
    /// <param name="id">The ID of the call direction to delete.</param>
    /// <returns>A success response if deletion was successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting Call Direction with ID: {Id}", id);
        
        var command = new DeleteCallDirectionCommand 
        { 
            Id = id,
            ModifiedBy = User.Identity?.Name ?? string.Empty
        };
        var result = await _mediator.Send(command);
        
        return Ok(Success(result, "Call Direction deleted successfully"));
    }

    /// <summary>
    /// Gets a call direction by its ID.
    /// </summary>
    /// <param name="id">The ID of the call direction to retrieve.</param>
    /// <returns>The call direction details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<CallDirectionDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Getting Call Direction with ID: {Id}", id);
        
        var query = new GetCallDirectionByIdQuery { Id = id };
        var result = await _mediator.Send(query);
        
        if (result == null)
        {
            return NotFoundResponse<CallDirectionDto>($"Call Direction with ID {id} not found");
        }
        
        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a list of all call directions.
    /// </summary>
    /// <returns>A list of all call directions.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<CallDirectionDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all Call Directions");
        
        var query = new GetAllCallDirectionsQuery();
        var result = await _mediator.Send(query);
        
        return Ok(Success(result));
    }

    /// <summary>
    /// Gets call directions by direction name.
    /// </summary>
    /// <param name="direction">The direction name to search for.</param>
    /// <returns>A list of call directions matching the specified direction name.</returns>
    [HttpGet("bydirection/{direction}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<CallDirectionDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByDirection(string direction)
    {
        _logger.LogInformation("Getting Call Directions with Direction: {Direction}", direction);
        
        var query = new GetCallDirectionByDirectionQuery { Direction = direction };
        var result = await _mediator.Send(query);
        
        return Ok(Success(result));
    }

    /// <summary>
    /// Gets call directions by ordinal position.
    /// </summary>
    /// <param name="position">The ordinal position to search for.</param>
    /// <returns>A list of call directions with the specified ordinal position.</returns>
    [HttpGet("byposition/{position:int}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<CallDirectionDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByOrdinalPosition(int position)
    {
        _logger.LogInformation("Getting Call Directions with Ordinal Position: {Position}", position);
        
        var query = new GetCallDirectionsByOrdinalPositionQuery();
        var result = await _mediator.Send(query);
        
        return Ok(Success(result));
    }

    /// <summary>
    /// Gets the default call direction.
    /// </summary>
    /// <returns>The default call direction.</returns>
    [HttpGet("default")]
    [ProducesResponseType(typeof(ApiResponse<CallDirectionDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDefault()
    {
        _logger.LogInformation("Getting default Call Direction");
        
        var query = new GetDefaultCallDirectionQuery();
        var result = await _mediator.Send(query);
        
        if (result == null)
        {
            return NotFoundResponse<CallDirectionDto>("Default Call Direction not found");
        }
        
        return Ok(Success(result));
    }
}
