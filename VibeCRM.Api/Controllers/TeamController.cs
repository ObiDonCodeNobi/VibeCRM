using MediatR;
using Microsoft.AspNetCore.Mvc;
using VibeCRM.Application.Common.Models;
using VibeCRM.Application.Features.Team.Commands.CreateTeam;
using VibeCRM.Application.Features.Team.Commands.DeleteTeam;
using VibeCRM.Application.Features.Team.Commands.UpdateTeam;
using VibeCRM.Application.Features.Team.DTOs;
using VibeCRM.Application.Features.Team.Queries.GetAllTeams;
using VibeCRM.Application.Features.Team.Queries.GetTeamById;
using VibeCRM.Application.Features.Team.Queries.GetTeamByName;
using VibeCRM.Application.Features.Team.Queries.GetTeamsByUserId;

namespace VibeCRM.Api.Controllers;

/// <summary>
/// API endpoints for managing team resources.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TeamController : ApiControllerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TeamController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for sending commands and queries.</param>
    /// <param name="logger">The logger instance for logging controller actions.</param>
    public TeamController(IMediator mediator, ILogger<TeamController> logger)
        : base(mediator, logger)
    {
    }

    /// <summary>
    /// Creates a new team.
    /// </summary>
    /// <param name="command">The team creation details.</param>
    /// <returns>The newly created team.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<TeamDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateTeamCommand command)
    {
        _logger.LogInformation("Creating new Team with Name: {Name}", command.Name);

        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, Success(result, "Team created successfully"));
    }

    /// <summary>
    /// Updates an existing team.
    /// </summary>
    /// <param name="id">The ID of the team to update.</param>
    /// <param name="command">The updated team details.</param>
    /// <returns>The updated team.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<TeamDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTeamCommand command)
    {
        if (id != command.Id)
        {
            return BadRequestResponse<TeamDto>("ID in the URL does not match the ID in the request body");
        }

        _logger.LogInformation("Updating Team with ID: {Id}", id);

        var result = await _mediator.Send(command);

        return Ok(Success(result, "Team updated successfully"));
    }

    /// <summary>
    /// Deletes a team by its ID.
    /// </summary>
    /// <param name="id">The ID of the team to delete.</param>
    /// <returns>A success response if deletion was successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting Team with ID: {Id}", id);

        var command = new DeleteTeamCommand
        {
            Id = id,
            ModifiedBy = Guid.Parse(User.Identity?.Name ?? Guid.Empty.ToString())
        };
        var result = await _mediator.Send(command);

        return Ok(Success(result, "Team deleted successfully"));
    }

    /// <summary>
    /// Gets a team by its ID.
    /// </summary>
    /// <param name="id">The ID of the team to retrieve.</param>
    /// <returns>The team details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<TeamDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Getting Team with ID: {Id}", id);

        var query = new GetTeamByIdQuery { Id = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<TeamDto>($"Team with ID {id} not found");
        }

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a list of all teams.
    /// </summary>
    /// <returns>A list of all teams.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<TeamDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all Teams");

        var query = new GetAllTeamsQuery();
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a team by its name.
    /// </summary>
    /// <param name="name">The name of the team to retrieve.</param>
    /// <returns>The team details.</returns>
    [HttpGet("by-name")]
    [ProducesResponseType(typeof(ApiResponse<TeamDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByName([FromQuery] string name)
    {
        _logger.LogInformation("Getting Team with Name: {Name}", name);

        var query = new GetTeamByNameQuery { Name = name };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<TeamDto>($"Team with Name {name} not found");
        }

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets all teams that a user belongs to.
    /// </summary>
    /// <param name="userId">The ID of the user to filter teams by.</param>
    /// <returns>A list of teams the user belongs to.</returns>
    [HttpGet("by-user/{userId}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<TeamDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByUserId(Guid userId)
    {
        _logger.LogInformation("Getting Teams for User ID: {UserId}", userId);

        var query = new GetTeamsByUserIdQuery { UserId = userId };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }
}