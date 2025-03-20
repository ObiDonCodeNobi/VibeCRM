using MediatR;
using Microsoft.AspNetCore.Mvc;
using VibeCRM.Application.Common.Models;
using VibeCRM.Application.Features.SalesOrder.Commands.CreateSalesOrder;
using VibeCRM.Application.Features.SalesOrder.Commands.DeleteSalesOrder;
using VibeCRM.Application.Features.SalesOrder.Commands.UpdateSalesOrder;
using VibeCRM.Application.Features.SalesOrder.DTOs;
using VibeCRM.Application.Features.SalesOrder.Queries.GetAllSalesOrders;
using VibeCRM.Application.Features.SalesOrder.Queries.GetSalesOrderByActivity;
using VibeCRM.Application.Features.SalesOrder.Queries.GetSalesOrderByCompany;
using VibeCRM.Application.Features.SalesOrder.Queries.GetSalesOrderById;
using VibeCRM.Application.Features.SalesOrder.Queries.GetSalesOrderByNumber;
using VibeCRM.Application.Features.SalesOrder.Queries.GetSalesOrderByOrderDateRange;
using VibeCRM.Application.Features.SalesOrder.Queries.GetSalesOrderByQuote;

namespace VibeCRM.Api.Controllers;

/// <summary>
/// API endpoints for managing sales order resources.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SalesOrderController : ApiControllerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SalesOrderController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for sending commands and queries.</param>
    /// <param name="logger">The logger instance for logging controller actions.</param>
    public SalesOrderController(IMediator mediator, ILogger<SalesOrderController> logger)
        : base(mediator, logger)
    {
    }

    /// <summary>
    /// Creates a new sales order.
    /// </summary>
    /// <param name="command">The sales order creation details.</param>
    /// <returns>The newly created sales order.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<SalesOrderDetailsDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateSalesOrderCommand command)
    {
        _logger.LogInformation("Creating new Sales Order with Number: {Number}", command.Number);

        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, Success(result, "Sales Order created successfully"));
    }

    /// <summary>
    /// Updates an existing sales order.
    /// </summary>
    /// <param name="id">The ID of the sales order to update.</param>
    /// <param name="command">The updated sales order details.</param>
    /// <returns>The updated sales order.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<SalesOrderDetailsDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSalesOrderCommand command)
    {
        if (id != command.Id)
        {
            return BadRequestResponse<SalesOrderDetailsDto>("ID in the URL does not match the ID in the request body");
        }

        _logger.LogInformation("Updating Sales Order with ID: {Id}", id);

        var result = await _mediator.Send(command);

        return Ok(Success(result, "Sales Order updated successfully"));
    }

    /// <summary>
    /// Deletes a sales order by its ID.
    /// </summary>
    /// <param name="id">The ID of the sales order to delete.</param>
    /// <returns>A success response if deletion was successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting Sales Order with ID: {Id}", id);

        var command = new DeleteSalesOrderCommand
        {
            Id = id,
            ModifiedBy = Guid.Parse(User.Identity?.Name ?? Guid.Empty.ToString())
        };
        var result = await _mediator.Send(command);

        return Ok(Success(result, "Sales Order deleted successfully"));
    }

    /// <summary>
    /// Gets a sales order by its ID.
    /// </summary>
    /// <param name="id">The ID of the sales order to retrieve.</param>
    /// <returns>The sales order details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<SalesOrderDetailsDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Getting Sales Order with ID: {Id}", id);

        var query = new GetSalesOrderByIdQuery { Id = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<SalesOrderDetailsDto>($"Sales Order with ID {id} not found");
        }

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a list of all sales orders.
    /// </summary>
    /// <returns>A list of all sales orders.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<SalesOrderListDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all Sales Orders");

        var query = new GetAllSalesOrdersQuery();
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets sales orders by company ID.
    /// </summary>
    /// <param name="companyId">The ID of the company to filter by.</param>
    /// <returns>A list of sales orders for the specified company.</returns>
    [HttpGet("bycompany/{companyId}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<SalesOrderListDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByCompany(Guid companyId)
    {
        _logger.LogInformation("Getting Sales Orders for Company ID: {CompanyId}", companyId);

        var query = new GetSalesOrderByCompanyQuery { CompanyId = companyId };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets sales orders by activity ID.
    /// </summary>
    /// <param name="activityId">The ID of the activity to filter by.</param>
    /// <returns>A list of sales orders for the specified activity.</returns>
    [HttpGet("byactivity/{activityId}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<SalesOrderListDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByActivity(Guid activityId)
    {
        _logger.LogInformation("Getting Sales Orders for Activity ID: {ActivityId}", activityId);

        var query = new GetSalesOrderByActivityQuery { ActivityId = activityId };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets sales orders by quote ID.
    /// </summary>
    /// <param name="quoteId">The ID of the quote to filter by.</param>
    /// <returns>A list of sales orders for the specified quote.</returns>
    [HttpGet("byquote/{quoteId}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<SalesOrderListDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByQuote(Guid quoteId)
    {
        _logger.LogInformation("Getting Sales Orders for Quote ID: {QuoteId}", quoteId);

        var query = new GetSalesOrderByQuoteQuery { QuoteId = quoteId };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets sales orders by order number.
    /// </summary>
    /// <param name="number">The order number to search for.</param>
    /// <returns>A list of sales orders matching the specified number.</returns>
    [HttpGet("bynumber/{number}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<SalesOrderListDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByNumber(string number)
    {
        _logger.LogInformation("Getting Sales Orders with Number: {Number}", number);

        var query = new GetSalesOrderByNumberQuery { Number = number };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets sales orders by order date range.
    /// </summary>
    /// <param name="startDate">The start date of the range.</param>
    /// <param name="endDate">The end date of the range.</param>
    /// <returns>A list of sales orders within the specified date range.</returns>
    [HttpGet("bydaterange")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<SalesOrderListDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        if (startDate > endDate)
        {
            return BadRequestResponse<IEnumerable<SalesOrderListDto>>("Start date cannot be later than end date");
        }

        _logger.LogInformation("Getting Sales Orders between {StartDate} and {EndDate}", startDate, endDate);

        var query = new GetSalesOrderByOrderDateRangeQuery { StartDate = startDate, EndDate = endDate };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }
}