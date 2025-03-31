using MediatR;
using Microsoft.AspNetCore.Mvc;
using VibeCRM.Application.Features.ProductGroup.Commands.CreateProductGroup;
using VibeCRM.Application.Features.ProductGroup.Commands.DeleteProductGroup;
using VibeCRM.Application.Features.ProductGroup.Commands.UpdateProductGroup;
using VibeCRM.Application.Features.ProductGroup.Queries.GetAllProductGroups;
using VibeCRM.Application.Features.ProductGroup.Queries.GetProductGroupById;
using VibeCRM.Application.Features.ProductGroup.Queries.GetProductGroupsByParentId;
using VibeCRM.Application.Features.ProductGroup.Queries.GetRootProductGroups;
using VibeCRM.Shared.DTOs.ProductGroup;
using VibeCRM.Shared.Models;

namespace VibeCRM.Api.Controllers;

/// <summary>
/// API endpoints for managing product group hierarchies.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProductGroupController : ApiControllerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProductGroupController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for sending commands and queries.</param>
    /// <param name="logger">The logger instance for logging controller actions.</param>
    public ProductGroupController(IMediator mediator, ILogger<ProductGroupController> logger)
        : base(mediator, logger)
    {
    }

    /// <summary>
    /// Creates a new product group.
    /// </summary>
    /// <param name="command">The product group creation details.</param>
    /// <returns>The newly created product group.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<ProductGroupDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateProductGroupCommand command)
    {
        _logger.LogInformation("Creating new Product Group with Name: {Name}", command.Name);

        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, Success(result, "Product Group created successfully"));
    }

    /// <summary>
    /// Updates an existing product group.
    /// </summary>
    /// <param name="id">The ID of the product group to update.</param>
    /// <param name="command">The updated product group details.</param>
    /// <returns>The updated product group.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<ProductGroupDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductGroupCommand command)
    {
        if (id != command.Id)
        {
            return BadRequestResponse<ProductGroupDto>("ID in the URL does not match the ID in the request body");
        }

        _logger.LogInformation("Updating Product Group with ID: {Id}", id);

        var result = await _mediator.Send(command);

        return Ok(Success(result, "Product Group updated successfully"));
    }

    /// <summary>
    /// Deletes a product group by its ID.
    /// </summary>
    /// <param name="id">The ID of the product group to delete.</param>
    /// <returns>A success response if deletion was successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting Product Group with ID: {Id}", id);

        var command = new DeleteProductGroupCommand { Id = id };
        var result = await _mediator.Send(command);

        return Ok(Success(result, "Product Group deleted successfully"));
    }

    /// <summary>
    /// Gets a product group by its ID.
    /// </summary>
    /// <param name="id">The ID of the product group to retrieve.</param>
    /// <returns>The product group details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<ProductGroupDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Getting Product Group with ID: {Id}", id);

        var query = new GetProductGroupByIdQuery { Id = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<ProductGroupDto>($"Product Group with ID {id} not found");
        }

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a list of all product groups.
    /// </summary>
    /// <returns>A list of all product groups.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ProductGroupDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all Product Groups");

        var query = new GetAllProductGroupsQuery();
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets product groups by parent ID.
    /// </summary>
    /// <param name="parentId">The parent ID to search for.</param>
    /// <returns>A list of product groups with the specified parent ID.</returns>
    [HttpGet("byparent/{parentId}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ProductGroupDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByParentId(Guid parentId)
    {
        _logger.LogInformation("Getting Product Groups with Parent ID: {ParentId}", parentId);

        var query = new GetProductGroupsByParentIdQuery { ParentId = parentId };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets root product groups (those with no parent).
    /// </summary>
    /// <returns>A list of root product groups.</returns>
    [HttpGet("root")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ProductGroupDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRoot()
    {
        _logger.LogInformation("Getting root Product Groups");

        var query = new GetRootProductGroupsQuery();
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }
}