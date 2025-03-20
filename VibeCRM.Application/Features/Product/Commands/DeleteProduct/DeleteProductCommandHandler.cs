using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Product.Commands.DeleteProduct
{
    /// <summary>
    /// Handler for processing DeleteProductCommand requests
    /// </summary>
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<DeleteProductCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteProductCommandHandler"/> class.
        /// </summary>
        /// <param name="productRepository">The product repository for data access</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public DeleteProductCommandHandler(
            IProductRepository productRepository,
            ILogger<DeleteProductCommandHandler> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeleteProductCommand request
        /// </summary>
        /// <param name="request">The request containing the product ID to delete</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the product was successfully deleted, otherwise false</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null</exception>
        /// <exception cref="ArgumentException">Thrown when the product ID is empty</exception>
        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.ProductId == Guid.Empty)
            {
                throw new ArgumentException("Product ID is required", nameof(request.ProductId));
            }

            _logger.LogInformation("Deleting product with ID: {ProductId}", request.ProductId);

            // Get the existing product
            var existingProduct = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);
            if (existingProduct == null)
            {
                _logger.LogWarning("Product with ID {ProductId} not found for deletion", request.ProductId);
                return false;
            }

            // Soft delete the product by setting Active to false
            existingProduct.Active = false;
            existingProduct.ModifiedBy = Guid.Parse(request.DeletedBy);
            existingProduct.ModifiedDate = DateTime.UtcNow;

            // Update the product in the repository
            await _productRepository.UpdateAsync(existingProduct, cancellationToken);

            _logger.LogInformation("Successfully deleted product with ID: {ProductId}", request.ProductId);

            return true;
        }
    }
}