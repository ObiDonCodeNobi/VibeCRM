using MediatR;
using Microsoft.AspNetCore.Mvc;
using VibeCRM.Application.Features.ServiceType.Commands.CreateServiceType;
using VibeCRM.Application.Features.ServiceType.Commands.DeleteServiceType;
using VibeCRM.Application.Features.ServiceType.Commands.UpdateServiceType;
using VibeCRM.Application.Features.ServiceType.Queries.GetAllServiceTypes;
using VibeCRM.Application.Features.ServiceType.Queries.GetDefaultServiceType;
using VibeCRM.Application.Features.ServiceType.Queries.GetServiceTypeById;
using VibeCRM.Application.Features.ServiceType.Queries.GetServiceTypeByType;
using VibeCRM.Shared.DTOs.ServiceType;
using VibeCRM.Shared.Models;

namespace VibeCRM.Api.Controllers;

/// <summary>
/// API endpoints for managing service type reference data.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ServiceTypeController : ApiControllerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceTypeController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for sending commands and queries.</param>
    /// <param name="logger">The logger instance for logging controller actions.</param>
    public ServiceTypeController(IMediator mediator, ILogger<ServiceTypeController> logger)
        : base(mediator, logger)
    {
    }

    /// <summary>
    /// Creates a new service type.
    /// </summary>
    /// <param name="command">The service type creation details.</param>
    /// <returns>The newly created service type.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<ServiceTypeDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateServiceTypeCommand command)
    {
        _logger.LogInformation("Creating new Service Type with Type: {Type}", command.Type);

        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, Success(result, "Service Type created successfully"));
    }

    /// <summary>
    /// Updates an existing service type.
    /// </summary>
    /// <param name="id">The ID of the service type to update.</param>
    /// <param name="command">The updated service type details.</param>
    /// <returns>The updated service type.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<ServiceTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateServiceTypeCommand command)
    {
        if (id != command.Id)
        {
            return BadRequestResponse<ServiceTypeDto>("ID in the URL does not match the ID in the request body");
        }

        _logger.LogInformation("Updating Service Type with ID: {Id}", id);

        var result = await _mediator.Send(command);

        return Ok(Success(result, "Service Type updated successfully"));
    }

    /// <summary>
    /// Deletes a service type by its ID.
    /// </summary>
    /// <param name="id">The ID of the service type to delete.</param>
    /// <returns>A success response if deletion was successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting Service Type with ID: {Id}", id);

        var command = new DeleteServiceTypeCommand { Id = id };
        var result = await _mediator.Send(command);

        return Ok(Success(result, "Service Type deleted successfully"));
    }

    /// <summary>
    /// Gets a service type by its ID.
    /// </summary>
    /// <param name="id">The ID of the service type to retrieve.</param>
    /// <returns>The service type details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<ServiceTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Getting Service Type with ID: {Id}", id);

        var query = new GetServiceTypeByIdQuery { Id = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<ServiceTypeDto>($"Service Type with ID {id} not found");
        }

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a list of all service types.
    /// </summary>
    /// <returns>A list of all service types.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ServiceTypeDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all Service Types");

        var query = new GetAllServiceTypesQuery();
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets service types by type name.
    /// </summary>
    /// <param name="type">The type name to search for.</param>
    /// <returns>A list of service types matching the specified type name.</returns>
    [HttpGet("bytype/{type}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ServiceTypeDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByType(string type)
    {
        _logger.LogInformation("Getting Service Types with Type: {Type}", type);

        var query = new GetServiceTypeByTypeQuery { Type = type };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets the default service type.
    /// </summary>
    /// <returns>The default service type.</returns>
    [HttpGet("default")]
    [ProducesResponseType(typeof(ApiResponse<ServiceTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDefault()
    {
        _logger.LogInformation("Getting default Service Type");

        var query = new GetDefaultServiceTypeQuery();
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<ServiceTypeDto>("Default Service Type not found");
        }

        return Ok(Success(result));
    }
}