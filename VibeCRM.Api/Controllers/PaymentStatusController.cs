using MediatR;
using Microsoft.AspNetCore.Mvc;
using VibeCRM.Application.Features.PaymentStatus.Commands.CreatePaymentStatus;
using VibeCRM.Application.Features.PaymentStatus.Commands.DeletePaymentStatus;
using VibeCRM.Application.Features.PaymentStatus.Commands.UpdatePaymentStatus;
using VibeCRM.Application.Features.PaymentStatus.Queries.GetAllPaymentStatuses;
using VibeCRM.Application.Features.PaymentStatus.Queries.GetPaymentStatusById;
using VibeCRM.Application.Features.PaymentStatus.Queries.GetPaymentStatusByStatus;
using VibeCRM.Shared.DTOs.PaymentStatus;
using VibeCRM.Shared.Models;

namespace VibeCRM.Api.Controllers;

/// <summary>
/// API endpoints for managing payment status reference data.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PaymentStatusController : ApiControllerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PaymentStatusController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for sending commands and queries.</param>
    /// <param name="logger">The logger instance for logging controller actions.</param>
    public PaymentStatusController(IMediator mediator, ILogger<PaymentStatusController> logger)
        : base(mediator, logger)
    {
    }

    /// <summary>
    /// Creates a new payment status.
    /// </summary>
    /// <param name="command">The payment status creation details.</param>
    /// <returns>The newly created payment status.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<PaymentStatusDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreatePaymentStatusCommand command)
    {
        _logger.LogInformation("Creating new Payment Status with Status: {Status}", command.Status);

        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, Success(result, "Payment Status created successfully"));
    }

    /// <summary>
    /// Updates an existing payment status.
    /// </summary>
    /// <param name="id">The ID of the payment status to update.</param>
    /// <param name="command">The updated payment status details.</param>
    /// <returns>The updated payment status.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<PaymentStatusDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePaymentStatusCommand command)
    {
        if (id != command.Id)
        {
            return BadRequestResponse<PaymentStatusDto>("ID in the URL does not match the ID in the request body");
        }

        _logger.LogInformation("Updating Payment Status with ID: {Id}", id);

        var result = await _mediator.Send(command);

        return Ok(Success(result, "Payment Status updated successfully"));
    }

    /// <summary>
    /// Deletes a payment status by its ID.
    /// </summary>
    /// <param name="id">The ID of the payment status to delete.</param>
    /// <returns>A success response if deletion was successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting Payment Status with ID: {Id}", id);

        var command = new DeletePaymentStatusCommand { Id = id };
        var result = await _mediator.Send(command);

        return Ok(Success(result, "Payment Status deleted successfully"));
    }

    /// <summary>
    /// Gets a payment status by its ID.
    /// </summary>
    /// <param name="id">The ID of the payment status to retrieve.</param>
    /// <returns>The payment status details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<PaymentStatusDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Getting Payment Status with ID: {Id}", id);

        var query = new GetPaymentStatusByIdQuery { Id = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<PaymentStatusDto>($"Payment Status with ID {id} not found");
        }

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a list of all payment statuses.
    /// </summary>
    /// <returns>A list of all payment statuses.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<PaymentStatusDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all Payment Statuses");

        var query = new GetAllPaymentStatusesQuery();
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets payment statuses by status name.
    /// </summary>
    /// <param name="status">The status name to search for.</param>
    /// <returns>A list of payment statuses matching the specified status name.</returns>
    [HttpGet("bystatus/{status}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<PaymentStatusDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByStatus(string status)
    {
        _logger.LogInformation("Getting Payment Statuses with Status: {Status}", status);

        var query = new GetPaymentStatusByStatusQuery { Status = status };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }
}