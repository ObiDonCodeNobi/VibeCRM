using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.ProductType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.ProductType.Queries.GetDefaultProductType
{
    /// <summary>
    /// Handler for the GetDefaultProductTypeQuery.
    /// Retrieves the default product type from the database.
    /// </summary>
    public class GetDefaultProductTypeQueryHandler : IRequestHandler<GetDefaultProductTypeQuery, ProductTypeDto>
    {
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDefaultProductTypeQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetDefaultProductTypeQueryHandler"/> class.
        /// </summary>
        /// <param name="productTypeRepository">The product type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetDefaultProductTypeQueryHandler(
            IProductTypeRepository productTypeRepository,
            IMapper mapper,
            ILogger<GetDefaultProductTypeQueryHandler> logger)
        {
            _productTypeRepository = productTypeRepository ?? throw new ArgumentNullException(nameof(productTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetDefaultProductTypeQuery by retrieving the default product type.
        /// </summary>
        /// <param name="request">The query request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The default product type DTO if found, otherwise null.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when no default product type is found.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the retrieval process.</exception>
        public async Task<ProductTypeDto> Handle(GetDefaultProductTypeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving default product type");

                // Get default product type
                var defaultProductType = await _productTypeRepository.GetDefaultAsync(cancellationToken);
                
                if (defaultProductType == null)
                {
                    _logger.LogError("Default product type not found");
                    throw new KeyNotFoundException("Default product type not found");
                }

                // Map to DTO
                var productTypeDto = _mapper.Map<ProductTypeDto>(defaultProductType);

                _logger.LogInformation("Successfully retrieved default product type with ID: {ProductTypeId}", productTypeDto.Id);

                return productTypeDto;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving default product type: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
