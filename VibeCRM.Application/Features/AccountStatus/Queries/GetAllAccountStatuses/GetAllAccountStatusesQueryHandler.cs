using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.AccountStatus.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.AccountStatus.Queries.GetAllAccountStatuses
{
    /// <summary>
    /// Handler for processing GetAllAccountStatusesQuery requests.
    /// Implements the CQRS query handler pattern for retrieving all active account statuses.
    /// </summary>
    public class GetAllAccountStatusesQueryHandler : IRequestHandler<GetAllAccountStatusesQuery, IEnumerable<AccountStatusListDto>>
    {
        private readonly IAccountStatusRepository _accountStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllAccountStatusesQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllAccountStatusesQueryHandler"/> class.
        /// </summary>
        /// <param name="accountStatusRepository">The account status repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        public GetAllAccountStatusesQueryHandler(
            IAccountStatusRepository accountStatusRepository,
            IMapper mapper,
            ILogger<GetAllAccountStatusesQueryHandler> logger)
        {
            _accountStatusRepository = accountStatusRepository ?? throw new ArgumentNullException(nameof(accountStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllAccountStatusesQuery by retrieving all active account statuses from the database.
        /// </summary>
        /// <param name="request">The GetAllAccountStatusesQuery request.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of AccountStatusListDto objects representing all active account statuses.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<IEnumerable<AccountStatusListDto>> Handle(GetAllAccountStatusesQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Retrieving all active account statuses");

                // Get all active account statuses
                var accountStatuses = await _accountStatusRepository.GetAllAsync(cancellationToken);

                // The repository already filters by Active = true as per our soft delete pattern

                // Map to DTOs
                var accountStatusDtos = _mapper.Map<IEnumerable<AccountStatusListDto>>(accountStatuses);

                // Note: In a real implementation, we would need to add a method to the repository
                // to get company counts for each account status. For now, we'll set it to 0.
                foreach (var dto in accountStatusDtos)
                {
                    dto.CompanyCount = 0; // Default until repository supports company count
                }

                // Order by ordinal position
                var orderedAccountStatusDtos = accountStatusDtos.OrderBy(a => a.OrdinalPosition).ToList();

                _logger.LogInformation("Successfully retrieved {Count} active account statuses", orderedAccountStatusDtos.Count);

                return orderedAccountStatusDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all active account statuses: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}