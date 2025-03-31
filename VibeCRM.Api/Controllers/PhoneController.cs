using MediatR;
using Microsoft.AspNetCore.Mvc;
using VibeCRM.Application.Features.Phone.Commands.AddPhoneToCompany;
using VibeCRM.Application.Features.Phone.Commands.AddPhoneToPerson;
using VibeCRM.Application.Features.Phone.Commands.CreatePhone;
using VibeCRM.Application.Features.Phone.Commands.DeletePhone;
using VibeCRM.Application.Features.Phone.Commands.RemovePhoneFromCompany;
using VibeCRM.Application.Features.Phone.Commands.RemovePhoneFromPerson;
using VibeCRM.Application.Features.Phone.Commands.UpdatePhone;
using VibeCRM.Application.Features.Phone.Queries.GetAllPhones;
using VibeCRM.Application.Features.Phone.Queries.GetPhoneById;
using VibeCRM.Application.Features.Phone.Queries.GetPhonesByCompany;
using VibeCRM.Application.Features.Phone.Queries.GetPhonesByPerson;
using VibeCRM.Application.Features.Phone.Queries.GetPhonesByPhoneType;
using VibeCRM.Application.Features.Phone.Queries.SearchPhonesByNumber;
using VibeCRM.Shared.DTOs.Phone;
using VibeCRM.Shared.Models;

namespace VibeCRM.Api.Controllers;

