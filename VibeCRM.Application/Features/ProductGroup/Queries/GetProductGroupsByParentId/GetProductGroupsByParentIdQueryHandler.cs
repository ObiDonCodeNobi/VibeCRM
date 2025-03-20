using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.ProductGroup.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.ProductGroup.Queries.GetProductGroupsByParentId
{
    /// <summary>
    /// Handler for processing GetProductGroupsByParentIdQuery requests.
    /// Implements the CQRS query handler pattern for retrieving child product groups of a specific parent.
    /// </summary>
    public class GetProductGroupsByParentIdQueryHandler : IRequestHandler<GetProductGroupsByParentIdQuery, IEnumerable<ProductGroupListDto>>
    {
        private readonly IProductGroupRepository _productGroupRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetProductGroupsByParentIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetProductGroupsByParentIdQueryHandler"/> class.
        /// </summary>
        /// <param name="productGroupRepository">The product group repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public GetProductGroupsByParentIdQueryHandler(
            IProductGroupRepository productGroupRepository,
            IMapper mapper,
            ILogger<GetProductGroupsByParentIdQueryHandler> logger)
        {
            _productGroupRepository = productGroupRepository ?? throw new ArgumentNullException(nameof(productGroupRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetProductGroupsByParentIdQuery by retrieving all child product groups of a specific parent.
        /// </summary>
        /// <param name="request">The GetProductGroupsByParentIdQuery containing the parent product group ID.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of ProductGroupListDto objects representing the child product groups.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<IEnumerable<ProductGroupListDto>> Handle(GetProductGroupsByParentIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                // Verify parent exists
                var parentProductGroup = await _productGroupRepository.GetByIdAsync(request.ParentId, cancellationToken);
                if (parentProductGroup == null || !parentProductGroup.Active)
                {
                    _logger.LogWarning("Parent product group not found with ID: {ParentProductGroupId}", request.ParentId);
                    return Enumerable.Empty<ProductGroupListDto>();
                }

                // Retrieve child product groups
                var childProductGroups = await _productGroupRepository.GetByParentGroupIdAsync(request.ParentId, cancellationToken);

                // Map to DTOs
                var productGroupDtos = _mapper.Map<IEnumerable<ProductGroupListDto>>(childProductGroups);

                _logger.LogInformation("Retrieved {Count} child product groups for parent ID: {ParentProductGroupId}",
                    childProductGroups.Count(), request.ParentId);

                return productGroupDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving child product groups for parent ID: {ParentProductGroupId}",
                    request.ParentId);
                throw;
            }
        }
    }
}