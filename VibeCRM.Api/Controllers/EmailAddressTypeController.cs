using MediatR;
using Microsoft.AspNetCore.Mvc;
using VibeCRM.Application.Features.EmailAddressType.Commands.CreateEmailAddressType;
using VibeCRM.Application.Features.EmailAddressType.Commands.DeleteEmailAddressType;
using VibeCRM.Application.Features.EmailAddressType.Commands.UpdateEmailAddressType;
using VibeCRM.Application.Features.EmailAddressType.Queries.GetAllEmailAddressTypes;
using VibeCRM.Application.Features.EmailAddressType.Queries.GetDefaultEmailAddressType;
using VibeCRM.Application.Features.EmailAddressType.Queries.GetEmailAddressTypeById;
using VibeCRM.Application.Features.EmailAddressType.Queries.GetEmailAddressTypeByType;
using VibeCRM.Application.Features.EmailAddressType.Queries.GetEmailAddressTypesByOrdinalPosition;
using VibeCRM.Shared.DTOs.EmailAddressType;
using VibeCRM.Shared.Models;

namespace VibeCRM.Api.Controllers;

/// <summary>
/// API endpoints for managing email address type reference data.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class EmailAddressTypeController : ApiControllerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EmailAddressTypeController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for sending commands and queries.</param>
    /// <param name="logger">The logger instance for logging controller actions.</param>
    public EmailAddressTypeController(IMediator mediator, ILogger<EmailAddressTypeController> logger)
        : base(mediator, logger)
    {
    }

    /// <summary>
    /// Creates a new email address type.
    /// </summary>
    /// <param name="command">The email address type creation details.</param>
    /// <returns>The newly created email address type.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<EmailAddressTypeDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateEmailAddressTypeCommand command)
    {
        _logger.LogInformation("Creating new Email Address Type with Type: {Type}", command.Type);

        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = result }, Success(result, "Email Address Type created successfully"));
    }

    /// <summary>
    /// Updates an existing email address type.
    /// </summary>
    /// <param name="id">The ID of the email address type to update.</param>
    /// <param name="command">The updated email address type details.</param>
    /// <returns>The updated email address type.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<EmailAddressTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEmailAddressTypeCommand command)
    {
        if (id != command.Id)
        {
            return BadRequestResponse<EmailAddressTypeDto>("ID in the URL does not match the ID in the request body");
        }

        _logger.LogInformation("Updating Email Address Type with ID: {Id}", id);

        var result = await _mediator.Send(command);

        return Ok(Success(result, "Email Address Type updated successfully"));
    }

    /// <summary>
    /// Deletes an email address type by its ID.
    /// </summary>
    /// <param name="id">The ID of the email address type to delete.</param>
    /// <returns>A success response if deletion was successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting Email Address Type with ID: {Id}", id);

        var command = new DeleteEmailAddressTypeCommand { Id = id };
        var result = await _mediator.Send(command);

        return Ok(Success(result, "Email Address Type deleted successfully"));
    }

    /// <summary>
    /// Gets an email address type by its ID.
    /// </summary>
    /// <param name="id">The ID of the email address type to retrieve.</param>
    /// <returns>The email address type details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<EmailAddressTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Getting Email Address Type with ID: {Id}", id);

        var query = new GetEmailAddressTypeByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<EmailAddressTypeDto>($"Email Address Type with ID {id} not found");
        }

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a list of all email address types.
    /// </summary>
    /// <returns>A list of all email address types.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<EmailAddressTypeDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all Email Address Types");

        var query = new GetAllEmailAddressTypesQuery();
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets email address types by type name.
    /// </summary>
    /// <param name="type">The type name to search for.</param>
    /// <returns>A list of email address types matching the specified type name.</returns>
    [HttpGet("bytype/{type}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<EmailAddressTypeDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByType(string type)
    {
        _logger.LogInformation("Getting Email Address Types with Type: {Type}", type);

        var query = new GetEmailAddressTypeByTypeQuery(type);
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets email address types by ordinal position.
    /// </summary>
    /// <param name="position">The ordinal position to search for.</param>
    /// <returns>A list of email address types with the specified ordinal position.</returns>
    [HttpGet("byposition/{position:int}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<EmailAddressTypeDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByOrdinalPosition(int position)
    {
        _logger.LogInformation("Getting Email Address Types with Ordinal Position: {Position}", position);

        var query = new GetEmailAddressTypesByOrdinalPositionQuery();
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets the default email address type.
    /// </summary>
    /// <returns>The default email address type.</returns>
    [HttpGet("default")]
    [ProducesResponseType(typeof(ApiResponse<EmailAddressTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDefault()
    {
        _logger.LogInformation("Getting default Email Address Type");

        var query = new GetDefaultEmailAddressTypeQuery();
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<EmailAddressTypeDto>("Default Email Address Type not found");
        }

        return Ok(Success(result));
    }
}