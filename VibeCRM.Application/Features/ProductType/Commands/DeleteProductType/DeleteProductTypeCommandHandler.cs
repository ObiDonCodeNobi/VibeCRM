using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.ProductType.Commands.DeleteProductType
{
    /// <summary>
    /// Handler for the DeleteProductTypeCommand.
    /// Soft-deletes an existing product type in the database by setting Active = false.
    /// </summary>
    public class DeleteProductTypeCommandHandler : IRequestHandler<DeleteProductTypeCommand, bool>
    {
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly ILogger<DeleteProductTypeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteProductTypeCommandHandler"/> class.
        /// </summary>
        /// <param name="productTypeRepository">The product type repository.</param>
        /// <param name="logger">The logger.</param>
        public DeleteProductTypeCommandHandler(
            IProductTypeRepository productTypeRepository,
            ILogger<DeleteProductTypeCommandHandler> logger)
        {
            _productTypeRepository = productTypeRepository ?? throw new ArgumentNullException(nameof(productTypeRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeleteProductTypeCommand by soft-deleting an existing product type.
        /// </summary>
        /// <param name="request">The command containing the ID of the product type to delete.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if the deletion was successful, otherwise false.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the product type with the specified ID is not found.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the deletion process.</exception>
        public async Task<bool> Handle(DeleteProductTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Soft-deleting product type with ID: {ProductTypeId}", request.Id);

                // Get existing product type
                var existingProductType = await _productTypeRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingProductType == null)
                {
                    _logger.LogError("Product type with ID: {ProductTypeId} not found", request.Id);
                    throw new KeyNotFoundException($"Product type with ID: {request.Id} not found");
                }

                // Soft delete by setting Active = false
                existingProductType.Active = false;
                existingProductType.ModifiedDate = DateTime.UtcNow;
                existingProductType.ModifiedBy = Guid.Parse("00000000-0000-0000-0000-000000000000"); // This should be replaced with the actual user ID

                // Update in repository
                await _productTypeRepository.UpdateAsync(existingProductType, cancellationToken);

                _logger.LogInformation("Successfully soft-deleted product type with ID: {ProductTypeId}", request.Id);

                return true;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error soft-deleting product type: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
