using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.ProductType;

namespace VibeCRM.Application.Features.ProductType.Queries.GetProductTypeByType
{
    /// <summary>
    /// Handler for the GetProductTypeByTypeQuery.
    /// Retrieves a product type by its type name from the database.
    /// </summary>
    public class GetProductTypeByTypeQueryHandler : IRequestHandler<GetProductTypeByTypeQuery, ProductTypeDto>
    {
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetProductTypeByTypeQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetProductTypeByTypeQueryHandler"/> class.
        /// </summary>
        /// <param name="productTypeRepository">The product type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetProductTypeByTypeQueryHandler(
            IProductTypeRepository productTypeRepository,
            IMapper mapper,
            ILogger<GetProductTypeByTypeQueryHandler> logger)
        {
            _productTypeRepository = productTypeRepository ?? throw new ArgumentNullException(nameof(productTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetProductTypeByTypeQuery by retrieving a product type by its type name.
        /// </summary>
        /// <param name="request">The query containing the type name of the product type to retrieve.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The product type DTO if found, otherwise null.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when no product type with the specified type name is found.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the retrieval process.</exception>
        public async Task<ProductTypeDto> Handle(GetProductTypeByTypeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving product type with type name: {TypeName}", request.Type);

                // Get product types by type name
                var productTypes = await _productTypeRepository.GetByTypeAsync(request.Type, cancellationToken);

                // Get the first product type with the requested type name
                var productType = productTypes.FirstOrDefault();

                if (productType == null)
                {
                    _logger.LogError("Product type with type name: {TypeName} not found", request.Type);
                    throw new KeyNotFoundException($"Product type with type name: {request.Type} not found");
                }

                // Map to DTO
                var productTypeDto = _mapper.Map<ProductTypeDto>(productType);

                _logger.LogInformation("Successfully retrieved product type with type name: {TypeName}", request.Type);

                return productTypeDto;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving product type by type name: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}