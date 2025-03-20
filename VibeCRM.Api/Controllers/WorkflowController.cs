using MediatR;
using Microsoft.AspNetCore.Mvc;
using VibeCRM.Application.Common.Models;
using VibeCRM.Application.Features.Workflow.Commands.CreateWorkflow;
using VibeCRM.Application.Features.Workflow.Commands.DeleteWorkflow;
using VibeCRM.Application.Features.Workflow.Commands.UpdateWorkflow;
using VibeCRM.Application.Features.Workflow.DTOs;
using VibeCRM.Application.Features.Workflow.Queries.GetAllWorkflows;
using VibeCRM.Application.Features.Workflow.Queries.GetWorkflowById;
using VibeCRM.Application.Features.Workflow.Queries.GetWorkflowsByWorkflowType;

namespace VibeCRM.Api.Controllers;

/// <summary>
/// API endpoints for managing workflow resources.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class WorkflowController : ApiControllerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WorkflowController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for sending commands and queries.</param>
    /// <param name="logger">The logger instance for logging controller actions.</param>
    public WorkflowController(IMediator mediator, ILogger<WorkflowController> logger)
        : base(mediator, logger)
    {
    }

    /// <summary>
    /// Creates a new workflow.
    /// </summary>
    /// <param name="command">The workflow creation details.</param>
    /// <returns>The newly created workflow.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<WorkflowDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateWorkflowCommand command)
    {
        _logger.LogInformation("Creating new Workflow with Subject: {Subject}", command.Subject);

        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, Success(result, "Workflow created successfully"));
    }

    /// <summary>
    /// Updates an existing workflow.
    /// </summary>
    /// <param name="id">The ID of the workflow to update.</param>
    /// <param name="command">The updated workflow details.</param>
    /// <returns>The updated workflow.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<WorkflowDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateWorkflowCommand command)
    {
        if (id != command.Id)
        {
            return BadRequestResponse<WorkflowDto>("ID in the URL does not match the ID in the request body");
        }

        _logger.LogInformation("Updating Workflow with ID: {Id}", id);

        var result = await _mediator.Send(command);

        return Ok(Success(result, "Workflow updated successfully"));
    }

    /// <summary>
    /// Deletes a workflow by its ID.
    /// </summary>
    /// <param name="id">The ID of the workflow to delete.</param>
    /// <returns>A success response if deletion was successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting Workflow with ID: {Id}", id);

        var command = new DeleteWorkflowCommand
        {
            Id = id,
            ModifiedBy = Guid.Parse(User.Identity?.Name ?? Guid.Empty.ToString())
        };
        var result = await _mediator.Send(command);

        return Ok(Success(result, "Workflow deleted successfully"));
    }

    /// <summary>
    /// Gets a workflow by its ID.
    /// </summary>
    /// <param name="id">The ID of the workflow to retrieve.</param>
    /// <returns>The workflow details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<WorkflowDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Getting Workflow with ID: {Id}", id);

        var query = new GetWorkflowByIdQuery { Id = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<WorkflowDto>($"Workflow with ID {id} not found");
        }

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a list of all workflows.
    /// </summary>
    /// <returns>A list of all workflows.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<WorkflowDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all Workflows");

        var query = new GetAllWorkflowsQuery();
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets workflows by workflow type ID.
    /// </summary>
    /// <param name="workflowTypeId">The ID of the workflow type to filter by.</param>
    /// <returns>A list of workflows with the specified workflow type.</returns>
    [HttpGet("bytype/{workflowTypeId}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<WorkflowDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByWorkflowType(Guid workflowTypeId)
    {
        _logger.LogInformation("Getting Workflows with Workflow Type ID: {WorkflowTypeId}", workflowTypeId);

        var query = new GetWorkflowsByWorkflowTypeQuery { WorkflowTypeId = workflowTypeId };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }
}