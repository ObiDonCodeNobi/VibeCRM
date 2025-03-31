using MediatR;
using Microsoft.AspNetCore.Mvc;
using VibeCRM.Application.Features.Address.Commands.CreateAddress;
using VibeCRM.Application.Features.Address.Commands.DeleteAddress;
using VibeCRM.Application.Features.Address.Commands.UpdateAddress;
using VibeCRM.Application.Features.Address.Queries.GetAddressById;
using VibeCRM.Application.Features.Address.Queries.GetAllAddresses;
using VibeCRM.Shared.DTOs.Address;
using VibeCRM.Shared.Models;

namespace VibeCRM.Api.Controllers;

/// <summary>
/// API endpoints for managing addresses.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AddressController : ApiControllerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AddressController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for sending commands and queries.</param>
    /// <param name="logger">The logger instance for logging controller actions.</param>
    public AddressController(IMediator mediator, ILogger<AddressController> logger)
        : base(mediator, logger)
    {
    }

    /// <summary>
    /// Creates a new address.
    /// </summary>
    /// <param name="command">The address creation details.</param>
    /// <returns>The newly created address.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<AddressDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateAddressCommand command)
    {
        _logger.LogInformation("Creating new Address for {Line1}, {City}, {StateId}",
            command.Line1, command.City, command.StateId);

        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, Success(result, "Address created successfully"));
    }

    /// <summary>
    /// Updates an existing address.
    /// </summary>
    /// <param name="id">The ID of the address to update.</param>
    /// <param name="command">The updated address details.</param>
    /// <returns>The updated address.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<AddressDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAddressCommand command)
    {
        if (id != command.AddressId)
        {
            return BadRequestResponse<AddressDto>("ID in the URL does not match the ID in the request body");
        }

        _logger.LogInformation("Updating Address with ID: {Id}", id);

        var result = await _mediator.Send(command);

        return Ok(Success(result, "Address updated successfully"));
    }

    /// <summary>
    /// Deletes an address by its ID.
    /// </summary>
    /// <param name="id">The ID of the address to delete.</param>
    /// <returns>A success response if deletion was successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting Address with ID: {Id}", id);

        var command = new DeleteAddressCommand(id, Guid.Empty); // Using empty GUID for ModifiedBy as a placeholder
        var result = await _mediator.Send(command);

        return Ok(Success(result, "Address deleted successfully"));
    }

    /// <summary>
    /// Gets an address by its ID.
    /// </summary>
    /// <param name="id">The ID of the address to retrieve.</param>
    /// <returns>The address details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<AddressDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Getting Address with ID: {Id}", id);

        var query = new GetAddressByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<AddressDto>($"Address with ID {id} not found");
        }

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a list of all addresses.
    /// </summary>
    /// <returns>A list of all addresses.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<AddressDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all Addresses");

        var query = new GetAllAddressesQuery();
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }
}