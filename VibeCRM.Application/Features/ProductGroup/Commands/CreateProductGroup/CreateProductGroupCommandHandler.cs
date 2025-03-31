using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.ProductGroup;

namespace VibeCRM.Application.Features.ProductGroup.Commands.CreateProductGroup
{
    /// <summary>
    /// Handler for processing CreateProductGroupCommand requests.
    /// Implements the CQRS command handler pattern for creating new product group entities.
    /// </summary>
    public class CreateProductGroupCommandHandler : IRequestHandler<CreateProductGroupCommand, ProductGroupDetailsDto>
    {
        private readonly IProductGroupRepository _productGroupRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProductGroupCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateProductGroupCommandHandler"/> class.
        /// </summary>
        /// <param name="productGroupRepository">The product group repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public CreateProductGroupCommandHandler(
            IProductGroupRepository productGroupRepository,
            IMapper mapper,
            ILogger<CreateProductGroupCommandHandler> logger)
        {
            _productGroupRepository = productGroupRepository ?? throw new ArgumentNullException(nameof(productGroupRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreateProductGroupCommand by creating a new product group entity in the database.
        /// </summary>
        /// <param name="request">The CreateProductGroupCommand containing the product group details.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A ProductGroupDetailsDto representing the newly created product group.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<ProductGroupDetailsDto> Handle(CreateProductGroupCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            // Map command to entity
            var productGroup = _mapper.Map<VibeCRM.Domain.Entities.BusinessEntities.ProductGroup>(request);

            try
            {
                // Add the product group to the repository
                var createdProductGroup = await _productGroupRepository.AddAsync(productGroup, cancellationToken);
                _logger.LogInformation("Created new product group with ID: {ProductGroupId}", createdProductGroup.ProductGroupId);

                // Return the mapped DTO
                return _mapper.Map<ProductGroupDetailsDto>(createdProductGroup);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating product group: {Name}", request.Name);
                throw;
            }
        }
    }
}