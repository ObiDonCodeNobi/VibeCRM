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

namespace VibeCRM.Application.Features.Product.Queries.GetAllProducts
{
    /// <summary>
    /// Handler for processing GetAllProductsQuery requests
    /// </summary>
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductListDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllProductsQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllProductsQueryHandler"/> class.
        /// </summary>
        /// <param name="productRepository">The product repository for data access</param>
        /// <param name="mapper">The AutoMapper instance for object mapping</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public GetAllProductsQueryHandler(
            IProductRepository productRepository,
            IMapper mapper,
            ILogger<GetAllProductsQueryHandler> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllProductsQuery request
        /// </summary>
        /// <param name="request">The request to retrieve all active products</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of product list DTOs</returns>
        public async Task<IEnumerable<ProductListDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving all active products");

            // Get all active products
            var products = await _productRepository.GetAllAsync(cancellationToken);

            if (products == null || !products.Any())
            {
                _logger.LogInformation("No active products found");
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

            _logger.LogInformation("Successfully retrieved {Count} active products", products.Count());

            // Map the product entities to DTOs
            return _mapper.Map<IEnumerable<ProductListDto>>(products);
        }
    }
}
