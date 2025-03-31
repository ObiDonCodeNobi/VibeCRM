using MediatR;
using Microsoft.AspNetCore.Mvc;
using VibeCRM.Application.Features.Person.Commands.CreatePerson;
using VibeCRM.Application.Features.Person.Commands.DeletePerson;
using VibeCRM.Application.Features.Person.Commands.UpdatePerson;
using VibeCRM.Application.Features.Person.Queries.GetAllPersons;
using VibeCRM.Application.Features.Person.Queries.GetPersonById;
using VibeCRM.Application.Features.Person.Queries.GetPersonWithRelatedEntities;
using VibeCRM.Shared.DTOs.Person;
using VibeCRM.Shared.Models;

namespace VibeCRM.Api.Controllers;

/// <summary>
/// API endpoints for managing person resources.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PersonController : ApiControllerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PersonController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for sending commands and queries.</param>
    /// <param name="logger">The logger instance for logging controller actions.</param>
    public PersonController(IMediator mediator, ILogger<PersonController> logger)
        : base(mediator, logger)
    {
    }

    /// <summary>
    /// Creates a new person.
    /// </summary>
    /// <param name="command">The person creation details.</param>
    /// <returns>The newly created person.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<PersonDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreatePersonCommand command)
    {
        _logger.LogInformation("Creating new Person with FirstName: {Firstname}, LastName: {Lastname}",
            command.Firstname, command.Lastname);

        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, Success(result, "Person created successfully"));
    }

    /// <summary>
    /// Updates an existing person.
    /// </summary>
    /// <param name="id">The ID of the person to update.</param>
    /// <param name="command">The updated person details.</param>
    /// <returns>The updated person.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<PersonDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePersonCommand command)
    {
        if (id != command.Id)
        {
            return BadRequestResponse<PersonDto>("ID in the URL does not match the ID in the request body");
        }

        _logger.LogInformation("Updating Person with ID: {Id}", id);

        var result = await _mediator.Send(command);

        return Ok(Success(result, "Person updated successfully"));
    }

    /// <summary>
    /// Deletes a person by their ID.
    /// </summary>
    /// <param name="id">The ID of the person to delete.</param>
    /// <returns>A success response if deletion was successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting Person with ID: {Id}", id);

        var command = new DeletePersonCommand
        {
            Id = id,
            ModifiedBy = Guid.Parse(User.Identity?.Name ?? Guid.Empty.ToString())
        };
        var result = await _mediator.Send(command);

        return Ok(Success(result, "Person deleted successfully"));
    }

    /// <summary>
    /// Gets a person by their ID.
    /// </summary>
    /// <param name="id">The ID of the person to retrieve.</param>
    /// <returns>The person details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<PersonDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Getting Person with ID: {Id}", id);

        var query = new GetPersonByIdQuery { Id = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<PersonDto>($"Person with ID {id} not found");
        }

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a list of all persons.
    /// </summary>
    /// <returns>A list of all persons.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<PersonDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all Persons");

        var query = new GetAllPersonsQuery();
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a person with all related entities.
    /// </summary>
    /// <param name="id">The ID of the person to retrieve.</param>
    /// <returns>The person details with related entities.</returns>
    [HttpGet("{id}/with-related")]
    [ProducesResponseType(typeof(ApiResponse<PersonDetailsDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetWithRelatedEntities(Guid id)
    {
        _logger.LogInformation("Getting Person with related entities, ID: {Id}", id);

        var query = new GetPersonWithRelatedEntitiesQuery(id);
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<PersonDetailsDto>($"Person with ID {id} not found");
        }

        return Ok(Success(result));
    }
}