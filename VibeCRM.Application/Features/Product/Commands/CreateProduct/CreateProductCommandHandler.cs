using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Product.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Product.Commands.CreateProduct
{
    /// <summary>
    /// Handler for processing CreateProductCommand requests
    /// </summary>
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDetailsDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProductCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateProductCommandHandler"/> class.
        /// </summary>
        /// <param name="productRepository">The product repository for data access</param>
        /// <param name="mapper">The AutoMapper instance for object mapping</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public CreateProductCommandHandler(
            IProductRepository productRepository,
            IMapper mapper,
            ILogger<CreateProductCommandHandler> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreateProductCommand request
        /// </summary>
        /// <param name="request">The request containing the product information to create</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The created product details DTO</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null</exception>
        /// <exception cref="ArgumentException">Thrown when required fields are missing</exception>
        public async Task<ProductDetailsDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ArgumentException("Product name is required", nameof(request.Name));
            }

            if (request.ProductTypeId == Guid.Empty)
            {
                throw new ArgumentException("Product type ID is required", nameof(request.ProductTypeId));
            }

            _logger.LogInformation("Creating new product with name: {ProductName}", request.Name);

            // Create a new product entity
            var product = new VibeCRM.Domain.Entities.BusinessEntities.Product
            {
                ProductId = Guid.NewGuid(),
                ProductTypeId = request.ProductTypeId,
                Name = request.Name,
                Description = request.Description,
                CreatedBy = Guid.Parse(request.CreatedBy),
                CreatedDate = DateTime.UtcNow,
                ModifiedBy = Guid.Parse(request.CreatedBy),
                ModifiedDate = DateTime.UtcNow,
                Active = true
            };

            // Add the product to the repository
            var createdProduct = await _productRepository.AddAsync(product, cancellationToken);

            // Load related entities
            await _productRepository.LoadProductTypeAsync(createdProduct, cancellationToken);

            _logger.LogInformation("Successfully created product with ID: {ProductId}", createdProduct.ProductId);

            // Map the product entity to the DTO
            return _mapper.Map<ProductDetailsDto>(createdProduct);
        }
    }
}