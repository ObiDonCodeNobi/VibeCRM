using MediatR;
using Microsoft.AspNetCore.Mvc;
using VibeCRM.Application.Features.Call.Commands.CreateCall;
using VibeCRM.Application.Features.Call.Commands.DeleteCall;
using VibeCRM.Application.Features.Call.Commands.UpdateCall;
using VibeCRM.Application.Features.Call.Queries.GetAllCalls;
using VibeCRM.Application.Features.Call.Queries.GetCallById;
using VibeCRM.Shared.DTOs.Call;
using VibeCRM.Shared.Models;

namespace VibeCRM.Api.Controllers;

/// <summary>
/// API endpoints for managing call resources.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CallController : ApiControllerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CallController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for sending commands and queries.</param>
    /// <param name="logger">The logger instance for logging controller actions.</param>
    public CallController(IMediator mediator, ILogger<CallController> logger)
        : base(mediator, logger)
    {
    }

    /// <summary>
    /// Creates a new call.
    /// </summary>
    /// <param name="command">The call creation details.</param>
    /// <returns>The newly created call.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<CallDetailsDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateCallCommand command)
    {
        _logger.LogInformation("Creating new Call with Description: {Description}", command.Description);

        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, Success(result, "Call created successfully"));
    }

    /// <summary>
    /// Updates an existing call.
    /// </summary>
    /// <param name="id">The ID of the call to update.</param>
    /// <param name="command">The updated call details.</param>
    /// <returns>The updated call.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<CallDetailsDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCallCommand command)
    {
        if (id != command.Id)
        {
            return BadRequestResponse<CallDetailsDto>("ID in the URL does not match the ID in the request body");
        }

        _logger.LogInformation("Updating Call with ID: {Id}", id);

        var result = await _mediator.Send(command);

        return Ok(Success(result, "Call updated successfully"));
    }

    /// <summary>
    /// Deletes a call by its ID.
    /// </summary>
    /// <param name="id">The ID of the call to delete.</param>
    /// <returns>A success response if deletion was successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting Call with ID: {Id}", id);

        var command = new DeleteCallCommand
        {
            Id = id,
            ModifiedBy = Guid.Parse(User.Identity?.Name ?? Guid.Empty.ToString())
        };
        var result = await _mediator.Send(command);

        return Ok(Success(result, "Call deleted successfully"));
    }

    /// <summary>
    /// Gets a call by its ID.
    /// </summary>
    /// <param name="id">The ID of the call to retrieve.</param>
    /// <returns>The call details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<CallDetailsDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Getting Call with ID: {Id}", id);

        var query = new GetCallByIdQuery { Id = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<CallDetailsDto>($"Call with ID {id} not found");
        }

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a list of all calls.
    /// </summary>
    /// <returns>A list of all calls.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<CallListDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all Calls");

        var query = new GetAllCallsQuery();
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }
}