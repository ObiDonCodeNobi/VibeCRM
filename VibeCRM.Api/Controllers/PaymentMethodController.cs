using MediatR;
using Microsoft.AspNetCore.Mvc;
using VibeCRM.Application.Features.PaymentMethod.Commands.CreatePaymentMethod;
using VibeCRM.Application.Features.PaymentMethod.Commands.DeletePaymentMethod;
using VibeCRM.Application.Features.PaymentMethod.Commands.UpdatePaymentMethod;
using VibeCRM.Application.Features.PaymentMethod.Queries.GetAllPaymentMethods;
using VibeCRM.Application.Features.PaymentMethod.Queries.GetDefaultPaymentMethod;
using VibeCRM.Application.Features.PaymentMethod.Queries.GetPaymentMethodById;
using VibeCRM.Application.Features.PaymentMethod.Queries.GetPaymentMethodByName;
using VibeCRM.Application.Features.PaymentMethod.Queries.GetPaymentMethodByOrdinalPosition;
using VibeCRM.Shared.DTOs.PaymentMethod;
using VibeCRM.Shared.Models;

namespace VibeCRM.Api.Controllers;

/// <summary>
/// API endpoints for managing payment method reference data.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PaymentMethodController : ApiControllerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PaymentMethodController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for sending commands and queries.</param>
    /// <param name="logger">The logger instance for logging controller actions.</param>
    public PaymentMethodController(IMediator mediator, ILogger<PaymentMethodController> logger)
        : base(mediator, logger)
    {
    }

    /// <summary>
    /// Creates a new payment method.
    /// </summary>
    /// <param name="command">The payment method creation details.</param>
    /// <returns>The newly created payment method.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<PaymentMethodDetailsDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreatePaymentMethodCommand command)
    {
        _logger.LogInformation("Creating new Payment Method with Name: {Name}", command.Name);

        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, Success(result, "Payment Method created successfully"));
    }

    /// <summary>
    /// Updates an existing payment method.
    /// </summary>
    /// <param name="id">The ID of the payment method to update.</param>
    /// <param name="command">The updated payment method details.</param>
    /// <returns>The updated payment method.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<PaymentMethodDetailsDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePaymentMethodCommand command)
    {
        if (id != command.Id)
        {
            return BadRequestResponse<PaymentMethodDetailsDto>("ID in the URL does not match the ID in the request body");
        }

        _logger.LogInformation("Updating Payment Method with ID: {Id}", id);

        var result = await _mediator.Send(command);

        return Ok(Success(result, "Payment Method updated successfully"));
    }

    /// <summary>
    /// Deletes a payment method by its ID.
    /// </summary>
    /// <param name="id">The ID of the payment method to delete.</param>
    /// <returns>A success response if deletion was successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting Payment Method with ID: {Id}", id);

        var command = new DeletePaymentMethodCommand { Id = id };
        var result = await _mediator.Send(command);

        return Ok(Success(result, "Payment Method deleted successfully"));
    }

    /// <summary>
    /// Gets a payment method by its ID.
    /// </summary>
    /// <param name="id">The ID of the payment method to retrieve.</param>
    /// <returns>The payment method details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<PaymentMethodDetailsDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Getting Payment Method with ID: {Id}", id);

        var query = new GetPaymentMethodByIdQuery { Id = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<PaymentMethodDetailsDto>($"Payment Method with ID {id} not found");
        }

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a list of all payment methods.
    /// </summary>
    /// <returns>A list of all payment methods.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<PaymentMethodListDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all Payment Methods");

        var query = new GetAllPaymentMethodsQuery();
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets payment methods by name.
    /// </summary>
    /// <param name="name">The name to search for.</param>
    /// <returns>A list of payment methods matching the specified name.</returns>
    [HttpGet("byname/{name}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<PaymentMethodListDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByName(string name)
    {
        _logger.LogInformation("Getting Payment Methods with Name: {Name}", name);

        var query = new GetPaymentMethodByNameQuery { Name = name };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets payment methods by ordinal position.
    /// </summary>
    /// <param name="position">The ordinal position to search for.</param>
    /// <returns>A list of payment methods with the specified ordinal position.</returns>
    [HttpGet("byposition/{position:int}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<PaymentMethodListDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByOrdinalPosition(int position)
    {
        _logger.LogInformation("Getting Payment Methods with Ordinal Position: {Position}", position);

        var query = new GetPaymentMethodByOrdinalPositionQuery();
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets the default payment method.
    /// </summary>
    /// <returns>The default payment method.</returns>
    [HttpGet("default")]
    [ProducesResponseType(typeof(ApiResponse<PaymentMethodDetailsDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDefault()
    {
        _logger.LogInformation("Getting default Payment Method");

        var query = new GetDefaultPaymentMethodQuery();
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<PaymentMethodDetailsDto>("Default Payment Method not found");
        }

        return Ok(Success(result));
    }
}