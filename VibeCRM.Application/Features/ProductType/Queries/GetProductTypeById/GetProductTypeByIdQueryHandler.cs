using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.ProductType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.ProductType.Queries.GetProductTypeById
{
    /// <summary>
    /// Handler for the GetProductTypeByIdQuery.
    /// Retrieves a product type by its ID from the database.
    /// </summary>
    public class GetProductTypeByIdQueryHandler : IRequestHandler<GetProductTypeByIdQuery, ProductTypeDetailsDto>
    {
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetProductTypeByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetProductTypeByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="productTypeRepository">The product type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetProductTypeByIdQueryHandler(
            IProductTypeRepository productTypeRepository,
            IMapper mapper,
            ILogger<GetProductTypeByIdQueryHandler> logger)
        {
            _productTypeRepository = productTypeRepository ?? throw new ArgumentNullException(nameof(productTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetProductTypeByIdQuery by retrieving a product type by its ID.
        /// </summary>
        /// <param name="request">The query containing the ID of the product type to retrieve.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The product type details DTO if found, otherwise null.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the product type with the specified ID is not found.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the retrieval process.</exception>
        public async Task<ProductTypeDetailsDto> Handle(GetProductTypeByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving product type with ID: {ProductTypeId}", request.Id);

                // Get product type by ID
                var productType = await _productTypeRepository.GetByIdAsync(request.Id, cancellationToken);
                if (productType == null)
                {
                    _logger.LogError("Product type with ID: {ProductTypeId} not found", request.Id);
                    throw new KeyNotFoundException($"Product type with ID: {request.Id} not found");
                }

                // Map to DTO
                var productTypeDto = _mapper.Map<ProductTypeDetailsDto>(productType);

                _logger.LogInformation("Successfully retrieved product type with ID: {ProductTypeId}", productTypeDto.Id);

                return productTypeDto;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving product type by ID: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}