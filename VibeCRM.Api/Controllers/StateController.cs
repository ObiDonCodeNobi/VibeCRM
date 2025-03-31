using MediatR;
using Microsoft.AspNetCore.Mvc;
using VibeCRM.Application.Features.State.Commands.CreateState;
using VibeCRM.Application.Features.State.Commands.DeleteState;
using VibeCRM.Application.Features.State.Commands.UpdateState;
using VibeCRM.Application.Features.State.Queries.GetAllStates;
using VibeCRM.Application.Features.State.Queries.GetDefaultState;
using VibeCRM.Application.Features.State.Queries.GetStateById;
using VibeCRM.Application.Features.State.Queries.GetStatesByAbbreviation;
using VibeCRM.Application.Features.State.Queries.GetStatesByName;
using VibeCRM.Shared.DTOs.State;
using VibeCRM.Shared.Models;

namespace VibeCRM.Api.Controllers;

/// <summary>
/// API endpoints for managing states/provinces.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class StateController : ApiControllerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StateController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for sending commands and queries.</param>
    /// <param name="logger">The logger instance for logging controller actions.</param>
    public StateController(IMediator mediator, ILogger<StateController> logger)
        : base(mediator, logger)
    {
    }

    /// <summary>
    /// Creates a new state.
    /// </summary>
    /// <param name="command">The state creation details.</param>
    /// <returns>The newly created state.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<StateDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateStateCommand command)
    {
        _logger.LogInformation("Creating new State with Name: {Name}, Abbreviation: {Abbreviation}, CountryCode: {CountryCode}",
            command.Name, command.Abbreviation, command.CountryCode);

        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, Success(result, "State created successfully"));
    }

    /// <summary>
    /// Updates an existing state.
    /// </summary>
    /// <param name="id">The ID of the state to update.</param>
    /// <param name="command">The updated state details.</param>
    /// <returns>The updated state.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<StateDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateStateCommand command)
    {
        if (id != command.Id)
        {
            return BadRequestResponse<StateDto>("ID in the URL does not match the ID in the request body");
        }

        _logger.LogInformation("Updating State with ID: {Id}", id);

        var result = await _mediator.Send(command);

        return Ok(Success(result, "State updated successfully"));
    }

    /// <summary>
    /// Deletes a state by its ID.
    /// </summary>
    /// <param name="id">The ID of the state to delete.</param>
    /// <returns>A success response if deletion was successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting State with ID: {Id}", id);

        var command = new DeleteStateCommand { Id = id };
        var result = await _mediator.Send(command);

        return Ok(Success(result, "State deleted successfully"));
    }

    /// <summary>
    /// Gets a state by its ID.
    /// </summary>
    /// <param name="id">The ID of the state to retrieve.</param>
    /// <returns>The state details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<StateDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Getting State with ID: {Id}", id);

        var query = new GetStateByIdQuery { Id = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<StateDto>($"State with ID {id} not found");
        }

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a list of all states.
    /// </summary>
    /// <returns>A list of all states.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<StateDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all States");

        var query = new GetAllStatesQuery();
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets states by name.
    /// </summary>
    /// <param name="name">The name to search for.</param>
    /// <returns>A list of states matching the specified name.</returns>
    [HttpGet("byname/{name}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<StateDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByName(string name)
    {
        _logger.LogInformation("Getting States with Name: {Name}", name);

        var query = new GetStatesByNameQuery { Name = name };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets states by abbreviation.
    /// </summary>
    /// <param name="abbreviation">The abbreviation to search for.</param>
    /// <returns>A list of states matching the specified abbreviation.</returns>
    [HttpGet("byabbreviation/{abbreviation}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<StateDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByAbbreviation(string abbreviation)
    {
        _logger.LogInformation("Getting States with Abbreviation: {Abbreviation}", abbreviation);

        var query = new GetStatesByAbbreviationQuery { Abbreviation = abbreviation };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets the default state.
    /// </summary>
    /// <returns>The default state.</returns>
    [HttpGet("default")]
    [ProducesResponseType(typeof(ApiResponse<StateDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDefault()
    {
        _logger.LogInformation("Getting Default State");

        var query = new GetDefaultStateQuery();
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<StateDto>("Default state not found");
        }

        return Ok(Success(result));
    }
}