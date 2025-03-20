using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.ProductGroup.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.ProductGroup.Queries.GetAllProductGroups
{
    /// <summary>
    /// Handler for processing GetAllProductGroupsQuery requests.
    /// Implements the CQRS query handler pattern for retrieving all active product groups.
    /// </summary>
    public class GetAllProductGroupsQueryHandler : IRequestHandler<GetAllProductGroupsQuery, IEnumerable<ProductGroupListDto>>
    {
        private readonly IProductGroupRepository _productGroupRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllProductGroupsQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllProductGroupsQueryHandler"/> class.
        /// </summary>
        /// <param name="productGroupRepository">The product group repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public GetAllProductGroupsQueryHandler(
            IProductGroupRepository productGroupRepository,
            IMapper mapper,
            ILogger<GetAllProductGroupsQueryHandler> logger)
        {
            _productGroupRepository = productGroupRepository ?? throw new ArgumentNullException(nameof(productGroupRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllProductGroupsQuery by retrieving all active product groups from the database.
        /// </summary>
        /// <param name="request">The GetAllProductGroupsQuery request.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of ProductGroupListDto objects representing all active product groups.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<IEnumerable<ProductGroupListDto>> Handle(GetAllProductGroupsQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                // Retrieve all active product groups
                var productGroups = await _productGroupRepository.GetAllAsync(cancellationToken);

                // Map to DTOs
                var productGroupDtos = _mapper.Map<IEnumerable<ProductGroupListDto>>(productGroups);

                _logger.LogInformation("Retrieved {Count} product groups", productGroups.Count());

                return productGroupDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all product groups");
                throw;
            }
        }
    }
}