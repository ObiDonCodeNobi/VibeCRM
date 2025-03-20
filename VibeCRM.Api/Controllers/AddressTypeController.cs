using MediatR;
using Microsoft.AspNetCore.Mvc;
using VibeCRM.Application.Common.Models;
using VibeCRM.Application.Features.AddressType.Commands.CreateAddressType;
using VibeCRM.Application.Features.AddressType.Commands.DeleteAddressType;
using VibeCRM.Application.Features.AddressType.Commands.UpdateAddressType;
using VibeCRM.Application.Features.AddressType.DTOs;
using VibeCRM.Application.Features.AddressType.Queries.GetAddressTypeByType;
using VibeCRM.Application.Features.AddressType.Queries.GetDefaultAddressType;

namespace VibeCRM.Api.Controllers;

/// <summary>
/// API endpoints for managing address type reference data.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AddressTypeController : ApiControllerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AddressTypeController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for sending commands and queries.</param>
    /// <param name="logger">The logger instance for logging controller actions.</param>
    public AddressTypeController(IMediator mediator, ILogger<AddressTypeController> logger)
        : base(mediator, logger)
    {
    }

    /// <summary>
    /// Creates a new address type.
    /// </summary>
    /// <param name="command">The address type creation details.</param>
    /// <returns>The newly created address type.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<AddressTypeDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateAddressTypeCommand command)
    {
        _logger.LogInformation("Creating new Address Type with Type: {Type}", command.Type);

        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, Success(result, "Address Type created successfully"));
    }

    /// <summary>
    /// Updates an existing address type.
    /// </summary>
    /// <param name="id">The ID of the address type to update.</param>
    /// <param name="command">The updated address type details.</param>
    /// <returns>The updated address type.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<AddressTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAddressTypeCommand command)
    {
        if (id != command.Id)
        {
            return BadRequestResponse<AddressTypeDto>("ID in the URL does not match the ID in the request body");
        }

        _logger.LogInformation("Updating Address Type with ID: {Id}", id);

        var result = await _mediator.Send(command);

        return Ok(Success(result, "Address Type updated successfully"));
    }

    /// <summary>
    /// Deletes an address type by its ID.
    /// </summary>
    /// <param name="id">The ID of the address type to delete.</param>
    /// <returns>A success response if deletion was successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting Address Type with ID: {Id}", id);

        var command = new DeleteAddressTypeCommand { Id = id };
        var result = await _mediator.Send(command);

        return Ok(Success(result, "Address Type deleted successfully"));
    }

    /// <summary>
    /// Gets an address type by its ID.
    /// </summary>
    /// <param name="id">The ID of the address type to retrieve.</param>
    /// <returns>The address type details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<AddressTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Getting Address Type with ID: {Id}", id);

        // Since there's no specific GetAddressTypeByIdQuery, we'll use the GetAddressTypeByTypeQuery
        // and filter the results in the controller
        var query = new GetAddressTypeByTypeQuery { Type = string.Empty };
        var result = await _mediator.Send(query);

        if (result == null || result.Id != id)
        {
            return NotFoundResponse<AddressTypeDto>($"Address Type with ID {id} not found");
        }

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a list of all address types.
    /// </summary>
    /// <returns>A list of all address types.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<AddressTypeDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all Address Types");

        // Since there's no specific GetAllAddressTypesQuery, we'll use the GetAddressTypeByTypeQuery
        // with an empty type string to get all address types
        var query = new GetAddressTypeByTypeQuery { Type = string.Empty };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets address types by type name.
    /// </summary>
    /// <param name="type">The type name to search for.</param>
    /// <returns>A list of address types matching the specified type name.</returns>
    [HttpGet("bytype/{type}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<AddressTypeDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByType(string type)
    {
        _logger.LogInformation("Getting Address Types with Type: {Type}", type);

        var query = new GetAddressTypeByTypeQuery { Type = type };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets the default address type.
    /// </summary>
    /// <returns>The default address type.</returns>
    [HttpGet("default")]
    [ProducesResponseType(typeof(ApiResponse<AddressTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDefault()
    {
        _logger.LogInformation("Getting default Address Type");

        var query = new GetDefaultAddressTypeQuery();
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<AddressTypeDto>("Default Address Type not found");
        }

        return Ok(Success(result));
    }
}