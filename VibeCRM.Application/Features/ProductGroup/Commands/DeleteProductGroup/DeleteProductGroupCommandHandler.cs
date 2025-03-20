using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.ProductGroup.Commands.DeleteProductGroup
{
    /// <summary>
    /// Handler for processing DeleteProductGroupCommand requests.
    /// Implements the CQRS command handler pattern for soft-deleting product group entities.
    /// </summary>
    public class DeleteProductGroupCommandHandler : IRequestHandler<DeleteProductGroupCommand, bool>
    {
        private readonly IProductGroupRepository _productGroupRepository;
        private readonly ILogger<DeleteProductGroupCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteProductGroupCommandHandler"/> class.
        /// </summary>
        /// <param name="productGroupRepository">The product group repository for database operations.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public DeleteProductGroupCommandHandler(
            IProductGroupRepository productGroupRepository,
            ILogger<DeleteProductGroupCommandHandler> logger)
        {
            _productGroupRepository = productGroupRepository ?? throw new ArgumentNullException(nameof(productGroupRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeleteProductGroupCommand by soft-deleting an existing product group entity in the database.
        /// </summary>
        /// <param name="request">The DeleteProductGroupCommand containing the product group ID to delete.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the product group to delete is not found.</exception>
        public async Task<bool> Handle(DeleteProductGroupCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            // Retrieve the existing product group
            var existingProductGroup = await _productGroupRepository.GetByIdAsync(request.Id, cancellationToken);
            if (existingProductGroup == null || !existingProductGroup.Active)
            {
                _logger.LogError("Product group not found for deletion with ID: {ProductGroupId}", request.Id);
                throw new InvalidOperationException($"Product group with ID {request.Id} not found or already deleted.");
            }

            try
            {
                // Update the modified by information before deletion
                existingProductGroup.ModifiedBy = request.ModifiedBy;
                existingProductGroup.ModifiedDate = DateTime.UtcNow;

                // Perform the soft delete operation
                await _productGroupRepository.DeleteAsync(existingProductGroup.ProductGroupId, cancellationToken);
                _logger.LogInformation("Soft deleted product group with ID: {ProductGroupId}", request.Id);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting product group with ID: {ProductGroupId}", request.Id);
                throw;
            }
        }
    }
}