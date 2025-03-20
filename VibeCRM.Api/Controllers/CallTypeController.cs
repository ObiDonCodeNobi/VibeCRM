using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Common.Models;
using VibeCRM.Application.Features.CallType.Commands.CreateCallType;
using VibeCRM.Application.Features.CallType.Commands.DeleteCallType;
using VibeCRM.Application.Features.CallType.Commands.UpdateCallType;
using VibeCRM.Application.Features.CallType.DTOs;
using VibeCRM.Application.Features.CallType.Queries.GetAllCallTypes;
using VibeCRM.Application.Features.CallType.Queries.GetCallTypeById;
using VibeCRM.Application.Features.CallType.Queries.GetCallTypeByType;
using VibeCRM.Application.Features.CallType.Queries.GetCallTypesByOrdinalPosition;
using VibeCRM.Application.Features.CallType.Queries.GetDefaultCallType;
using VibeCRM.Application.Features.CallType.Queries.GetInboundCallTypes;
using VibeCRM.Application.Features.CallType.Queries.GetOutboundCallTypes;

namespace VibeCRM.Api.Controllers;

/// <summary>
/// API endpoints for managing call type reference data.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CallTypeController : ApiControllerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CallTypeController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for sending commands and queries.</param>
    /// <param name="logger">The logger instance for logging controller actions.</param>
    public CallTypeController(IMediator mediator, ILogger<CallTypeController> logger)
        : base(mediator, logger)
    {
    }

    /// <summary>
    /// Creates a new call type.
    /// </summary>
    /// <param name="command">The call type creation details.</param>
    /// <returns>The newly created call type.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<CallTypeDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateCallTypeCommand command)
    {
        _logger.LogInformation("Creating new Call Type with Type: {Type}", command.Type);
        
        var result = await _mediator.Send(command);
        
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, Success(result, "Call Type created successfully"));
    }

    /// <summary>
    /// Updates an existing call type.
    /// </summary>
    /// <param name="id">The ID of the call type to update.</param>
    /// <param name="command">The updated call type details.</param>
    /// <returns>The updated call type.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<CallTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCallTypeCommand command)
    {
        if (id != command.Id)
        {
            return BadRequestResponse<CallTypeDto>("ID in the URL does not match the ID in the request body");
        }

        _logger.LogInformation("Updating Call Type with ID: {Id}", id);
        
        var result = await _mediator.Send(command);
        
        return Ok(Success(result, "Call Type updated successfully"));
    }

    /// <summary>
    /// Deletes a call type by its ID.
    /// </summary>
    /// <param name="id">The ID of the call type to delete.</param>
    /// <returns>A success response if deletion was successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting Call Type with ID: {Id}", id);
        
        var command = new DeleteCallTypeCommand { Id = id };
        var result = await _mediator.Send(command);
        
        return Ok(Success(result, "Call Type deleted successfully"));
    }

    /// <summary>
    /// Gets a call type by its ID.
    /// </summary>
    /// <param name="id">The ID of the call type to retrieve.</param>
    /// <returns>The call type details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<CallTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Getting Call Type with ID: {Id}", id);
        
        var query = new GetCallTypeByIdQuery { Id = id };
        var result = await _mediator.Send(query);
        
        if (result == null)
        {
            return NotFoundResponse<CallTypeDto>($"Call Type with ID {id} not found");
        }
        
        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a list of all call types.
    /// </summary>
    /// <returns>A list of all call types.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<CallTypeDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all Call Types");
        
        var query = new GetAllCallTypesQuery();
        var result = await _mediator.Send(query);
        
        return Ok(Success(result));
    }

    /// <summary>
    /// Gets call types by type name.
    /// </summary>
    /// <param name="type">The type name to search for.</param>
    /// <returns>A list of call types matching the specified type name.</returns>
    [HttpGet("bytype/{type}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<CallTypeDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByType(string type)
    {
        _logger.LogInformation("Getting Call Types with Type: {Type}", type);
        
        var query = new GetCallTypeByTypeQuery { Type = type };
        var result = await _mediator.Send(query);
        
        return Ok(Success(result));
    }

    /// <summary>
    /// Gets call types by ordinal position.
    /// </summary>
    /// <param name="position">The ordinal position to search for.</param>
    /// <returns>A list of call types with the specified ordinal position.</returns>
    [HttpGet("byposition/{position:int}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<CallTypeDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByOrdinalPosition(int position)
    {
        _logger.LogInformation("Getting Call Types with Ordinal Position: {Position}", position);
        
        var query = new GetCallTypesByOrdinalPositionQuery();
        var result = await _mediator.Send(query);
        
        return Ok(Success(result));
    }

    /// <summary>
    /// Gets the default call type.
    /// </summary>
    /// <returns>The default call type.</returns>
    [HttpGet("default")]
    [ProducesResponseType(typeof(ApiResponse<CallTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDefault()
    {
        _logger.LogInformation("Getting default Call Type");
        
        var query = new GetDefaultCallTypeQuery();
        var result = await _mediator.Send(query);
        
        if (result == null)
        {
            return NotFoundResponse<CallTypeDto>("Default Call Type not found");
        }
        
        return Ok(Success(result));
    }

    /// <summary>
    /// Gets all inbound call types.
    /// </summary>
    /// <returns>A list of all inbound call types.</returns>
    [HttpGet("inbound")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<CallTypeDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetInbound()
    {
        _logger.LogInformation("Getting all inbound Call Types");
        
        var query = new GetInboundCallTypesQuery();
        var result = await _mediator.Send(query);
        
        return Ok(Success(result));
    }

    /// <summary>
    /// Gets all outbound call types.
    /// </summary>
    /// <returns>A list of all outbound call types.</returns>
    [HttpGet("outbound")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<CallTypeDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOutbound()
    {
        _logger.LogInformation("Getting all outbound Call Types");
        
        var query = new GetOutboundCallTypesQuery();
        var result = await _mediator.Send(query);
        
        return Ok(Success(result));
    }
}
