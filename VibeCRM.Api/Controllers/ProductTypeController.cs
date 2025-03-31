using MediatR;
using Microsoft.AspNetCore.Mvc;
using VibeCRM.Application.Features.ProductType.Commands.CreateProductType;
using VibeCRM.Application.Features.ProductType.Commands.DeleteProductType;
using VibeCRM.Application.Features.ProductType.Commands.UpdateProductType;
using VibeCRM.Application.Features.ProductType.Queries.GetAllProductTypes;
using VibeCRM.Application.Features.ProductType.Queries.GetDefaultProductType;
using VibeCRM.Application.Features.ProductType.Queries.GetProductTypeById;
using VibeCRM.Application.Features.ProductType.Queries.GetProductTypeByOrdinalPosition;
using VibeCRM.Application.Features.ProductType.Queries.GetProductTypeByType;
using VibeCRM.Shared.DTOs.ProductType;
using VibeCRM.Shared.Models;

namespace VibeCRM.Api.Controllers;

/// <summary>
/// API endpoints for managing product type reference data.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProductTypeController : ApiControllerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProductTypeController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for sending commands and queries.</param>
    /// <param name="logger">The logger instance for logging controller actions.</param>
    public ProductTypeController(IMediator mediator, ILogger<ProductTypeController> logger)
        : base(mediator, logger)
    {
    }

    /// <summary>
    /// Creates a new product type.
    /// </summary>
    /// <param name="command">The product type creation details.</param>
    /// <returns>The newly created product type.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<ProductTypeDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateProductTypeCommand command)
    {
        _logger.LogInformation("Creating new Product Type with Type: {Type}", command.Type);

        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, Success(result, "Product Type created successfully"));
    }

    /// <summary>
    /// Updates an existing product type.
    /// </summary>
    /// <param name="id">The ID of the product type to update.</param>
    /// <param name="command">The updated product type details.</param>
    /// <returns>The updated product type.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<ProductTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductTypeCommand command)
    {
        if (id != command.Id)
        {
            return BadRequestResponse<ProductTypeDto>("ID in the URL does not match the ID in the request body");
        }

        _logger.LogInformation("Updating Product Type with ID: {Id}", id);

        var result = await _mediator.Send(command);

        return Ok(Success(result, "Product Type updated successfully"));
    }

    /// <summary>
    /// Deletes a product type by its ID.
    /// </summary>
    /// <param name="id">The ID of the product type to delete.</param>
    /// <returns>A success response if deletion was successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting Product Type with ID: {Id}", id);

        var command = new DeleteProductTypeCommand { Id = id };
        var result = await _mediator.Send(command);

        return Ok(Success(result, "Product Type deleted successfully"));
    }

    /// <summary>
    /// Gets a product type by its ID.
    /// </summary>
    /// <param name="id">The ID of the product type to retrieve.</param>
    /// <returns>The product type details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<ProductTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Getting Product Type with ID: {Id}", id);

        var query = new GetProductTypeByIdQuery { Id = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<ProductTypeDto>($"Product Type with ID {id} not found");
        }

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a list of all product types.
    /// </summary>
    /// <returns>A list of all product types.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ProductTypeDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all Product Types");

        var query = new GetAllProductTypesQuery();
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets product types by type name.
    /// </summary>
    /// <param name="type">The type name to search for.</param>
    /// <returns>A list of product types matching the specified type name.</returns>
    [HttpGet("bytype/{type}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ProductTypeDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByType(string type)
    {
        _logger.LogInformation("Getting Product Types with Type: {Type}", type);

        var query = new GetProductTypeByTypeQuery { Type = type };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets product types by ordinal position.
    /// </summary>
    /// <param name="position">The ordinal position to search for.</param>
    /// <returns>A list of product types with the specified ordinal position.</returns>
    [HttpGet("byposition/{position:int}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ProductTypeDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByOrdinalPosition(int position)
    {
        _logger.LogInformation("Getting Product Types with Ordinal Position: {Position}", position);

        var query = new GetProductTypeByOrdinalPositionQuery { OrdinalPosition = position };
        var result = await _mediator.Send(query);

        return Ok(Success(result));
    }

    /// <summary>
    /// Gets the default product type.
    /// </summary>
    /// <returns>The default product type.</returns>
    [HttpGet("default")]
    [ProducesResponseType(typeof(ApiResponse<ProductTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDefault()
    {
        _logger.LogInformation("Getting default Product Type");

        var query = new GetDefaultProductTypeQuery();
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFoundResponse<ProductTypeDto>("Default Product Type not found");
        }

        return Ok(Success(result));
    }
}