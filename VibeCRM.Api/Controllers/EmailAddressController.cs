using MediatR;
using Microsoft.AspNetCore.Mvc;
using VibeCRM.Application.Features.EmailAddress.Commands.CreateEmailAddress;
using VibeCRM.Application.Features.EmailAddress.Commands.DeleteEmailAddress;
using VibeCRM.Application.Features.EmailAddress.Commands.UpdateEmailAddress;
using VibeCRM.Application.Features.EmailAddress.Queries.GetAllEmailAddresses;
using VibeCRM.Application.Features.EmailAddress.Queries.GetEmailAddressById;
using VibeCRM.Application.Features.EmailAddress.Queries.GetEmailAddressesByType;
using VibeCRM.Application.Features.EmailAddress.Queries.SearchEmailAddresses;
using VibeCRM.Shared.DTOs.EmailAddress;
using VibeCRM.Shared.Models;

namespace VibeCRM.Api.Controllers;

/// <summary>
/// API endpoints for managing email addresses.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class EmailAddressController : ApiControllerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EmailAddressController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for sending commands and queries.</param>
    /// <param name="logger">The logger instance for logging controller actions.</param>
    public EmailAddressController(IMediator mediator, ILogger<EmailAddressController> logger)
        : base(mediator, logger)
    {
    }

    /// <summary>
    /// Creates a new email address.
    /// </summary>
    /// <param name="command">The email address creation details.</param>
    /// <returns>The newly created email address.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<EmailAddressDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateEmailAddressCommand command)
    {
        _logger.LogInformation("Creating new Email Address: {Address}", command.Address);

        command.CreatedBy = Guid.Parse(User.Identity?.Name ?? Guid.Empty.ToString());
        command.ModifiedBy = command.CreatedBy;

        var id = await _mediator.Send(command);

        // Fetch the created email address to return in the response
        var query = new GetEmailAddressByIdQuery { Id = id };
        var result = await _mediator.Send(query);

        return CreatedAtAction(nameof(GetById), new { id }, Success(result, "Email address created successfully"));
    }

    /// <summary>
    /// Updates an existing email address.
    /// </summary>
    /// <param name="id">The ID of the email address to update.</param>
    /// <param name="command">The updated email address details.</param>
    /// <returns>The updated email address.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<EmailAddressDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEmailAddressCommand command)
    {
        if (id != command.Id)
        {
            return BadRequestResponse<EmailAddressDto>("ID in the URL does not match the ID in the request body");
        }

        _logger.LogInformation("Updating Email Address with ID: {Id}", id);

        command.ModifiedBy = Guid.Parse(User.Identity?.Name ?? Guid.Empty.ToString());

        await _mediator.Send(command);

        // Fetch the updated email address to return in the response
        var query = new GetEmailAddressByIdQuery { Id = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<EmailAddressDto>($"Email Address with ID {id} not found");
        }

        return Ok(Success(result, "Email address updated successfully"));
    }

    /// <summary>
    /// Deletes an email address by its ID.
    /// </summary>
    /// <param name="id">The ID of the email address to delete.</param>
    /// <returns>A success response if deletion was successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting Email Address with ID: {Id}", id);

        var command = new DeleteEmailAddressCommand
        {
            Id = id,
            ModifiedBy = Guid.Parse(User.Identity?.Name ?? Guid.Empty.ToString())
        };

        var result = await _mediator.Send(command);

        return Ok(Success(result, "Email address deleted successfully"));
    }

    /// <summary>
    /// Gets an email address by its ID.
    /// </summary>
    /// <param name="id">The ID of the email address to retrieve.</param>
    /// <returns>The email address details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<EmailAddressDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Getting Email Address with ID: {Id}", id);

        var query = new GetEmailAddressByIdQuery { Id = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<EmailAddressDto>($"Email Address with ID {id} not found");
        }

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a list of all email addresses.
    /// </summary>
    /// <returns>A list of all email addresses.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<EmailAddressDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all Email Addresses");

        var query = new GetAllEmailAddressesQuery();
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets email addresses by type ID.
    /// </summary>
    /// <param name="typeId">The email address type ID to search for.</param>
    /// <returns>A list of email addresses with the specified type.</returns>
    [HttpGet("bytype/{typeId}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<EmailAddressDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByType(Guid typeId)
    {
        _logger.LogInformation("Getting Email Addresses with Type ID: {TypeId}", typeId);

        var query = new GetEmailAddressesByTypeQuery { EmailAddressTypeId = typeId };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Searches for email addresses by address string.
    /// </summary>
    /// <param name="searchTerm">The search term to look for in email addresses.</param>
    /// <returns>A list of email addresses matching the search term.</returns>
    [HttpGet("search/{searchTerm}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<EmailAddressDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Search(string searchTerm)
    {
        _logger.LogInformation("Searching Email Addresses with term: {SearchTerm}", searchTerm);

        var query = new SearchEmailAddressesQuery { SearchTerm = searchTerm };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }
}