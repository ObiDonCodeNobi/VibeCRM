using MediatR;
using Microsoft.AspNetCore.Mvc;
using VibeCRM.Application.Features.PhoneType.Commands.CreatePhoneType;
using VibeCRM.Application.Features.PhoneType.Commands.DeletePhoneType;
using VibeCRM.Application.Features.PhoneType.Commands.UpdatePhoneType;
using VibeCRM.Application.Features.PhoneType.Queries.GetAllPhoneTypes;
using VibeCRM.Application.Features.PhoneType.Queries.GetDefaultPhoneType;
using VibeCRM.Application.Features.PhoneType.Queries.GetPhoneTypeById;
using VibeCRM.Application.Features.PhoneType.Queries.GetPhoneTypeByOrdinalPosition;
using VibeCRM.Application.Features.PhoneType.Queries.GetPhoneTypeByType;
using VibeCRM.Shared.DTOs.PhoneType;
using VibeCRM.Shared.Models;

namespace VibeCRM.Api.Controllers;

/// <summary>
/// API endpoints for managing phone type reference data.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PhoneTypeController : ApiControllerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PhoneTypeController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for sending commands and queries.</param>
    /// <param name="logger">The logger instance for logging controller actions.</param>
    public PhoneTypeController(IMediator mediator, ILogger<PhoneTypeController> logger)
        : base(mediator, logger)
    {
    }

    /// <summary>
    /// Creates a new phone type.
    /// </summary>
    /// <param name="command">The phone type creation details.</param>
    /// <returns>The newly created phone type.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<PhoneTypeDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreatePhoneTypeCommand command)
    {
        _logger.LogInformation("Creating new Phone Type with Type: {Type}", command.Type);

        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, Success(result, "Phone Type created successfully"));
    }

    /// <summary>
    /// Updates an existing phone type.
    /// </summary>
    /// <param name="id">The ID of the phone type to update.</param>
    /// <param name="command">The updated phone type details.</param>
    /// <returns>The updated phone type.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<PhoneTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePhoneTypeCommand command)
    {
        if (id != command.Id)
        {
            return BadRequestResponse<PhoneTypeDto>("ID in the URL does not match the ID in the request body");
        }

        _logger.LogInformation("Updating Phone Type with ID: {Id}", id);

        var result = await _mediator.Send(command);

        return Ok(Success(result, "Phone Type updated successfully"));
    }

    /// <summary>
    /// Deletes a phone type by its ID.
    /// </summary>
    /// <param name="id">The ID of the phone type to delete.</param>
    /// <returns>A success response if deletion was successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting Phone Type with ID: {Id}", id);

        var command = new DeletePhoneTypeCommand { Id = id };
        var result = await _mediator.Send(command);

        return Ok(Success(result, "Phone Type deleted successfully"));
    }

    /// <summary>
    /// Gets a phone type by its ID.
    /// </summary>
    /// <param name="id">The ID of the phone type to retrieve.</param>
    /// <returns>The phone type details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<PhoneTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Getting Phone Type with ID: {Id}", id);

        var query = new GetPhoneTypeByIdQuery { Id = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<PhoneTypeDto>($"Phone Type with ID {id} not found");
        }

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a list of all phone types.
    /// </summary>
    /// <returns>A list of all phone types.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<PhoneTypeDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all Phone Types");

        var query = new GetAllPhoneTypesQuery();
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets phone types by type name.
    /// </summary>
    /// <param name="type">The type name to search for.</param>
    /// <returns>A list of phone types matching the specified type name.</returns>
    [HttpGet("bytype/{type}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<PhoneTypeDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByType(string type)
    {
        _logger.LogInformation("Getting Phone Types with Type: {Type}", type);

        var query = new GetPhoneTypeByTypeQuery { Type = type };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets phone types by ordinal position.
    /// </summary>
    /// <param name="position">The ordinal position to search for.</param>
    /// <returns>A list of phone types with the specified ordinal position.</returns>
    [HttpGet("byposition/{position:int}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<PhoneTypeDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByOrdinalPosition(int position)
    {
        _logger.LogInformation("Getting Phone Types with Ordinal Position: {Position}", position);

        var query = new GetPhoneTypeByOrdinalPositionQuery { OrdinalPosition = position };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets the default phone type.
    /// </summary>
    /// <returns>The default phone type.</returns>
    [HttpGet("default")]
    [ProducesResponseType(typeof(ApiResponse<PhoneTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDefault()
    {
        _logger.LogInformation("Getting default Phone Type");

        var query = new GetDefaultPhoneTypeQuery();
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<PhoneTypeDto>("Default Phone Type not found");
        }

        return Ok(Success(result));
    }
}