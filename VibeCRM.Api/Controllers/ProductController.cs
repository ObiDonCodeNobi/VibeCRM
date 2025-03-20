using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Common.Models;
using VibeCRM.Application.Features.Product.Commands.CreateProduct;
using VibeCRM.Application.Features.Product.Commands.DeleteProduct;
using VibeCRM.Application.Features.Product.Commands.UpdateProduct;
using VibeCRM.Application.Features.Product.DTOs;
using VibeCRM.Application.Features.Product.Queries.GetAllProducts;
using VibeCRM.Application.Features.Product.Queries.GetProductById;
using VibeCRM.Application.Features.Product.Queries.GetProductsByProductGroup;
using VibeCRM.Application.Features.Product.Queries.GetProductsByProductType;

namespace VibeCRM.Api.Controllers;

/// <summary>
/// API endpoints for managing product resources.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProductController : ApiControllerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProductController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for sending commands and queries.</param>
    /// <param name="logger">The logger instance for logging controller actions.</param>
    public ProductController(IMediator mediator, ILogger<ProductController> logger)
        : base(mediator, logger)
    {
    }

    /// <summary>
    /// Creates a new product.
    /// </summary>
    /// <param name="command">The product creation details.</param>
    /// <returns>The newly created product.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<ProductDetailsDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
    {
        _logger.LogInformation("Creating new Product with Name: {Name}", command.Name);
        
        var result = await _mediator.Send(command);
        
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, Success(result, "Product created successfully"));
    }

    /// <summary>
    /// Updates an existing product.
    /// </summary>
    /// <param name="id">The ID of the product to update.</param>
    /// <param name="command">The updated product details.</param>
    /// <returns>The updated product.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<ProductDetailsDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductCommand command)
    {
        if (id != command.ProductId)
        {
            return BadRequestResponse<ProductDetailsDto>("ID in the URL does not match the ID in the request body");
        }

        _logger.LogInformation("Updating Product with ID: {Id}", id);
        
        var result = await _mediator.Send(command);
        
        return Ok(Success(result, "Product updated successfully"));
    }

    /// <summary>
    /// Deletes a product by its ID.
    /// </summary>
    /// <param name="id">The ID of the product to delete.</param>
    /// <returns>A success response if deletion was successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting Product with ID: {Id}", id);
        
        var command = new DeleteProductCommand 
        { 
            ProductId = id,
            DeletedBy = User.Identity?.Name ?? string.Empty
        };
        var result = await _mediator.Send(command);
        
        return Ok(Success(result, "Product deleted successfully"));
    }

    /// <summary>
    /// Gets a product by its ID.
    /// </summary>
    /// <param name="id">The ID of the product to retrieve.</param>
    /// <returns>The product details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<ProductDetailsDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Getting Product with ID: {Id}", id);
        
        var query = new GetProductByIdQuery { ProductId = id };
        var result = await _mediator.Send(query);
        
        if (result == null)
        {
            return NotFoundResponse<ProductDetailsDto>($"Product with ID {id} not found");
        }
        
        return Ok(Success(result));
    }

    /// <summary>
    /// Gets a list of all products.
    /// </summary>
    /// <returns>A list of all products.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ProductListDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all Products");
        
        var query = new GetAllProductsQuery();
        var result = await _mediator.Send(query);
        
        return Ok(Success(result));
    }

    /// <summary>
    /// Gets products by product type ID.
    /// </summary>
    /// <param name="productTypeId">The ID of the product type to filter by.</param>
    /// <returns>A list of products of the specified type.</returns>
    [HttpGet("bytype/{productTypeId}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ProductListDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByProductType(Guid productTypeId)
    {
        _logger.LogInformation("Getting Products by Product Type ID: {ProductTypeId}", productTypeId);
        
        var query = new GetProductsByProductTypeQuery { ProductTypeId = productTypeId };
        var result = await _mediator.Send(query);
        
        return Ok(Success(result));
    }

    /// <summary>
    /// Gets products by product group ID.
    /// </summary>
    /// <param name="productGroupId">The ID of the product group to filter by.</param>
    /// <returns>A list of products in the specified group.</returns>
    [HttpGet("bygroup/{productGroupId}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ProductListDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByProductGroup(Guid productGroupId)
    {
        _logger.LogInformation("Getting Products by Product Group ID: {ProductGroupId}", productGroupId);
        
        var query = new GetProductsByProductGroupQuery { ProductGroupId = productGroupId };
        var result = await _mediator.Send(query);
        
        return Ok(Success(result));
    }
}
