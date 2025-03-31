using MediatR;
using Microsoft.AspNetCore.Mvc;
using VibeCRM.Application.Features.AccountStatus.Commands.CreateAccountStatus;
using VibeCRM.Application.Features.AccountStatus.Commands.DeleteAccountStatus;
using VibeCRM.Application.Features.AccountStatus.Commands.UpdateAccountStatus;
using VibeCRM.Application.Features.AccountStatus.Queries.GetAccountStatusById;
using VibeCRM.Application.Features.AccountStatus.Queries.GetAccountStatusByOrdinalPosition;
using VibeCRM.Application.Features.AccountStatus.Queries.GetAccountStatusByStatus;
using VibeCRM.Application.Features.AccountStatus.Queries.GetAllAccountStatuses;
using VibeCRM.Shared.DTOs.AccountStatus;
using VibeCRM.Shared.Models;

namespace VibeCRM.Api.Controllers;

/// <summary>
/// API endpoints for managing account status reference data.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AccountStatusController : ApiControllerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AccountStatusController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for sending commands and queries.</param>
    /// <param name="logger">The logger instance for logging controller actions.</param>
    public AccountStatusController(IMediator mediator, ILogger<AccountStatusController> logger)
        : base(mediator, logger)
    {
    }

    /// <summary>
    /// Creates a new account status.
    /// </summary>
    /// <param name="command">The account status creation details.</param>
    /// <returns>The newly created account status.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<AccountStatusDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateAccountStatusCommand command)
    {
        _logger.LogInformation("Creating new Account Status with Status: {Status}", command.Status);

        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, Success(result, "Account Status created successfully"));
    }

    /// <summary>
    /// Updates an existing account status.
    /// </summary>
    /// <param name="id">The ID of the account status to update.</param>
    /// <param name="command">The updated account status details.</param>
    /// <returns>The updated account status.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<AccountStatusDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAccountStatusCommand command)
    {
        if (id != command.Id)
        {
            return BadRequestResponse<AccountStatusDto>("ID in the URL does not match the ID in the request body");
        }

        _logger.LogInformation("Updating Account Status with ID: {Id}", id);

        var result = await _mediator.Send(command);

        return Ok(Success(result, "Account Status updated successfully"));
    }

    /// <summary>
    /// Deletes an account status by its ID.
    /// </summary>
    /// <param name="id">The ID of the account status to delete.</param>
    /// <returns>A success response if deletion was successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting Account Status with ID: {Id}", id);

        var command = new DeleteAccountStatusCommand { Id = id };
        var result = await _mediator.Send(command);

        return Ok(Success(result, "Account Status deleted successfully"));
    }

    /// <summary>
    /// Gets an account status by its ID.
    /// </summary>
    /// <param name="id">The ID of the account status to retrieve.</param>
    /// <returns>The account status details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<AccountStatusDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Getting Account Status with ID: {Id}", id);

        var query = new GetAccountStatusByIdQuery { Id = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<AccountStatusDto>($"Account Status with ID {id} not found");
        }

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a list of all account statuses.
    /// </summary>
    /// <returns>A list of all account statuses.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<AccountStatusDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all Account Statuses");

        var query = new GetAllAccountStatusesQuery();
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets account statuses by status name.
    /// </summary>
    /// <param name="status">The status name to search for.</param>
    /// <returns>A list of account statuses matching the specified status name.</returns>
    [HttpGet("bystatus/{status}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<AccountStatusDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByStatus(string status)
    {
        _logger.LogInformation("Getting Account Statuses with Status: {Status}", status);

        var query = new GetAccountStatusByStatusQuery { Status = status };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets account statuses by ordinal position.
    /// </summary>
    /// <param name="position">The ordinal position to search for.</param>
    /// <returns>A list of account statuses with the specified ordinal position.</returns>
    [HttpGet("byposition/{position:int}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<AccountStatusDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByOrdinalPosition(int position)
    {
        _logger.LogInformation("Getting Account Statuses with Ordinal Position: {Position}", position);

        var query = new GetAccountStatusByOrdinalPositionQuery { OrdinalPosition = position };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }
}