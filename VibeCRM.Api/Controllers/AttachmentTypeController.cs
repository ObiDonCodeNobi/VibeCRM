using MediatR;
using Microsoft.AspNetCore.Mvc;
using VibeCRM.Application.Features.AttachmentType.Commands.CreateAttachmentType;
using VibeCRM.Application.Features.AttachmentType.Commands.DeleteAttachmentType;
using VibeCRM.Application.Features.AttachmentType.Commands.UpdateAttachmentType;
using VibeCRM.Application.Features.AttachmentType.Queries.GetAllAttachmentTypes;
using VibeCRM.Application.Features.AttachmentType.Queries.GetAttachmentTypeByFileExtension;
using VibeCRM.Application.Features.AttachmentType.Queries.GetAttachmentTypeById;
using VibeCRM.Application.Features.AttachmentType.Queries.GetAttachmentTypeByOrdinalPosition;
using VibeCRM.Application.Features.AttachmentType.Queries.GetAttachmentTypeByType;
using VibeCRM.Application.Features.AttachmentType.Queries.GetDefaultAttachmentType;
using VibeCRM.Shared.DTOs.AttachmentType;
using VibeCRM.Shared.Models;

namespace VibeCRM.Api.Controllers;

/// <summary>
/// API endpoints for managing attachment type reference data.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AttachmentTypeController : ApiControllerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AttachmentTypeController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for sending commands and queries.</param>
    /// <param name="logger">The logger instance for logging controller actions.</param>
    public AttachmentTypeController(IMediator mediator, ILogger<AttachmentTypeController> logger)
        : base(mediator, logger)
    {
    }

    /// <summary>
    /// Creates a new attachment type.
    /// </summary>
    /// <param name="command">The attachment type creation details.</param>
    /// <returns>The newly created attachment type.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<AttachmentTypeDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateAttachmentTypeCommand command)
    {
        _logger.LogInformation("Creating new Attachment Type with Type: {Type}", command.Type);

        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = result }, Success(result, "Attachment Type created successfully"));
    }

    /// <summary>
    /// Updates an existing attachment type.
    /// </summary>
    /// <param name="id">The ID of the attachment type to update.</param>
    /// <param name="command">The updated attachment type details.</param>
    /// <returns>The updated attachment type.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<AttachmentTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAttachmentTypeCommand command)
    {
        if (command.Id != id)
        {
            return BadRequestResponse<AttachmentTypeDto>("ID in the URL does not match the ID in the request body");
        }

        _logger.LogInformation("Updating Attachment Type with ID: {Id}", id);

        var result = await _mediator.Send(command);

        return Ok(Success(result, "Attachment Type updated successfully"));
    }

    /// <summary>
    /// Deletes an attachment type by its ID.
    /// </summary>
    /// <param name="id">The ID of the attachment type to delete.</param>
    /// <returns>A success response if deletion was successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting Attachment Type with ID: {Id}", id);

        var command = new DeleteAttachmentTypeCommand { Id = id };
        var result = await _mediator.Send(command);

        return Ok(Success(result, "Attachment Type deleted successfully"));
    }

    /// <summary>
    /// Gets an attachment type by its ID.
    /// </summary>
    /// <param name="id">The ID of the attachment type to retrieve.</param>
    /// <returns>The attachment type details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<AttachmentTypeDetailsDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Getting Attachment Type with ID: {Id}", id);

        var query = new GetAttachmentTypeByIdQuery { Id = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<AttachmentTypeDetailsDto>($"Attachment Type with ID {id} not found");
        }

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a list of all attachment types.
    /// </summary>
    /// <returns>A list of all attachment types.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<AttachmentTypeListDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all Attachment Types");

        var query = new GetAllAttachmentTypesQuery();
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets attachment types by type name.
    /// </summary>
    /// <param name="type">The type name to search for.</param>
    /// <returns>A list of attachment types matching the specified type name.</returns>
    [HttpGet("bytype/{type}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<AttachmentTypeListDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByType(string type)
    {
        _logger.LogInformation("Getting Attachment Types with Type: {Type}", type);

        var query = new GetAttachmentTypeByTypeQuery { Type = type };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets attachment types by file extension.
    /// </summary>
    /// <param name="extension">The file extension to search for (e.g., ".pdf", ".docx").</param>
    /// <returns>A list of attachment types that support the specified file extension.</returns>
    [HttpGet("byextension/{extension}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<AttachmentTypeListDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByFileExtension(string extension)
    {
        _logger.LogInformation("Getting Attachment Types for file extension: {Extension}", extension);

        var query = new GetAttachmentTypeByFileExtensionQuery { FileExtension = extension };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets attachment types ordered by their ordinal position.
    /// </summary>
    /// <returns>A list of attachment types ordered by ordinal position.</returns>
    [HttpGet("byposition")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<AttachmentTypeListDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByOrdinalPosition()
    {
        _logger.LogInformation("Getting Attachment Types ordered by ordinal position");

        var query = new GetAttachmentTypeByOrdinalPositionQuery();
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets the default attachment type.
    /// </summary>
    /// <returns>The default attachment type.</returns>
    [HttpGet("default")]
    [ProducesResponseType(typeof(ApiResponse<AttachmentTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDefault()
    {
        _logger.LogInformation("Getting default Attachment Type");

        var query = new GetDefaultAttachmentTypeQuery();
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<AttachmentTypeDto>("Default Attachment Type not found");
        }

        return Ok(Success(result));
    }
}