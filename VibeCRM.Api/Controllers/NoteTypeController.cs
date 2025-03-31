using MediatR;
using Microsoft.AspNetCore.Mvc;
using VibeCRM.Application.Features.NoteType.Commands.CreateNoteType;
using VibeCRM.Application.Features.NoteType.Commands.DeleteNoteType;
using VibeCRM.Application.Features.NoteType.Commands.UpdateNoteType;
using VibeCRM.Application.Features.NoteType.Queries.GetAllNoteTypes;
using VibeCRM.Application.Features.NoteType.Queries.GetNoteTypeById;
using VibeCRM.Application.Features.NoteType.Queries.GetNoteTypeByOrdinalPosition;
using VibeCRM.Application.Features.NoteType.Queries.GetNoteTypeByType;
using VibeCRM.Shared.DTOs.NoteType;
using VibeCRM.Shared.Models;

namespace VibeCRM.Api.Controllers;

/// <summary>
/// API endpoints for managing note type reference data.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class NoteTypeController : ApiControllerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NoteTypeController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for sending commands and queries.</param>
    /// <param name="logger">The logger instance for logging controller actions.</param>
    public NoteTypeController(IMediator mediator, ILogger<NoteTypeController> logger)
        : base(mediator, logger)
    {
    }

    /// <summary>
    /// Creates a new note type.
    /// </summary>
    /// <param name="command">The note type creation details.</param>
    /// <returns>The newly created note type.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<NoteTypeDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateNoteTypeCommand command)
    {
        _logger.LogInformation("Creating new Note Type with Type: {Type}", command.Type);

        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, Success(result, "Note Type created successfully"));
    }

    /// <summary>
    /// Updates an existing note type.
    /// </summary>
    /// <param name="id">The ID of the note type to update.</param>
    /// <param name="command">The updated note type details.</param>
    /// <returns>The updated note type.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<NoteTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateNoteTypeCommand command)
    {
        if (id != command.Id)
        {
            return BadRequestResponse<NoteTypeDto>("ID in the URL does not match the ID in the request body");
        }

        _logger.LogInformation("Updating Note Type with ID: {Id}", id);

        var result = await _mediator.Send(command);

        return Ok(Success(result, "Note Type updated successfully"));
    }

    /// <summary>
    /// Deletes a note type by its ID.
    /// </summary>
    /// <param name="id">The ID of the note type to delete.</param>
    /// <returns>A success response if deletion was successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting Note Type with ID: {Id}", id);

        var command = new DeleteNoteTypeCommand { Id = id };
        var result = await _mediator.Send(command);

        return Ok(Success(result, "Note Type deleted successfully"));
    }

    /// <summary>
    /// Gets a note type by its ID.
    /// </summary>
    /// <param name="id">The ID of the note type to retrieve.</param>
    /// <returns>The note type details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<NoteTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Getting Note Type with ID: {Id}", id);

        var query = new GetNoteTypeByIdQuery { Id = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<NoteTypeDto>($"Note Type with ID {id} not found");
        }

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a list of all note types.
    /// </summary>
    /// <returns>A list of all note types.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<NoteTypeDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all Note Types");

        var query = new GetAllNoteTypesQuery();
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets note types by type name.
    /// </summary>
    /// <param name="type">The type name to search for.</param>
    /// <returns>A list of note types matching the specified type name.</returns>
    [HttpGet("bytype/{type}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<NoteTypeDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByType(string type)
    {
        _logger.LogInformation("Getting Note Types with Type: {Type}", type);

        var query = new GetNoteTypeByTypeQuery { Type = type };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets note types by ordinal position.
    /// </summary>
    /// <param name="position">The ordinal position to search for.</param>
    /// <returns>A list of note types with the specified ordinal position.</returns>
    [HttpGet("byposition/{position:int}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<NoteTypeDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByOrdinalPosition(int position)
    {
        _logger.LogInformation("Getting Note Types with Ordinal Position: {Position}", position);

        var query = new GetNoteTypeByOrdinalPositionQuery();
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }
}