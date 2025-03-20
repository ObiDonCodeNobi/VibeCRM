using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VibeCRM.Application.Features.Product.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Product.Queries.GetProductsByProductType
{
    /// <summary>
    /// Handler for processing GetProductsByProductTypeQuery requests
    /// </summary>
    public class GetProductsByProductTypeQueryHandler : IRequestHandler<GetProductsByProductTypeQuery, IEnumerable<ProductListDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetProductsByProductTypeQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetProductsByProductTypeQueryHandler"/> class.
        /// </summary>
        /// <param name="productRepository">The product repository for data access</param>
        /// <param name="mapper">The AutoMapper instance for object mapping</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public GetProductsByProductTypeQueryHandler(
            IProductRepository productRepository,
            IMapper mapper,
            ILogger<GetProductsByProductTypeQueryHandler> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetProductsByProductTypeQuery request
        /// </summary>
        /// <param name="request">The request containing the product type ID to filter products by</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of product list DTOs that match the specified product type</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null</exception>
        /// <exception cref="ArgumentException">Thrown when the product type ID is empty</exception>
        public async Task<IEnumerable<ProductListDto>> Handle(GetProductsByProductTypeQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.ProductTypeId == Guid.Empty)
            {
                throw new ArgumentException("Product Type ID is required", nameof(request.ProductTypeId));
            }

            _logger.LogInformation("Retrieving products for product type ID: {ProductTypeId}", request.ProductTypeId);

            // Get products by product type ID
            var products = await _productRepository.GetByProductTypeIdAsync(request.ProductTypeId, cancellationToken);

            if (products == null || !products.Any())
            {
                _logger.LogInformation("No products found for product type ID: {ProductTypeId}", request.ProductTypeId);
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

            _logger.LogInformation("Successfully retrieved {Count} products for product type ID: {ProductTypeId}", 
                products.Count(), request.ProductTypeId);

            // Map the product entities to DTOs
            return _mapper.Map<IEnumerable<ProductListDto>>(products);
        }
    }
}
