using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.Product;

namespace VibeCRM.Application.Features.Product.Queries.GetProductsByProductGroup
{
    /// <summary>
    /// Handler for processing GetProductsByProductGroupQuery requests
    /// </summary>
    public class GetProductsByProductGroupQueryHandler : IRequestHandler<GetProductsByProductGroupQuery, IEnumerable<ProductListDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetProductsByProductGroupQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetProductsByProductGroupQueryHandler"/> class.
        /// </summary>
        /// <param name="productRepository">The product repository for data access</param>
        /// <param name="mapper">The AutoMapper instance for object mapping</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public GetProductsByProductGroupQueryHandler(
            IProductRepository productRepository,
            IMapper mapper,
            ILogger<GetProductsByProductGroupQueryHandler> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetProductsByProductGroupQuery request
        /// </summary>
        /// <param name="request">The request containing the product group ID to filter products by</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of product list DTOs that belong to the specified product group</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null</exception>
        /// <exception cref="ArgumentException">Thrown when the product group ID is empty</exception>
        public async Task<IEnumerable<ProductListDto>> Handle(GetProductsByProductGroupQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.ProductGroupId == Guid.Empty)
            {
                throw new ArgumentException("Product Group ID is required", nameof(request.ProductGroupId));
            }

            _logger.LogInformation("Retrieving products for product group ID: {ProductGroupId}", request.ProductGroupId);

            // Get products by product group ID
            var products = await _productRepository.GetByProductGroupIdAsync(request.ProductGroupId, cancellationToken);

            if (products == null || !products.Any())
            {
                _logger.LogInformation("No products found for product group ID: {ProductGroupId}", request.ProductGroupId);
                return new List<ProductListDto>();
            }

            // Load related entities for each product
            foreach (var product in products)
            {
                await _productRepository.LoadProductTypeAsync(product, cancellationToken);
                await _productRepository.LoadQuoteLineItemsAsync(product, cancellationToken);
                await _productRepository.LoadSalesOrderLineItemsAsync(product, cancellationToken);
                await _productRepository.LoadProductGroupsAsync(product, cancellationToken);
            }

            _logger.LogInformation("Successfully retrieved {Count} products for product group ID: {ProductGroupId}",
                products.Count(), request.ProductGroupId);

            // Map the product entities to DTOs
            return _mapper.Map<IEnumerable<ProductListDto>>(products);
        }
    }
}