/// <summary>
/// API endpoints for managing phone numbers.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PhoneController : ApiControllerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PhoneController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for sending commands and queries.</param>
    /// <param name="logger">The logger instance for logging controller actions.</param>
    public PhoneController(IMediator mediator, ILogger<PhoneController> logger)
        : base(mediator, logger)
    {
    }

    /// <summary>
    /// Creates a new phone number.
    /// </summary>
    /// <param name="command">The phone number creation details.</param>
    /// <returns>The newly created phone number.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<PhoneDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreatePhoneCommand command)
    {
        _logger.LogInformation("Creating new Phone with Area Code: {AreaCode}, Prefix: {Prefix}, Line Number: {LineNumber}",
            command.AreaCode, command.Prefix, command.LineNumber);

        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, Success(result, "Phone number created successfully"));
    }

    /// <summary>
    /// Updates an existing phone number.
    /// </summary>
    /// <param name="id">The ID of the phone number to update.</param>
    /// <param name="command">The updated phone number details.</param>
    /// <returns>The updated phone number.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<PhoneDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePhoneCommand command)
    {
        if (id != command.Id)
        {
            return BadRequestResponse<PhoneDto>("ID in the URL does not match the ID in the request body");
        }

        _logger.LogInformation("Updating Phone with ID: {Id}", id);

        var result = await _mediator.Send(command);

        return Ok(Success(result, "Phone number updated successfully"));
    }

    /// <summary>
    /// Deletes a phone number by its ID.
    /// </summary>
    /// <param name="id">The ID of the phone number to delete.</param>
    /// <returns>A success response if deletion was successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting Phone with ID: {Id}", id);

        var command = new DeletePhoneCommand { Id = id };
        var result = await _mediator.Send(command);

        return Ok(Success(result, "Phone number deleted successfully"));
    }

    /// <summary>
    /// Gets a phone number by its ID.
    /// </summary>
    /// <param name="id">The ID of the phone number to retrieve.</param>
    /// <returns>The phone number details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<PhoneDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Getting Phone with ID: {Id}", id);

        var query = new GetPhoneByIdQuery { Id = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<PhoneDto>($"Phone with ID {id} not found");
        }

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a list of all phone numbers.
    /// </summary>
    /// <returns>A list of all phone numbers.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<PhoneDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all Phones");

        var query = new GetAllPhonesQuery();
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets phone numbers by company ID.
    /// </summary>
    /// <param name="companyId">The company ID to search for.</param>
    /// <returns>A list of phone numbers associated with the specified company.</returns>
    [HttpGet("bycompany/{companyId}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<PhoneDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByCompany(Guid companyId)
    {
        _logger.LogInformation("Getting Phones for Company with ID: {CompanyId}", companyId);

        var query = new GetPhonesByCompanyQuery { CompanyId = companyId };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets phone numbers by person ID.
    /// </summary>
    /// <param name="personId">The person ID to search for.</param>
    /// <returns>A list of phone numbers associated with the specified person.</returns>
    [HttpGet("byperson/{personId}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<PhoneDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByPerson(Guid personId)
    {
        _logger.LogInformation("Getting Phones for Person with ID: {PersonId}", personId);

        var query = new GetPhonesByPersonQuery { PersonId = personId };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets phone numbers by phone type ID.
    /// </summary>
    /// <param name="phoneTypeId">The phone type ID to search for.</param>
    /// <returns>A list of phone numbers with the specified phone type.</returns>
    [HttpGet("bytype/{phoneTypeId}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<PhoneDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByPhoneType(Guid phoneTypeId)
    {
        _logger.LogInformation("Getting Phones with Phone Type ID: {PhoneTypeId}", phoneTypeId);

        var query = new GetPhonesByPhoneTypeQuery { PhoneTypeId = phoneTypeId };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Searches for phone numbers by number.
    /// </summary>
    /// <param name="searchTerm">The search term to look for in phone numbers.</param>
    /// <returns>A list of phone numbers matching the search term.</returns>
    [HttpGet("search/{searchTerm}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<PhoneDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> SearchByNumber(string searchTerm)
    {
        _logger.LogInformation("Searching Phones with term: {SearchTerm}", searchTerm);

        var query = new SearchPhonesByNumberQuery { SearchTerm = searchTerm };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Adds a phone number to a company.
    /// </summary>
    /// <param name="command">The command containing the phone and company details.</param>
    /// <returns>The result of the operation.</returns>
    [HttpPost("addtocompany")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddToCompany([FromBody] AddPhoneToCompanyCommand command)
    {
        _logger.LogInformation("Adding Phone with ID: {PhoneId} to Company with ID: {CompanyId}",
            command.PhoneId, command.CompanyId);

        var result = await _mediator.Send(command);

        return Ok(Success(result, "Phone number added to company successfully"));
    }

    /// <summary>
    /// Removes a phone number from a company.
    /// </summary>
    /// <param name="command">The command containing the phone and company details.</param>
    /// <returns>The result of the operation.</returns>
    [HttpPost("removefromcompany")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveFromCompany([FromBody] RemovePhoneFromCompanyCommand command)
    {
        _logger.LogInformation("Removing Phone with ID: {PhoneId} from Company with ID: {CompanyId}",
            command.PhoneId, command.CompanyId);

        var result = await _mediator.Send(command);

        return Ok(Success(result, "Phone number removed from company successfully"));
    }

    /// <summary>
    /// Adds a phone number to a person.
    /// </summary>
    /// <param name="command">The command containing the phone and person details.</param>
    /// <returns>The result of the operation.</returns>
    [HttpPost("addtoperson")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddToPerson([FromBody] AddPhoneToPersonCommand command)
    {
        _logger.LogInformation("Adding Phone with ID: {PhoneId} to Person with ID: {PersonId}",
            command.PhoneId, command.PersonId);

        var result = await _mediator.Send(command);

        return Ok(Success(result, "Phone number added to person successfully"));
    }

    /// <summary>
    /// Removes a phone number from a person.
    /// </summary>
    /// <param name="command">The command containing the phone and person details.</param>
    /// <returns>The result of the operation.</returns>
    [HttpPost("removefromperson")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveFromPerson([FromBody] RemovePhoneFromPersonCommand command)
    {
        _logger.LogInformation("Removing Phone with ID: {PhoneId} from Person with ID: {PersonId}",
            command.PhoneId, command.PersonId);

        var result = await _mediator.Send(command);

        return Ok(Success(result, "Phone number removed from person successfully"));
    }
}