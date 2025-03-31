using MediatR;
using Microsoft.AspNetCore.Mvc;
using VibeCRM.Application.Features.Company.Commands.CreateCompany;
using VibeCRM.Application.Features.Company.Commands.DeleteCompany;
using VibeCRM.Application.Features.Company.Commands.UpdateCompany;
using VibeCRM.Application.Features.Company.Queries.GetAllCompanies;
using VibeCRM.Application.Features.Company.Queries.GetCompanyById;
using VibeCRM.Shared.DTOs.Company;
using VibeCRM.Shared.Models;

namespace VibeCRM.Api.Controllers;

/// <summary>
/// API endpoints for managing company resources.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CompanyController : ApiControllerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CompanyController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for sending commands and queries.</param>
    /// <param name="logger">The logger instance for logging controller actions.</param>
    public CompanyController(IMediator mediator, ILogger<CompanyController> logger)
        : base(mediator, logger)
    {
    }

    /// <summary>
    /// Creates a new company.
    /// </summary>
    /// <param name="command">The company creation details.</param>
    /// <returns>The newly created company.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<CompanyDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateCompanyCommand command)
    {
        _logger.LogInformation("Creating new Company with Name: {Name}", command.Name);

        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, Success(result, "Company created successfully"));
    }

    /// <summary>
    /// Updates an existing company.
    /// </summary>
    /// <param name="id">The ID of the company to update.</param>
    /// <param name="command">The updated company details.</param>
    /// <returns>The updated company.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<CompanyDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCompanyCommand command)
    {
        if (id != command.Id)
        {
            return BadRequestResponse<CompanyDto>("ID in the URL does not match the ID in the request body");
        }

        _logger.LogInformation("Updating Company with ID: {Id}", id);

        var result = await _mediator.Send(command);

        return Ok(Success(result, "Company updated successfully"));
    }

    /// <summary>
    /// Deletes a company by its ID.
    /// </summary>
    /// <param name="id">The ID of the company to delete.</param>
    /// <returns>A success response if deletion was successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting Company with ID: {Id}", id);

        var command = new DeleteCompanyCommand
        {
            Id = id,
            ModifiedBy = Guid.Parse(User.Identity?.Name ?? Guid.Empty.ToString())
        };
        var result = await _mediator.Send(command);

        return Ok(Success(result, "Company deleted successfully"));
    }

    /// <summary>
    /// Gets a company by its ID.
    /// </summary>
    /// <param name="id">The ID of the company to retrieve.</param>
    /// <returns>The company details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<CompanyDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Getting Company with ID: {Id}", id);

        var query = new GetCompanyByIdQuery { Id = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<CompanyDto>($"Company with ID {id} not found");
        }

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a list of all companies.
    /// </summary>
    /// <returns>A list of all companies.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<CompanyDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all Companies");

        var query = new GetAllCompaniesQuery();
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }
}