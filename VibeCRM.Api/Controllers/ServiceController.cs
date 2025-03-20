using MediatR;
using Microsoft.AspNetCore.Mvc;
using VibeCRM.Application.Common.Models;
using VibeCRM.Application.Features.Service.Commands.CreateService;
using VibeCRM.Application.Features.Service.Commands.DeleteService;
using VibeCRM.Application.Features.Service.Commands.UpdateService;
using VibeCRM.Application.Features.Service.DTOs;
using VibeCRM.Application.Features.Service.Queries.GetAllServices;
using VibeCRM.Application.Features.Service.Queries.GetAllServicesWithRelatedEntities;
using VibeCRM.Application.Features.Service.Queries.GetServiceById;
using VibeCRM.Application.Features.Service.Queries.GetServiceByIdWithRelatedEntities;

namespace VibeCRM.Api.Controllers;

/// <summary>
/// API endpoints for managing service resources.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ServiceController : ApiControllerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for sending commands and queries.</param>
    /// <param name="logger">The logger instance for logging controller actions.</param>
    public ServiceController(IMediator mediator, ILogger<ServiceController> logger)
        : base(mediator, logger)
    {
    }

    /// <summary>
    /// Creates a new service.
    /// </summary>
    /// <param name="command">The service creation details.</param>
    /// <returns>The newly created service.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<ServiceDetailsDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateServiceCommand command)
    {
        _logger.LogInformation("Creating new Service with Name: {Name}", command.Name);

        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, Success(result, "Service created successfully"));
    }

    /// <summary>
    /// Updates an existing service.
    /// </summary>
    /// <param name="id">The ID of the service to update.</param>
    /// <param name="command">The updated service details.</param>
    /// <returns>The updated service.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<ServiceDetailsDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateServiceCommand command)
    {
        if (id != command.Id)
        {
            return BadRequestResponse<ServiceDetailsDto>("ID in the URL does not match the ID in the request body");
        }

        _logger.LogInformation("Updating Service with ID: {Id}", id);

        var result = await _mediator.Send(command);

        return Ok(Success(result, "Service updated successfully"));
    }

    /// <summary>
    /// Deletes a service by its ID.
    /// </summary>
    /// <param name="id">The ID of the service to delete.</param>
    /// <returns>A success response if deletion was successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting Service with ID: {Id}", id);

        var command = new DeleteServiceCommand(id, Guid.Parse(User.Identity?.Name ?? Guid.Empty.ToString()));
        var result = await _mediator.Send(command);

        return Ok(Success(result, "Service deleted successfully"));
    }

    /// <summary>
    /// Gets a service by its ID.
    /// </summary>
    /// <param name="id">The ID of the service to retrieve.</param>
    /// <returns>The service details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<ServiceDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Getting Service with ID: {Id}", id);

        var query = new GetServiceByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<ServiceDto>($"Service with ID {id} not found");
        }

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a service by its ID including all related entities.
    /// </summary>
    /// <param name="id">The ID of the service to retrieve.</param>
    /// <returns>The service details with related entities.</returns>
    [HttpGet("{id}/withrelations")]
    [ProducesResponseType(typeof(ApiResponse<ServiceDetailsDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdWithRelations(Guid id)
    {
        _logger.LogInformation("Getting Service with related entities for ID: {Id}", id);

        var query = new GetServiceByIdWithRelatedEntitiesQuery { Id = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<ServiceDetailsDto>($"Service with ID {id} not found");
        }

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a list of all services.
    /// </summary>
    /// <returns>A list of all services.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ServiceListDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all Services");

        var query = new GetAllServicesQuery();
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a list of all services including related entities.
    /// </summary>
    /// <returns>A list of all services with related entities.</returns>
    [HttpGet("withrelations")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ServiceDetailsDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllWithRelations()
    {
        _logger.LogInformation("Getting all Services with related entities");

        var query = new GetAllServicesWithRelatedEntitiesQuery();
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }
}