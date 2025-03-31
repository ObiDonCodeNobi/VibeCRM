using MediatR;
using Microsoft.AspNetCore.Mvc;
using VibeCRM.Application.Features.Role.Commands.CreateRole;
using VibeCRM.Application.Features.Role.Commands.DeleteRole;
using VibeCRM.Application.Features.Role.Commands.UpdateRole;
using VibeCRM.Application.Features.Role.Queries.GetAllRoles;
using VibeCRM.Application.Features.Role.Queries.GetRoleById;
using VibeCRM.Application.Features.Role.Queries.GetRoleByName;
using VibeCRM.Application.Features.Role.Queries.GetRolesByUserId;
using VibeCRM.Shared.DTOs.Role;
using VibeCRM.Shared.Models;

namespace VibeCRM.Api.Controllers;

/// <summary>
/// API endpoints for managing role resources.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RoleController : ApiControllerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RoleController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for sending commands and queries.</param>
    /// <param name="logger">The logger instance for logging controller actions.</param>
    public RoleController(IMediator mediator, ILogger<RoleController> logger)
        : base(mediator, logger)
    {
    }

    /// <summary>
    /// Creates a new role.
    /// </summary>
    /// <param name="command">The role creation details.</param>
    /// <returns>The newly created role.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<RoleDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateRoleCommand command)
    {
        _logger.LogInformation("Creating new Role with Name: {Name}", command.Name);

        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, Success(result, "Role created successfully"));
    }

    /// <summary>
    /// Updates an existing role.
    /// </summary>
    /// <param name="id">The ID of the role to update.</param>
    /// <param name="command">The updated role details.</param>
    /// <returns>The updated role.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<RoleDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRoleCommand command)
    {
        if (id != command.Id)
        {
            return BadRequestResponse<RoleDto>("ID in the URL does not match the ID in the request body");
        }

        _logger.LogInformation("Updating Role with ID: {Id}", id);

        var result = await _mediator.Send(command);

        return Ok(Success(result, "Role updated successfully"));
    }

    /// <summary>
    /// Deletes a role by its ID.
    /// </summary>
    /// <param name="id">The ID of the role to delete.</param>
    /// <returns>A success response if deletion was successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting Role with ID: {Id}", id);

        var command = new DeleteRoleCommand
        {
            Id = id,
            ModifiedBy = Guid.Parse(User.Identity?.Name ?? Guid.Empty.ToString())
        };
        var result = await _mediator.Send(command);

        return Ok(Success(result, "Role deleted successfully"));
    }

    /// <summary>
    /// Gets a role by its ID.
    /// </summary>
    /// <param name="id">The ID of the role to retrieve.</param>
    /// <returns>The role details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<RoleDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Getting Role with ID: {Id}", id);

        var query = new GetRoleByIdQuery { Id = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<RoleDto>($"Role with ID {id} not found");
        }

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a list of all roles.
    /// </summary>
    /// <returns>A list of all roles.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<RoleDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all Roles");

        var query = new GetAllRolesQuery();
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a role by its name.
    /// </summary>
    /// <param name="name">The name of the role to retrieve.</param>
    /// <returns>The role details.</returns>
    [HttpGet("by-name")]
    [ProducesResponseType(typeof(ApiResponse<RoleDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByName([FromQuery] string name)
    {
        _logger.LogInformation("Getting Role with Name: {Name}", name);

        var query = new GetRoleByNameQuery { Name = name };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<RoleDto>($"Role with Name {name} not found");
        }

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets all roles assigned to a specific user.
    /// </summary>
    /// <param name="userId">The ID of the user to filter roles by.</param>
    /// <returns>A list of roles assigned to the user.</returns>
    [HttpGet("by-user/{userId}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<RoleDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByUserId(Guid userId)
    {
        _logger.LogInformation("Getting Roles for User ID: {UserId}", userId);

        var query = new GetRolesByUserIdQuery { UserId = userId };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }
}