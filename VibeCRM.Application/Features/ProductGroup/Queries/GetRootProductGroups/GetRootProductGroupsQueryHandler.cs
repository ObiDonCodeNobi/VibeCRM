using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.ProductGroup;

namespace VibeCRM.Application.Features.ProductGroup.Queries.GetRootProductGroups
{
    /// <summary>
    /// Handler for processing GetRootProductGroupsQuery requests.
    /// Implements the CQRS query handler pattern for retrieving all root-level product groups.
    /// </summary>
    public class GetRootProductGroupsQueryHandler : IRequestHandler<GetRootProductGroupsQuery, IEnumerable<ProductGroupListDto>>
    {
        private readonly IProductGroupRepository _productGroupRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetRootProductGroupsQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetRootProductGroupsQueryHandler"/> class.
        /// </summary>
        /// <param name="productGroupRepository">The product group repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public GetRootProductGroupsQueryHandler(
            IProductGroupRepository productGroupRepository,
            IMapper mapper,
            ILogger<GetRootProductGroupsQueryHandler> logger)
        {
            _productGroupRepository = productGroupRepository ?? throw new ArgumentNullException(nameof(productGroupRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetRootProductGroupsQuery by retrieving all root-level product groups from the database.
        /// </summary>
        /// <param name="request">The GetRootProductGroupsQuery request.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of ProductGroupListDto objects representing all root-level product groups.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<IEnumerable<ProductGroupListDto>> Handle(GetRootProductGroupsQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                // Retrieve all root product groups (those without a parent)
                var rootProductGroups = await _productGroupRepository.GetRootGroupsAsync(cancellationToken);

                // Map to DTOs
                var productGroupDtos = _mapper.Map<IEnumerable<ProductGroupListDto>>(rootProductGroups);

                _logger.LogInformation("Retrieved {Count} root product groups", rootProductGroups.Count());

                return productGroupDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving root product groups");
                throw;
            }
        }
    }
}