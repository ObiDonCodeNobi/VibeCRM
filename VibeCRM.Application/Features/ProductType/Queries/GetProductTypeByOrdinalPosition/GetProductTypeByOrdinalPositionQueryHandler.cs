using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.ProductType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.ProductType.Queries.GetProductTypeByOrdinalPosition
{
    /// <summary>
    /// Handler for the GetProductTypeByOrdinalPositionQuery.
    /// Retrieves a product type by its ordinal position from the database.
    /// </summary>
    public class GetProductTypeByOrdinalPositionQueryHandler : IRequestHandler<GetProductTypeByOrdinalPositionQuery, ProductTypeDto>
    {
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetProductTypeByOrdinalPositionQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetProductTypeByOrdinalPositionQueryHandler"/> class.
        /// </summary>
        /// <param name="productTypeRepository">The product type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetProductTypeByOrdinalPositionQueryHandler(
            IProductTypeRepository productTypeRepository,
            IMapper mapper,
            ILogger<GetProductTypeByOrdinalPositionQueryHandler> logger)
        {
            _productTypeRepository = productTypeRepository ?? throw new ArgumentNullException(nameof(productTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetProductTypeByOrdinalPositionQuery by retrieving a product type by its ordinal position.
        /// </summary>
        /// <param name="request">The query containing the ordinal position of the product type to retrieve.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The product type DTO if found, otherwise null.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when no product type with the specified ordinal position is found.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the retrieval process.</exception>
        public async Task<ProductTypeDto> Handle(GetProductTypeByOrdinalPositionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving product type with ordinal position: {OrdinalPosition}", request.OrdinalPosition);

                // Get product types ordered by ordinal position
                var productTypes = await _productTypeRepository.GetByOrdinalPositionAsync(cancellationToken);
                
                // Find the product type with the requested ordinal position
                var productType = productTypes.FirstOrDefault(pt => pt.OrdinalPosition == request.OrdinalPosition);
                
                if (productType == null)
                {
                    _logger.LogError("Product type with ordinal position: {OrdinalPosition} not found", request.OrdinalPosition);
                    throw new KeyNotFoundException($"Product type with ordinal position: {request.OrdinalPosition} not found");
                }

                // Map to DTO
                var productTypeDto = _mapper.Map<ProductTypeDto>(productType);

                _logger.LogInformation("Successfully retrieved product type with ordinal position: {OrdinalPosition}", request.OrdinalPosition);

                return productTypeDto;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving product type by ordinal position: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
