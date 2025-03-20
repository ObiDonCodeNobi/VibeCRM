using MediatR;
using Microsoft.AspNetCore.Mvc;
using VibeCRM.Application.Common.Models;
using VibeCRM.Application.Features.AccountType.Commands.CreateAccountType;
using VibeCRM.Application.Features.AccountType.Commands.DeleteAccountType;
using VibeCRM.Application.Features.AccountType.Commands.UpdateAccountType;
using VibeCRM.Application.Features.AccountType.DTOs;
using VibeCRM.Application.Features.AccountType.Queries.GetAccountTypeByType;

namespace VibeCRM.Api.Controllers;

/// <summary>
/// API endpoints for managing account type reference data.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AccountTypeController : ApiControllerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AccountTypeController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for sending commands and queries.</param>
    /// <param name="logger">The logger instance for logging controller actions.</param>
    public AccountTypeController(IMediator mediator, ILogger<AccountTypeController> logger)
        : base(mediator, logger)
    {
    }

    /// <summary>
    /// Creates a new account type.
    /// </summary>
    /// <param name="command">The account type creation details.</param>
    /// <returns>The newly created account type.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<AccountTypeDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateAccountTypeCommand command)
    {
        _logger.LogInformation("Creating new Account Type with Type: {Type}", command.Type);

        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = result }, Success(result, "Account Type created successfully"));
    }

    /// <summary>
    /// Updates an existing account type.
    /// </summary>
    /// <param name="id">The ID of the account type to update.</param>
    /// <param name="command">The updated account type details.</param>
    /// <returns>The updated account type.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<AccountTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAccountTypeCommand command)
    {
        if (command.Id != id)
        {
            return BadRequestResponse<AccountTypeDto>("ID in the URL does not match the ID in the request body");
        }

        _logger.LogInformation("Updating Account Type with ID: {Id}", id);

        var result = await _mediator.Send(command);

        return Ok(Success(result, "Account Type updated successfully"));
    }

    /// <summary>
    /// Deletes an account type by its ID.
    /// </summary>
    /// <param name="id">The ID of the account type to delete.</param>
    /// <returns>A success response if deletion was successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting Account Type with ID: {Id}", id);

        var command = new DeleteAccountTypeCommand { Id = id };
        var result = await _mediator.Send(command);

        return Ok(Success(result, "Account Type deleted successfully"));
    }

    /// <summary>
    /// Gets an account type by its ID.
    /// </summary>
    /// <param name="id">The ID of the account type to retrieve.</param>
    /// <returns>The account type details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<AccountTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Getting Account Type with ID: {Id}", id);

        // Since there's no specific GetAccountTypeByIdQuery, we'll use the GetAccountTypeByTypeQuery
        // and filter the results in the controller
        var query = new GetAccountTypeByTypeQuery { Type = string.Empty };
        var results = await _mediator.Send(query);

        var result = results.FirstOrDefault(at => at.Id == id);

        if (result == null)
        {
            return NotFoundResponse<AccountTypeDto>($"Account Type with ID {id} not found");
        }

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a list of all account types.
    /// </summary>
    /// <returns>A list of all account types.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<AccountTypeDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all Account Types");

        // Since there's no specific GetAllAccountTypesQuery, we'll use the GetAccountTypeByTypeQuery
        // with an empty type string to get all account types
        var query = new GetAccountTypeByTypeQuery { Type = string.Empty };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets account types by type name.
    /// </summary>
    /// <param name="type">The type name to search for.</param>
    /// <returns>A list of account types matching the specified type name.</returns>
    [HttpGet("bytype/{type}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<AccountTypeDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByType(string type)
    {
        _logger.LogInformation("Getting Account Types with Type: {Type}", type);

        var query = new GetAccountTypeByTypeQuery { Type = type };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }
}