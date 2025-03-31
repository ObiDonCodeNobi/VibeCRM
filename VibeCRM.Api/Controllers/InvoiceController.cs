using MediatR;
using Microsoft.AspNetCore.Mvc;
using VibeCRM.Application.Features.Invoice.Commands.CreateInvoice;
using VibeCRM.Application.Features.Invoice.Commands.DeleteInvoice;
using VibeCRM.Application.Features.Invoice.Commands.UpdateInvoice;
using VibeCRM.Application.Features.Invoice.Queries.GetAllInvoices;
using VibeCRM.Application.Features.Invoice.Queries.GetInvoiceById;
using VibeCRM.Application.Features.Invoice.Queries.GetInvoicesBySalesOrderId;
using VibeCRM.Shared.DTOs.Invoice;
using VibeCRM.Shared.Models;

namespace VibeCRM.Api.Controllers;

/// <summary>
/// API endpoints for managing invoice resources.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class InvoiceController : ApiControllerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvoiceController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for sending commands and queries.</param>
    /// <param name="logger">The logger instance for logging controller actions.</param>
    public InvoiceController(IMediator mediator, ILogger<InvoiceController> logger)
        : base(mediator, logger)
    {
    }

    /// <summary>
    /// Creates a new invoice.
    /// </summary>
    /// <param name="command">The invoice creation details.</param>
    /// <returns>The newly created invoice.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<InvoiceDetailsDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateInvoiceCommand command)
    {
        _logger.LogInformation("Creating new Invoice with Number: {Number}", command.Number);

        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = result }, Success(result, "Invoice created successfully"));
    }

    /// <summary>
    /// Updates an existing invoice.
    /// </summary>
    /// <param name="id">The ID of the invoice to update.</param>
    /// <param name="command">The updated invoice details.</param>
    /// <returns>The updated invoice.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<InvoiceDetailsDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateInvoiceCommand command)
    {
        if (id != command.Id)
        {
            return BadRequestResponse<InvoiceDetailsDto>("ID in the URL does not match the ID in the request body");
        }

        _logger.LogInformation("Updating Invoice with ID: {Id}", id);

        var result = await _mediator.Send(command);

        return Ok(Success(result, "Invoice updated successfully"));
    }

    /// <summary>
    /// Deletes an invoice by its ID.
    /// </summary>
    /// <param name="id">The ID of the invoice to delete.</param>
    /// <returns>A success response if deletion was successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting Invoice with ID: {Id}", id);

        var command = new DeleteInvoiceCommand
        {
            Id = id,
            ModifiedBy = Guid.Parse(User.Identity?.Name ?? Guid.Empty.ToString()),
            ModifiedDate = DateTime.UtcNow
        };
        var result = await _mediator.Send(command);

        return Ok(Success(result, "Invoice deleted successfully"));
    }

    /// <summary>
    /// Gets an invoice by its ID.
    /// </summary>
    /// <param name="id">The ID of the invoice to retrieve.</param>
    /// <returns>The invoice details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<InvoiceDetailsDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Getting Invoice with ID: {Id}", id);

        var query = new GetInvoiceByIdQuery { Id = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<InvoiceDetailsDto>($"Invoice with ID {id} not found");
        }

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a list of all invoices.
    /// </summary>
    /// <returns>A list of all invoices.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<InvoiceListDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all Invoices");

        var query = new GetAllInvoicesQuery();
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets invoices by sales order ID.
    /// </summary>
    /// <param name="salesOrderId">The ID of the sales order to filter by.</param>
    /// <returns>A list of invoices for the specified sales order.</returns>
    [HttpGet("bysalesorder/{salesOrderId}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<InvoiceListDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBySalesOrder(Guid salesOrderId)
    {
        _logger.LogInformation("Getting Invoices for Sales Order ID: {SalesOrderId}", salesOrderId);

        var query = new GetInvoicesBySalesOrderIdQuery { SalesOrderId = salesOrderId };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }
}