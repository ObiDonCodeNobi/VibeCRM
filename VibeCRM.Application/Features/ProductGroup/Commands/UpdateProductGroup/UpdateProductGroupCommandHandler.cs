using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.ProductGroup.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.ProductGroup.Commands.UpdateProductGroup
{
    /// <summary>
    /// Handler for processing UpdateProductGroupCommand requests.
    /// Implements the CQRS command handler pattern for updating existing product group entities.
    /// </summary>
    public class UpdateProductGroupCommandHandler : IRequestHandler<UpdateProductGroupCommand, ProductGroupDetailsDto>
    {
        private readonly IProductGroupRepository _productGroupRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProductGroupCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateProductGroupCommandHandler"/> class.
        /// </summary>
        /// <param name="productGroupRepository">The product group repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public UpdateProductGroupCommandHandler(
            IProductGroupRepository productGroupRepository,
            IMapper mapper,
            ILogger<UpdateProductGroupCommandHandler> logger)
        {
            _productGroupRepository = productGroupRepository ?? throw new ArgumentNullException(nameof(productGroupRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdateProductGroupCommand by updating an existing product group entity in the database.
        /// </summary>
        /// <param name="request">The UpdateProductGroupCommand containing the updated product group details.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A ProductGroupDetailsDto representing the updated product group.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the product group to update is not found.</exception>
        public async Task<ProductGroupDetailsDto> Handle(UpdateProductGroupCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            // Retrieve the existing product group
            var existingProductGroup = await _productGroupRepository.GetByIdAsync(request.Id, cancellationToken);
            if (existingProductGroup == null)
            {
                _logger.LogError("Product group not found for update with ID: {ProductGroupId}", request.Id);
                throw new InvalidOperationException($"Product group with ID {request.Id} not found.");
            }

            try
            {
                // Update the entity properties from the command
                existingProductGroup.Name = request.Name;
                existingProductGroup.Description = request.Description;
                existingProductGroup.ParentProductGroupId = request.ParentProductGroupId;
                existingProductGroup.DisplayOrder = request.DisplayOrder;
                existingProductGroup.ModifiedBy = request.ModifiedBy;
                existingProductGroup.ModifiedDate = DateTime.UtcNow;

                // Update the product group in the repository
                var updatedProductGroup = await _productGroupRepository.UpdateAsync(existingProductGroup, cancellationToken);
                _logger.LogInformation("Updated product group with ID: {ProductGroupId}", updatedProductGroup.ProductGroupId);

                // Return the mapped DTO
                return _mapper.Map<ProductGroupDetailsDto>(updatedProductGroup);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating product group with ID: {ProductGroupId}", request.Id);
                throw;
            }
        }
    }
}