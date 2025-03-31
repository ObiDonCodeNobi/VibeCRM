using MediatR;
using Microsoft.AspNetCore.Mvc;
using VibeCRM.Application.Features.QuoteLineItem.Commands.CreateQuoteLineItem;
using VibeCRM.Application.Features.QuoteLineItem.Commands.DeleteQuoteLineItem;
using VibeCRM.Application.Features.QuoteLineItem.Commands.UpdateQuoteLineItem;
using VibeCRM.Application.Features.QuoteLineItem.Queries.GetAllQuoteLineItems;
using VibeCRM.Application.Features.QuoteLineItem.Queries.GetQuoteLineItemById;
using VibeCRM.Application.Features.QuoteLineItem.Queries.GetQuoteLineItemsByDateRange;
using VibeCRM.Application.Features.QuoteLineItem.Queries.GetQuoteLineItemsByProduct;
using VibeCRM.Application.Features.QuoteLineItem.Queries.GetQuoteLineItemsByQuote;
using VibeCRM.Application.Features.QuoteLineItem.Queries.GetQuoteLineItemsByService;
using VibeCRM.Application.Features.QuoteLineItem.Queries.GetTotalForQuote;
using VibeCRM.Shared.DTOs.QuoteLineItem;
using VibeCRM.Shared.Models;

namespace VibeCRM.Api.Controllers;

/// <summary>
/// API endpoints for managing quote line items.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class QuoteLineItemController : ApiControllerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="QuoteLineItemController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for sending commands and queries.</param>
    /// <param name="logger">The logger instance for logging controller actions.</param>
    public QuoteLineItemController(IMediator mediator, ILogger<QuoteLineItemController> logger)
        : base(mediator, logger)
    {
    }

    /// <summary>
    /// Creates a new quote line item.
    /// </summary>
    /// <param name="command">The quote line item creation details.</param>
    /// <returns>The newly created quote line item.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<QuoteLineItemDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateQuoteLineItemCommand command)
    {
        _logger.LogInformation("Creating new Quote Line Item for Quote ID: {QuoteId}", command.QuoteId);

        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, Success(result, "Quote Line Item created successfully"));
    }

    /// <summary>
    /// Updates an existing quote line item.
    /// </summary>
    /// <param name="id">The ID of the quote line item to update.</param>
    /// <param name="command">The updated quote line item details.</param>
    /// <returns>The updated quote line item.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<QuoteLineItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateQuoteLineItemCommand command)
    {
        if (id != command.Id)
        {
            return BadRequestResponse<QuoteLineItemDto>("ID in the URL does not match the ID in the request body");
        }

        _logger.LogInformation("Updating Quote Line Item with ID: {Id}", id);

        var result = await _mediator.Send(command);

        return Ok(Success(result, "Quote Line Item updated successfully"));
    }

    /// <summary>
    /// Deletes a quote line item by its ID.
    /// </summary>
    /// <param name="id">The ID of the quote line item to delete.</param>
    /// <returns>A success response if deletion was successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting Quote Line Item with ID: {Id}", id);

        var command = new DeleteQuoteLineItemCommand { Id = id };
        var result = await _mediator.Send(command);

        return Ok(Success(result, "Quote Line Item deleted successfully"));
    }

    /// <summary>
    /// Gets a quote line item by its ID.
    /// </summary>
    /// <param name="id">The ID of the quote line item to retrieve.</param>
    /// <returns>The quote line item details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<QuoteLineItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Getting Quote Line Item with ID: {Id}", id);

        var query = new GetQuoteLineItemByIdQuery { Id = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<QuoteLineItemDto>($"Quote Line Item with ID {id} not found");
        }

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a list of all quote line items.
    /// </summary>
    /// <returns>A list of all quote line items.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<QuoteLineItemDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all Quote Line Items");

        var query = new GetAllQuoteLineItemsQuery();
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets quote line items by quote ID.
    /// </summary>
    /// <param name="quoteId">The quote ID to search for.</param>
    /// <returns>A list of quote line items for the specified quote.</returns>
    [HttpGet("byquote/{quoteId}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<QuoteLineItemDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByQuote(Guid quoteId)
    {
        _logger.LogInformation("Getting Quote Line Items for Quote ID: {QuoteId}", quoteId);

        var query = new GetQuoteLineItemsByQuoteQuery { QuoteId = quoteId };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets quote line items by product ID.
    /// </summary>
    /// <param name="productId">The product ID to search for.</param>
    /// <returns>A list of quote line items for the specified product.</returns>
    [HttpGet("byproduct/{productId}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<QuoteLineItemDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByProduct(Guid productId)
    {
        _logger.LogInformation("Getting Quote Line Items for Product ID: {ProductId}", productId);

        var query = new GetQuoteLineItemsByProductQuery { ProductId = productId };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets quote line items by service ID.
    /// </summary>
    /// <param name="serviceId">The service ID to search for.</param>
    /// <returns>A list of quote line items for the specified service.</returns>
    [HttpGet("byservice/{serviceId}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<QuoteLineItemDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByService(Guid serviceId)
    {
        _logger.LogInformation("Getting Quote Line Items for Service ID: {ServiceId}", serviceId);

        var query = new GetQuoteLineItemsByServiceQuery { ServiceId = serviceId };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets quote line items within a date range.
    /// </summary>
    /// <param name="startDate">The start date of the range.</param>
    /// <param name="endDate">The end date of the range.</param>
    /// <returns>A list of quote line items within the specified date range.</returns>
    [HttpGet("bydaterange")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<QuoteLineItemDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        if (startDate > endDate)
        {
            return BadRequestResponse<IEnumerable<QuoteLineItemDto>>("Start date must be before or equal to end date");
        }

        _logger.LogInformation("Getting Quote Line Items between {StartDate} and {EndDate}", startDate, endDate);

        var query = new GetQuoteLineItemsByDateRangeQuery { StartDate = startDate, EndDate = endDate };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets the total amount for a quote.
    /// </summary>
    /// <param name="quoteId">The ID of the quote to calculate the total for.</param>
    /// <returns>The total amount for the quote.</returns>
    [HttpGet("total/{quoteId}")]
    [ProducesResponseType(typeof(ApiResponse<decimal>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTotalForQuote(Guid quoteId)
    {
        _logger.LogInformation("Calculating total for Quote ID: {QuoteId}", quoteId);

        var query = new GetTotalForQuoteQuery { QuoteId = quoteId };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }
}