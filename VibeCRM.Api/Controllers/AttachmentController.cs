using MediatR;
using Microsoft.AspNetCore.Mvc;
using VibeCRM.Application.Common.Models;
using VibeCRM.Application.Features.Attachment.Commands.CreateAttachment;
using VibeCRM.Application.Features.Attachment.Commands.DeleteAttachment;
using VibeCRM.Application.Features.Attachment.Commands.UpdateAttachment;
using VibeCRM.Application.Features.Attachment.DTOs;
using VibeCRM.Application.Features.Attachment.Queries.GetAllAttachments;
using VibeCRM.Application.Features.Attachment.Queries.GetAttachmentById;

namespace VibeCRM.Api.Controllers;

/// <summary>
/// API endpoints for managing attachment resources.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AttachmentController : ApiControllerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AttachmentController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for sending commands and queries.</param>
    /// <param name="logger">The logger instance for logging controller actions.</param>
    public AttachmentController(IMediator mediator, ILogger<AttachmentController> logger)
        : base(mediator, logger)
    {
    }

    /// <summary>
    /// Creates a new attachment.
    /// </summary>
    /// <param name="command">The attachment creation details.</param>
    /// <returns>The newly created attachment.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<AttachmentDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateAttachmentCommand command)
    {
        _logger.LogInformation("Creating new Attachment with Subject: {Subject}", command.Subject);

        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, Success(result, "Attachment created successfully"));
    }

    /// <summary>
    /// Updates an existing attachment.
    /// </summary>
    /// <param name="id">The ID of the attachment to update.</param>
    /// <param name="command">The updated attachment details.</param>
    /// <returns>The updated attachment.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<AttachmentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAttachmentCommand command)
    {
        if (id != command.Id)
        {
            return BadRequestResponse<AttachmentDto>("ID in the URL does not match the ID in the request body");
        }

        _logger.LogInformation("Updating Attachment with ID: {Id}", id);

        var result = await _mediator.Send(command);

        return Ok(Success(result, "Attachment updated successfully"));
    }

    /// <summary>
    /// Deletes an attachment by its ID.
    /// </summary>
    /// <param name="id">The ID of the attachment to delete.</param>
    /// <returns>A success response if deletion was successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting Attachment with ID: {Id}", id);

        var command = new DeleteAttachmentCommand(id, Guid.Parse(User.Identity?.Name ?? Guid.Empty.ToString()));
        var result = await _mediator.Send(command);

        return Ok(Success(result, "Attachment deleted successfully"));
    }

    /// <summary>
    /// Gets an attachment by its ID.
    /// </summary>
    /// <param name="id">The ID of the attachment to retrieve.</param>
    /// <returns>The attachment details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<AttachmentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Getting Attachment with ID: {Id}", id);

        var query = new GetAttachmentByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<AttachmentDto>($"Attachment with ID {id} not found");
        }

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a list of all attachments.
    /// </summary>
    /// <returns>A list of all attachments.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<AttachmentDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all Attachments");

        var query = new GetAllAttachmentsQuery();
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }
}