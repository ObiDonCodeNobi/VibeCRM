using MediatR;
using Microsoft.AspNetCore.Mvc;
using VibeCRM.Application.Common.Models;
using VibeCRM.Application.Features.User.Commands.CreateUser;
using VibeCRM.Application.Features.User.Commands.DeleteUser;
using VibeCRM.Application.Features.User.Commands.UpdateUser;
using VibeCRM.Application.Features.User.DTOs;
using VibeCRM.Application.Features.User.Queries.GetAllUsers;
using VibeCRM.Application.Features.User.Queries.GetUserByEmail;
using VibeCRM.Application.Features.User.Queries.GetUserById;
using VibeCRM.Application.Features.User.Queries.GetUserByUsername;
using VibeCRM.Application.Features.User.Queries.GetUsersByRoleId;
using VibeCRM.Application.Features.User.Queries.GetUsersByTeamId;

namespace VibeCRM.Api.Controllers;

/// <summary>
/// API endpoints for managing user resources.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UserController : ApiControllerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for sending commands and queries.</param>
    /// <param name="logger">The logger instance for logging controller actions.</param>
    public UserController(IMediator mediator, ILogger<UserController> logger)
        : base(mediator, logger)
    {
    }

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="command">The user creation details.</param>
    /// <returns>The newly created user.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        _logger.LogInformation("Creating new User with LoginName: {LoginName}",
            command.LoginName);

        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, Success(result, "User created successfully"));
    }

    /// <summary>
    /// Updates an existing user.
    /// </summary>
    /// <param name="id">The ID of the user to update.</param>
    /// <param name="command">The updated user details.</param>
    /// <returns>The updated user.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserCommand command)
    {
        if (id != command.Id)
        {
            return BadRequestResponse<UserDto>("ID in the URL does not match the ID in the request body");
        }

        _logger.LogInformation("Updating User with ID: {Id}", id);

        var result = await _mediator.Send(command);

        return Ok(Success(result, "User updated successfully"));
    }

    /// <summary>
    /// Deletes a user by their ID.
    /// </summary>
    /// <param name="id">The ID of the user to delete.</param>
    /// <returns>A success response if deletion was successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting User with ID: {Id}", id);

        var command = new DeleteUserCommand
        {
            Id = id,
            ModifiedBy = Guid.Parse(User.Identity?.Name ?? Guid.Empty.ToString())
        };
        var result = await _mediator.Send(command);

        return Ok(Success(result, "User deleted successfully"));
    }

    /// <summary>
    /// Gets a user by their ID.
    /// </summary>
    /// <param name="id">The ID of the user to retrieve.</param>
    /// <returns>The user details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Getting User with ID: {Id}", id);

        var query = new GetUserByIdQuery { Id = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<UserDto>($"User with ID {id} not found");
        }

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a list of all users.
    /// </summary>
    /// <returns>A list of all users.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<UserDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all Users");

        var query = new GetAllUsersQuery();
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a user by username.
    /// </summary>
    /// <param name="username">The username of the user to retrieve.</param>
    /// <returns>The user details.</returns>
    [HttpGet("by-username/{username}")]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByUsername(string username)
    {
        _logger.LogInformation("Getting User with Username: {Username}", username);

        var query = new GetUserByUsernameQuery { Username = username };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<UserDto>($"User with Username {username} not found");
        }

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a user by their email address.
    /// </summary>
    /// <param name="email">The email address of the user to retrieve.</param>
    /// <returns>The user details.</returns>
    [HttpGet("by-email")]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByEmail([FromQuery] string email)
    {
        _logger.LogInformation("Getting User with Email: {Email}", email);

        var query = new GetUserByEmailQuery { Email = email };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<UserDto>($"User with Email {email} not found");
        }

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets all users belonging to a specific role.
    /// </summary>
    /// <param name="roleId">The ID of the role to filter users by.</param>
    /// <returns>A list of users with the specified role.</returns>
    [HttpGet("by-role/{roleId}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<UserDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByRoleId(Guid roleId)
    {
        _logger.LogInformation("Getting Users with Role ID: {RoleId}", roleId);

        var query = new GetUsersByRoleIdQuery { RoleId = roleId };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets all users belonging to a specific team.
    /// </summary>
    /// <param name="teamId">The ID of the team to filter users by.</param>
    /// <returns>A list of users in the specified team.</returns>
    [HttpGet("by-team/{teamId}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<UserDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByTeamId(Guid teamId)
    {
        _logger.LogInformation("Getting Users with Team ID: {TeamId}", teamId);

        var query = new GetUsersByTeamIdQuery { TeamId = teamId };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }
}