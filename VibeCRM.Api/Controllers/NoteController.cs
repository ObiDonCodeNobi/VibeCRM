using MediatR;
using Microsoft.AspNetCore.Mvc;
using VibeCRM.Application.Common.Models;
using VibeCRM.Application.Features.Note.Commands.CreateNote;
using VibeCRM.Application.Features.Note.Commands.DeleteNote;
using VibeCRM.Application.Features.Note.Commands.UpdateNote;
using VibeCRM.Application.Features.Note.DTOs;
using VibeCRM.Application.Features.Note.Queries.GetAllNotes;
using VibeCRM.Application.Features.Note.Queries.GetNoteById;
using VibeCRM.Application.Features.Note.Queries.GetNotesByCompany;
using VibeCRM.Application.Features.Note.Queries.GetNotesByNoteType;
using VibeCRM.Application.Features.Note.Queries.GetNotesByPerson;

namespace VibeCRM.Api.Controllers;

/// <summary>
/// API endpoints for managing note resources.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class NoteController : ApiControllerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NoteController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for sending commands and queries.</param>
    /// <param name="logger">The logger instance for logging controller actions.</param>
    public NoteController(IMediator mediator, ILogger<NoteController> logger)
        : base(mediator, logger)
    {
    }

    /// <summary>
    /// Creates a new note.
    /// </summary>
    /// <param name="command">The note creation details.</param>
    /// <returns>The newly created note.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<NoteDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateNoteCommand command)
    {
        _logger.LogInformation("Creating new Note with Subject: {Subject}", command.Subject);

        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, Success(result, "Note created successfully"));
    }

    /// <summary>
    /// Updates an existing note.
    /// </summary>
    /// <param name="id">The ID of the note to update.</param>
    /// <param name="command">The updated note details.</param>
    /// <returns>The updated note.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<NoteDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateNoteCommand command)
    {
        if (id != command.NoteId)
        {
            return BadRequestResponse<NoteDto>("ID in the URL does not match the ID in the request body");
        }

        _logger.LogInformation("Updating Note with ID: {Id}", id);

        var result = await _mediator.Send(command);

        return Ok(Success(result, "Note updated successfully"));
    }

    /// <summary>
    /// Deletes a note by its ID.
    /// </summary>
    /// <param name="id">The ID of the note to delete.</param>
    /// <returns>A success response if deletion was successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting Note with ID: {Id}", id);

        var command = new DeleteNoteCommand(id, Guid.Parse(User.Identity?.Name ?? Guid.Empty.ToString()));
        var result = await _mediator.Send(command);

        return Ok(Success(result, "Note deleted successfully"));
    }

    /// <summary>
    /// Gets a note by its ID.
    /// </summary>
    /// <param name="id">The ID of the note to retrieve.</param>
    /// <returns>The note details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<NoteDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Getting Note with ID: {Id}", id);

        var query = new GetNoteByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<NoteDto>($"Note with ID {id} not found");
        }

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a list of all notes.
    /// </summary>
    /// <returns>A list of all notes.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<NoteDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all Notes");

        var query = new GetAllNotesQuery();
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets notes by company ID.
    /// </summary>
    /// <param name="companyId">The ID of the company to get notes for.</param>
    /// <returns>A list of notes for the specified company.</returns>
    [HttpGet("bycompany/{companyId}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<NoteDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByCompany(Guid companyId)
    {
        _logger.LogInformation("Getting Notes for Company with ID: {CompanyId}", companyId);

        var query = new GetNotesByCompanyQuery(companyId);
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets notes by person ID.
    /// </summary>
    /// <param name="personId">The ID of the person to get notes for.</param>
    /// <returns>A list of notes for the specified person.</returns>
    [HttpGet("byperson/{personId}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<NoteDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByPerson(Guid personId)
    {
        _logger.LogInformation("Getting Notes for Person with ID: {PersonId}", personId);

        var query = new GetNotesByPersonQuery(personId);
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets notes by note type ID.
    /// </summary>
    /// <param name="noteTypeId">The ID of the note type to get notes for.</param>
    /// <returns>A list of notes for the specified note type.</returns>
    [HttpGet("bynotetype/{noteTypeId}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<NoteDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByNoteType(Guid noteTypeId)
    {
        _logger.LogInformation("Getting Notes for Note Type with ID: {NoteTypeId}", noteTypeId);

        var query = new GetNotesByNoteTypeQuery(noteTypeId);
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }
}