using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.ProductType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.ProductType.Commands.CreateProductType
{
    /// <summary>
    /// Handler for the CreateProductTypeCommand.
    /// Creates a new product type in the database.
    /// </summary>
    public class CreateProductTypeCommandHandler : IRequestHandler<CreateProductTypeCommand, ProductTypeDto>
    {
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProductTypeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateProductTypeCommandHandler"/> class.
        /// </summary>
        /// <param name="productTypeRepository">The product type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public CreateProductTypeCommandHandler(
            IProductTypeRepository productTypeRepository,
            IMapper mapper,
            ILogger<CreateProductTypeCommandHandler> logger)
        {
            _productTypeRepository = productTypeRepository ?? throw new ArgumentNullException(nameof(productTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreateProductTypeCommand by creating a new product type.
        /// </summary>
        /// <param name="request">The command containing the product type details to create.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The newly created product type DTO.</returns>
        /// <exception cref="Exception">Thrown when an error occurs during the creation process.</exception>
        public async Task<ProductTypeDto> Handle(CreateProductTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Creating new product type with type: {ProductType}", request.Type);

                // Map command to entity
                var productTypeEntity = _mapper.Map<Domain.Entities.TypeStatusEntities.ProductType>(request);

                // Set audit fields
                productTypeEntity.Id = Guid.NewGuid();
                productTypeEntity.CreatedDate = DateTime.UtcNow;
                productTypeEntity.CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000000"); // This should be replaced with the actual user ID
                productTypeEntity.ModifiedDate = DateTime.UtcNow;
                productTypeEntity.ModifiedBy = Guid.Parse("00000000-0000-0000-0000-000000000000"); // This should be replaced with the actual user ID
                productTypeEntity.Active = true;

                // Add to repository
                var createdProductType = await _productTypeRepository.AddAsync(productTypeEntity, cancellationToken);

                // Map to DTO
                var productTypeDto = _mapper.Map<ProductTypeDto>(createdProductType);

                _logger.LogInformation("Successfully created product type with ID: {ProductTypeId}", productTypeDto.Id);

                return productTypeDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product type: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
