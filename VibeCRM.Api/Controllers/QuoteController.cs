using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Common.Models;
using VibeCRM.Application.Features.Quote.Commands.CreateQuote;
using VibeCRM.Application.Features.Quote.Commands.DeleteQuote;
using VibeCRM.Application.Features.Quote.Commands.UpdateQuote;
using VibeCRM.Application.Features.Quote.DTOs;
using VibeCRM.Application.Features.Quote.Queries.GetAllQuotes;
using VibeCRM.Application.Features.Quote.Queries.GetQuoteById;
using VibeCRM.Application.Features.Quote.Queries.GetQuotesByActivity;
using VibeCRM.Application.Features.Quote.Queries.GetQuotesByCompany;
using VibeCRM.Application.Features.Quote.Queries.GetQuotesByNumber;

namespace VibeCRM.Api.Controllers;

/// <summary>
/// API endpoints for managing quote resources.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class QuoteController : ApiControllerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="QuoteController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for sending commands and queries.</param>
    /// <param name="logger">The logger instance for logging controller actions.</param>
    public QuoteController(IMediator mediator, ILogger<QuoteController> logger)
        : base(mediator, logger)
    {
    }

    /// <summary>
    /// Creates a new quote.
    /// </summary>
    /// <param name="command">The quote creation details.</param>
    /// <returns>The newly created quote.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<QuoteDetailsDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateQuoteCommand command)
    {
        _logger.LogInformation("Creating new Quote with Number: {Number}", command.Number);
        
        var result = await _mediator.Send(command);
        
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, Success(result, "Quote created successfully"));
    }

    /// <summary>
    /// Updates an existing quote.
    /// </summary>
    /// <param name="id">The ID of the quote to update.</param>
    /// <param name="command">The updated quote details.</param>
    /// <returns>The updated quote.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<QuoteDetailsDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateQuoteCommand command)
    {
        if (id != command.Id)
        {
            return BadRequestResponse<QuoteDetailsDto>("ID in the URL does not match the ID in the request body");
        }

        _logger.LogInformation("Updating Quote with ID: {Id}", id);
        
        var result = await _mediator.Send(command);
        
        return Ok(Success(result, "Quote updated successfully"));
    }

    /// <summary>
    /// Deletes a quote by its ID.
    /// </summary>
    /// <param name="id">The ID of the quote to delete.</param>
    /// <returns>A success response if deletion was successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting Quote with ID: {Id}", id);
        
        var command = new DeleteQuoteCommand 
        { 
            Id = id,
            ModifiedBy = Guid.Parse(User.Identity?.Name ?? Guid.Empty.ToString())
        };
        var result = await _mediator.Send(command);
        
        return Ok(Success(result, "Quote deleted successfully"));
    }

    /// <summary>
    /// Gets a quote by its ID.
    /// </summary>
    /// <param name="id">The ID of the quote to retrieve.</param>
    /// <returns>The quote details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<QuoteDetailsDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Getting Quote with ID: {Id}", id);
        
        var query = new GetQuoteByIdQuery { Id = id };
        var result = await _mediator.Send(query);
        
        if (result == null)
        {
            return NotFoundResponse<QuoteDetailsDto>($"Quote with ID {id} not found");
        }
        
        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a list of all quotes.
    /// </summary>
    /// <returns>A list of all quotes.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<QuoteListDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all Quotes");
        
        var query = new GetAllQuotesQuery();
        var result = await _mediator.Send(query);
        
        return Ok(Success(result));
    }

    /// <summary>
    /// Gets quotes by company ID.
    /// </summary>
    /// <param name="companyId">The ID of the company to filter by.</param>
    /// <returns>A list of quotes for the specified company.</returns>
    [HttpGet("bycompany/{companyId}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<QuoteListDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByCompany(Guid companyId)
    {
        _logger.LogInformation("Getting Quotes for Company ID: {CompanyId}", companyId);
        
        var query = new GetQuotesByCompanyQuery { CompanyId = companyId };
        var result = await _mediator.Send(query);
        
        return Ok(Success(result));
    }

    /// <summary>
    /// Gets quotes by activity ID.
    /// </summary>
    /// <param name="activityId">The ID of the activity to filter by.</param>
    /// <returns>A list of quotes for the specified activity.</returns>
    [HttpGet("byactivity/{activityId}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<QuoteListDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByActivity(Guid activityId)
    {
        _logger.LogInformation("Getting Quotes for Activity ID: {ActivityId}", activityId);
        
        var query = new GetQuotesByActivityQuery { ActivityId = activityId };
        var result = await _mediator.Send(query);
        
        return Ok(Success(result));
    }

    /// <summary>
    /// Gets quotes by quote number.
    /// </summary>
    /// <param name="number">The quote number to search for.</param>
    /// <returns>A list of quotes matching the specified number.</returns>
    [HttpGet("bynumber/{number}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<QuoteListDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByNumber(string number)
    {
        _logger.LogInformation("Getting Quotes with Number: {Number}", number);
        
        var query = new GetQuotesByNumberQuery { Number = number };
        var result = await _mediator.Send(query);
        
        return Ok(Success(result));
    }
}
