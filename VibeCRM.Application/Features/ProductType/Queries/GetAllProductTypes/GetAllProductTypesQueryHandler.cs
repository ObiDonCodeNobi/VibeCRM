using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.ProductType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.ProductType.Queries.GetAllProductTypes
{
    /// <summary>
    /// Handler for the GetAllProductTypesQuery.
    /// Retrieves all active product types from the database.
    /// </summary>
    public class GetAllProductTypesQueryHandler : IRequestHandler<GetAllProductTypesQuery, IEnumerable<ProductTypeListDto>>
    {
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllProductTypesQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllProductTypesQueryHandler"/> class.
        /// </summary>
        /// <param name="productTypeRepository">The product type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetAllProductTypesQueryHandler(
            IProductTypeRepository productTypeRepository,
            IMapper mapper,
            ILogger<GetAllProductTypesQueryHandler> logger)
        {
            _productTypeRepository = productTypeRepository ?? throw new ArgumentNullException(nameof(productTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllProductTypesQuery by retrieving all active product types.
        /// </summary>
        /// <param name="request">The query request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A collection of product type list DTOs.</returns>
        /// <exception cref="Exception">Thrown when an error occurs during the retrieval process.</exception>
        public async Task<IEnumerable<ProductTypeListDto>> Handle(GetAllProductTypesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving all product types");

                // Get all product types
                var productTypes = await _productTypeRepository.GetAllAsync(cancellationToken);

                // Map to DTOs
                var productTypeDtos = _mapper.Map<IEnumerable<ProductTypeListDto>>(productTypes);

                _logger.LogInformation("Successfully retrieved {Count} product types", productTypeDtos.Count());

                return productTypeDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all product types: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
