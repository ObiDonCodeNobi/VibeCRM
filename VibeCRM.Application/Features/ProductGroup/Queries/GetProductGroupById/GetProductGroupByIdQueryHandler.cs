using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.ProductGroup;

namespace VibeCRM.Application.Features.ProductGroup.Queries.GetProductGroupById
{
    /// <summary>
    /// Handler for processing GetProductGroupByIdQuery requests.
    /// Implements the CQRS query handler pattern for retrieving a specific product group by its ID.
    /// </summary>
    public class GetProductGroupByIdQueryHandler : IRequestHandler<GetProductGroupByIdQuery, ProductGroupDetailsDto>
    {
        private readonly IProductGroupRepository _productGroupRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetProductGroupByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetProductGroupByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="productGroupRepository">The product group repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public GetProductGroupByIdQueryHandler(
            IProductGroupRepository productGroupRepository,
            IMapper mapper,
            ILogger<GetProductGroupByIdQueryHandler> logger)
        {
            _productGroupRepository = productGroupRepository ?? throw new ArgumentNullException(nameof(productGroupRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetProductGroupByIdQuery by retrieving a specific product group from the database.
        /// </summary>
        /// <param name="request">The GetProductGroupByIdQuery containing the product group ID to retrieve.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A ProductGroupDetailsDto representing the requested product group, or null if not found.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<ProductGroupDetailsDto> Handle(GetProductGroupByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                // Retrieve the product group by ID
                var productGroup = await _productGroupRepository.GetByIdAsync(request.Id, cancellationToken);

                if (productGroup == null || !productGroup.Active)
                {
                    _logger.LogWarning("Product group not found with ID: {ProductGroupId}", request.Id);
                    return new ProductGroupDetailsDto();
                }

                // Map to DTO
                var productGroupDto = _mapper.Map<ProductGroupDetailsDto>(productGroup);

                _logger.LogInformation("Retrieved product group with ID: {ProductGroupId}", request.Id);

                return productGroupDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving product group with ID: {ProductGroupId}", request.Id);
                throw;
            }
        }
    }
}