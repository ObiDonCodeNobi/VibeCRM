using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.Product;

namespace VibeCRM.Application.Features.Product.Commands.UpdateProduct
{
    /// <summary>
    /// Handler for processing UpdateProductCommand requests
    /// </summary>
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDetailsDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProductCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateProductCommandHandler"/> class.
        /// </summary>
        /// <param name="productRepository">The product repository for data access</param>
        /// <param name="mapper">The AutoMapper instance for object mapping</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public UpdateProductCommandHandler(
            IProductRepository productRepository,
            IMapper mapper,
            ILogger<UpdateProductCommandHandler> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdateProductCommand request
        /// </summary>
        /// <param name="request">The request containing the product information to update</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated product details DTO</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null</exception>
        /// <exception cref="ArgumentException">Thrown when required fields are missing</exception>
        /// <exception cref="InvalidOperationException">Thrown when the product to update is not found</exception>
        public async Task<ProductDetailsDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.ProductId == Guid.Empty)
            {
                throw new ArgumentException("Product ID is required", nameof(request.ProductId));
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ArgumentException("Product name is required", nameof(request.Name));
            }

            if (request.ProductTypeId == Guid.Empty)
            {
                throw new ArgumentException("Product type ID is required", nameof(request.ProductTypeId));
            }

            _logger.LogInformation("Updating product with ID: {ProductId}", request.ProductId);

            // Get the existing product
            var existingProduct = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);
            if (existingProduct == null)
            {
                _logger.LogWarning("Product with ID {ProductId} not found for update", request.ProductId);
                throw new InvalidOperationException($"Product with ID {request.ProductId} not found");
            }

            // Update the product properties
            existingProduct.ProductTypeId = request.ProductTypeId;
            existingProduct.Name = request.Name;
            existingProduct.Description = request.Description;
            existingProduct.ModifiedBy = Guid.Parse(request.ModifiedBy);
            existingProduct.ModifiedDate = DateTime.UtcNow;

            // Update the product in the repository
            var updatedProduct = await _productRepository.UpdateAsync(existingProduct, cancellationToken);

            // Load related entities
            await _productRepository.LoadProductTypeAsync(updatedProduct, cancellationToken);
            await _productRepository.LoadQuoteLineItemsAsync(updatedProduct, cancellationToken);
            await _productRepository.LoadSalesOrderLineItemsAsync(updatedProduct, cancellationToken);
            await _productRepository.LoadProductGroupsAsync(updatedProduct, cancellationToken);

            _logger.LogInformation("Successfully updated product with ID: {ProductId}", updatedProduct.ProductId);

            // Map the product entity to the DTO
            return _mapper.Map<ProductDetailsDto>(updatedProduct);
        }
    }
}