using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.ProductType;

namespace VibeCRM.Application.Features.ProductType.Commands.UpdateProductType
{
    /// <summary>
    /// Handler for the UpdateProductTypeCommand.
    /// Updates an existing product type in the database.
    /// </summary>
    public class UpdateProductTypeCommandHandler : IRequestHandler<UpdateProductTypeCommand, ProductTypeDto>
    {
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProductTypeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateProductTypeCommandHandler"/> class.
        /// </summary>
        /// <param name="productTypeRepository">The product type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public UpdateProductTypeCommandHandler(
            IProductTypeRepository productTypeRepository,
            IMapper mapper,
            ILogger<UpdateProductTypeCommandHandler> logger)
        {
            _productTypeRepository = productTypeRepository ?? throw new ArgumentNullException(nameof(productTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdateProductTypeCommand by updating an existing product type.
        /// </summary>
        /// <param name="request">The command containing the product type details to update.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The updated product type DTO.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the product type with the specified ID is not found.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the update process.</exception>
        public async Task<ProductTypeDto> Handle(UpdateProductTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Updating product type with ID: {ProductTypeId}", request.Id);

                // Get existing product type
                var existingProductType = await _productTypeRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingProductType == null)
                {
                    _logger.LogError("Product type with ID: {ProductTypeId} not found", request.Id);
                    throw new KeyNotFoundException($"Product type with ID: {request.Id} not found");
                }

                // Update properties
                existingProductType.Type = request.Type;
                existingProductType.Description = request.Description;
                existingProductType.OrdinalPosition = request.OrdinalPosition;
                existingProductType.ModifiedDate = DateTime.UtcNow;
                existingProductType.ModifiedBy = Guid.Parse("00000000-0000-0000-0000-000000000000"); // This should be replaced with the actual user ID

                // Update in repository
                var updatedProductType = await _productTypeRepository.UpdateAsync(existingProductType, cancellationToken);

                // Map to DTO
                var productTypeDto = _mapper.Map<ProductTypeDto>(updatedProductType);

                _logger.LogInformation("Successfully updated product type with ID: {ProductTypeId}", productTypeDto.Id);

                return productTypeDto;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product type: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}