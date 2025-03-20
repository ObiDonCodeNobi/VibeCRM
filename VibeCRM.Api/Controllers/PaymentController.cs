using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Common.Models;
using VibeCRM.Application.Features.Payment.Commands.CreatePayment;
using VibeCRM.Application.Features.Payment.Commands.DeletePayment;
using VibeCRM.Application.Features.Payment.Commands.UpdatePayment;
using VibeCRM.Application.Features.Payment.DTOs;
using VibeCRM.Application.Features.Payment.Queries.GetAllPayments;
using VibeCRM.Application.Features.Payment.Queries.GetPaymentById;
using VibeCRM.Application.Features.Payment.Queries.GetPaymentsByCompany;
using VibeCRM.Application.Features.Payment.Queries.GetPaymentsByDateRange;
using VibeCRM.Application.Features.Payment.Queries.GetPaymentsByInvoice;
using VibeCRM.Application.Features.Payment.Queries.GetPaymentsByMethod;
using VibeCRM.Application.Features.Payment.Queries.GetPaymentsByPerson;
using VibeCRM.Application.Features.Payment.Queries.GetPaymentsByStatus;

namespace VibeCRM.Api.Controllers;

/// <summary>
/// API endpoints for managing payment resources.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PaymentController : ApiControllerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PaymentController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for sending commands and queries.</param>
    /// <param name="logger">The logger instance for logging controller actions.</param>
    public PaymentController(IMediator mediator, ILogger<PaymentController> logger)
        : base(mediator, logger)
    {
    }

    /// <summary>
    /// Creates a new payment.
    /// </summary>
    /// <param name="command">The payment creation details.</param>
    /// <returns>The newly created payment.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<PaymentDetailsDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreatePaymentCommand command)
    {
        _logger.LogInformation("Creating new Payment with Reference Number: {ReferenceNumber}", command.ReferenceNumber);
        
        var result = await _mediator.Send(command);
        
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, Success(result, "Payment created successfully"));
    }

    /// <summary>
    /// Updates an existing payment.
    /// </summary>
    /// <param name="id">The ID of the payment to update.</param>
    /// <param name="command">The updated payment details.</param>
    /// <returns>The updated payment.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<PaymentDetailsDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePaymentCommand command)
    {
        if (id != command.Id)
        {
            return BadRequestResponse<PaymentDetailsDto>("ID in the URL does not match the ID in the request body");
        }

        _logger.LogInformation("Updating Payment with ID: {Id}", id);
        
        var result = await _mediator.Send(command);
        
        return Ok(Success(result, "Payment updated successfully"));
    }

    /// <summary>
    /// Deletes a payment by its ID.
    /// </summary>
    /// <param name="id">The ID of the payment to delete.</param>
    /// <returns>A success response if deletion was successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting Payment with ID: {Id}", id);
        
        var command = new DeletePaymentCommand 
        { 
            Id = id,
            ModifiedBy = Guid.Parse(User.Identity?.Name ?? Guid.Empty.ToString())
        };
        var result = await _mediator.Send(command);
        
        return Ok(Success(result, "Payment deleted successfully"));
    }

    /// <summary>
    /// Gets a payment by its ID.
    /// </summary>
    /// <param name="id">The ID of the payment to retrieve.</param>
    /// <returns>The payment details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<PaymentDetailsDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Getting Payment with ID: {Id}", id);
        
        var query = new GetPaymentByIdQuery { Id = id };
        var result = await _mediator.Send(query);
        
        if (result == null)
        {
            return NotFoundResponse<PaymentDetailsDto>($"Payment with ID {id} not found");
        }
        
        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a list of all payments.
    /// </summary>
    /// <returns>A list of all payments.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<PaymentListDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all Payments");
        
        var query = new GetAllPaymentsQuery();
        var result = await _mediator.Send(query);
        
        return Ok(Success(result));
    }

    /// <summary>
    /// Gets payments by invoice ID.
    /// </summary>
    /// <param name="invoiceId">The ID of the invoice to filter by.</param>
    /// <returns>A list of payments for the specified invoice.</returns>
    [HttpGet("byinvoice/{invoiceId}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<PaymentListDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByInvoice(Guid invoiceId)
    {
        _logger.LogInformation("Getting Payments for Invoice ID: {InvoiceId}", invoiceId);
        
        var query = new GetPaymentsByInvoiceQuery { InvoiceId = invoiceId };
        var result = await _mediator.Send(query);
        
        return Ok(Success(result));
    }

    /// <summary>
    /// Gets payments by company ID.
    /// </summary>
    /// <param name="companyId">The ID of the company to filter by.</param>
    /// <returns>A list of payments for the specified company.</returns>
    [HttpGet("bycompany/{companyId}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<PaymentListDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByCompany(Guid companyId)
    {
        _logger.LogInformation("Getting Payments for Company ID: {CompanyId}", companyId);
        
        var query = new GetPaymentsByCompanyQuery { CompanyId = companyId };
        var result = await _mediator.Send(query);
        
        return Ok(Success(result));
    }

    /// <summary>
    /// Gets payments by person ID.
    /// </summary>
    /// <param name="personId">The ID of the person to filter by.</param>
    /// <returns>A list of payments for the specified person.</returns>
    [HttpGet("byperson/{personId}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<PaymentListDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByPerson(Guid personId)
    {
        _logger.LogInformation("Getting Payments for Person ID: {PersonId}", personId);
        
        var query = new GetPaymentsByPersonQuery { PersonId = personId };
        var result = await _mediator.Send(query);
        
        return Ok(Success(result));
    }

    /// <summary>
    /// Gets payments by payment method ID.
    /// </summary>
    /// <param name="methodId">The ID of the payment method to filter by.</param>
    /// <returns>A list of payments for the specified payment method.</returns>
    [HttpGet("bymethod/{methodId}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<PaymentListDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByMethod(Guid methodId)
    {
        _logger.LogInformation("Getting Payments for Method ID: {MethodId}", methodId);
        
        var query = new GetPaymentsByMethodQuery { PaymentMethodId = methodId };
        var result = await _mediator.Send(query);
        
        return Ok(Success(result));
    }

    /// <summary>
    /// Gets payments by payment status ID.
    /// </summary>
    /// <param name="statusId">The ID of the payment status to filter by.</param>
    /// <returns>A list of payments with the specified status.</returns>
    [HttpGet("bystatus/{statusId}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<PaymentListDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByStatus(Guid statusId)
    {
        _logger.LogInformation("Getting Payments with Status ID: {StatusId}", statusId);
        
        var query = new GetPaymentsByStatusQuery { PaymentStatusId = statusId };
        var result = await _mediator.Send(query);
        
        return Ok(Success(result));
    }

    /// <summary>
    /// Gets payments by date range.
    /// </summary>
    /// <param name="startDate">The start date of the range.</param>
    /// <param name="endDate">The end date of the range.</param>
    /// <returns>A list of payments within the specified date range.</returns>
    [HttpGet("bydaterange")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<PaymentListDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        if (startDate > endDate)
        {
            return BadRequestResponse<IEnumerable<PaymentListDto>>("Start date cannot be later than end date");
        }

        _logger.LogInformation("Getting Payments between {StartDate} and {EndDate}", startDate, endDate);
        
        var query = new GetPaymentsByDateRangeQuery { StartDate = startDate, EndDate = endDate };
        var result = await _mediator.Send(query);
        
        return Ok(Success(result));
    }
}
