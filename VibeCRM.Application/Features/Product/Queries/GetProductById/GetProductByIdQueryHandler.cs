using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.Product;

namespace VibeCRM.Application.Features.Product.Queries.GetProductById
{
    /// <summary>
    /// Handler for processing GetProductByIdQuery requests
    /// </summary>
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDetailsDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetProductByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetProductByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="productRepository">The product repository for data access</param>
        /// <param name="mapper">The AutoMapper instance for object mapping</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public GetProductByIdQueryHandler(
            IProductRepository productRepository,
            IMapper mapper,
            ILogger<GetProductByIdQueryHandler> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetProductByIdQuery request
        /// </summary>
        /// <param name="request">The request containing the product ID to retrieve</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The product details DTO if found, otherwise null</returns>
        /// <exception cref="ArgumentException">Thrown when the product ID is empty</exception>
        public async Task<ProductDetailsDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.ProductId == Guid.Empty)
            {
                throw new ArgumentException("Product ID cannot be empty", nameof(request.ProductId));
            }

            _logger.LogInformation("Retrieving product with ID: {ProductId}", request.ProductId);

            // Get the product with all related entities loaded
            var product = await _productRepository.GetByIdWithRelatedEntitiesAsync(request.ProductId, cancellationToken);

            if (product == null)
            {
                _logger.LogWarning("Product with ID {ProductId} not found", request.ProductId);
                return new ProductDetailsDto();
            }

            _logger.LogInformation("Successfully retrieved product with ID: {ProductId}", request.ProductId);

            // Map the product entity to the DTO
            return _mapper.Map<ProductDetailsDto>(product);
        }
    }
}