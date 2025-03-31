using MediatR;
using Microsoft.AspNetCore.Mvc;
using VibeCRM.Application.Features.SalesOrderLineItem.Commands.CreateSalesOrderLineItem;
using VibeCRM.Application.Features.SalesOrderLineItem.Commands.DeleteSalesOrderLineItem;
using VibeCRM.Application.Features.SalesOrderLineItem.Commands.UpdateSalesOrderLineItem;
using VibeCRM.Application.Features.SalesOrderLineItem.Queries.GetAllSalesOrderLineItems;
using VibeCRM.Application.Features.SalesOrderLineItem.Queries.GetSalesOrderLineItemById;
using VibeCRM.Application.Features.SalesOrderLineItem.Queries.GetSalesOrderLineItemsByProduct;
using VibeCRM.Application.Features.SalesOrderLineItem.Queries.GetSalesOrderLineItemsBySalesOrder;
using VibeCRM.Application.Features.SalesOrderLineItem.Queries.GetSalesOrderLineItemsByService;
using VibeCRM.Application.Features.SalesOrderLineItem.Queries.GetTotalForSalesOrder;
using VibeCRM.Shared.DTOs.SalesOrderLineItem;
using VibeCRM.Shared.Models;

namespace VibeCRM.Api.Controllers;

/// <summary>
/// API endpoints for managing sales order line items.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SalesOrderLineItemController : ApiControllerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SalesOrderLineItemController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for sending commands and queries.</param>
    /// <param name="logger">The logger instance for logging controller actions.</param>
    public SalesOrderLineItemController(IMediator mediator, ILogger<SalesOrderLineItemController> logger)
        : base(mediator, logger)
    {
    }

    /// <summary>
    /// Creates a new sales order line item.
    /// </summary>
    /// <param name="command">The sales order line item creation details.</param>
    /// <returns>The newly created sales order line item.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<SalesOrderLineItemDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateSalesOrderLineItemCommand command)
    {
        _logger.LogInformation("Creating new Sales Order Line Item for Sales Order ID: {SalesOrderId}", command.SalesOrderId);

        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, Success(result, "Sales Order Line Item created successfully"));
    }

    /// <summary>
    /// Updates an existing sales order line item.
    /// </summary>
    /// <param name="id">The ID of the sales order line item to update.</param>
    /// <param name="command">The updated sales order line item details.</param>
    /// <returns>The updated sales order line item.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<SalesOrderLineItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSalesOrderLineItemCommand command)
    {
        if (id != command.Id)
        {
            return BadRequestResponse<SalesOrderLineItemDto>("ID in the URL does not match the ID in the request body");
        }

        _logger.LogInformation("Updating Sales Order Line Item with ID: {Id}", id);

        var result = await _mediator.Send(command);

        return Ok(Success(result, "Sales Order Line Item updated successfully"));
    }

    /// <summary>
    /// Deletes a sales order line item by its ID.
    /// </summary>
    /// <param name="id">The ID of the sales order line item to delete.</param>
    /// <returns>A success response if deletion was successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting Sales Order Line Item with ID: {Id}", id);

        var command = new DeleteSalesOrderLineItemCommand { Id = id };
        var result = await _mediator.Send(command);

        return Ok(Success(result, "Sales Order Line Item deleted successfully"));
    }

    /// <summary>
    /// Gets a sales order line item by its ID.
    /// </summary>
    /// <param name="id">The ID of the sales order line item to retrieve.</param>
    /// <returns>The sales order line item details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<SalesOrderLineItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Getting Sales Order Line Item with ID: {Id}", id);

        var query = new GetSalesOrderLineItemByIdQuery { Id = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<SalesOrderLineItemDto>($"Sales Order Line Item with ID {id} not found");
        }

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a list of all sales order line items.
    /// </summary>
    /// <returns>A list of all sales order line items.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<SalesOrderLineItemDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all Sales Order Line Items");

        var query = new GetAllSalesOrderLineItemsQuery();
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets sales order line items by sales order ID.
    /// </summary>
    /// <param name="salesOrderId">The sales order ID to search for.</param>
    /// <returns>A list of sales order line items for the specified sales order.</returns>
    [HttpGet("bysalesorder/{salesOrderId}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<SalesOrderLineItemDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBySalesOrder(Guid salesOrderId)
    {
        _logger.LogInformation("Getting Sales Order Line Items for Sales Order ID: {SalesOrderId}", salesOrderId);

        var query = new GetSalesOrderLineItemsBySalesOrderQuery { SalesOrderId = salesOrderId };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets sales order line items by product ID.
    /// </summary>
    /// <param name="productId">The product ID to search for.</param>
    /// <returns>A list of sales order line items for the specified product.</returns>
    [HttpGet("byproduct/{productId}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<SalesOrderLineItemDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByProduct(Guid productId)
    {
        _logger.LogInformation("Getting Sales Order Line Items for Product ID: {ProductId}", productId);

        var query = new GetSalesOrderLineItemsByProductQuery { ProductId = productId };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets sales order line items by service ID.
    /// </summary>
    /// <param name="serviceId">The service ID to search for.</param>
    /// <returns>A list of sales order line items for the specified service.</returns>
    [HttpGet("byservice/{serviceId}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<SalesOrderLineItemDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByService(Guid serviceId)
    {
        _logger.LogInformation("Getting Sales Order Line Items for Service ID: {ServiceId}", serviceId);

        var query = new GetSalesOrderLineItemsByServiceQuery { ServiceId = serviceId };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets the total amount for a sales order.
    /// </summary>
    /// <param name="salesOrderId">The ID of the sales order to calculate the total for.</param>
    /// <returns>The total amount for the sales order.</returns>
    [HttpGet("total/{salesOrderId}")]
    [ProducesResponseType(typeof(ApiResponse<decimal>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTotalForSalesOrder(Guid salesOrderId)
    {
        _logger.LogInformation("Calculating total for Sales Order ID: {SalesOrderId}", salesOrderId);

        var query = new GetTotalForSalesOrderQuery { SalesOrderId = salesOrderId };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }
}