using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.AccountStatus;

namespace VibeCRM.Application.Features.AccountStatus.Queries.GetAccountStatusByOrdinalPosition
{
    /// <summary>
    /// Handler for processing GetAccountStatusByOrdinalPositionQuery requests.
    /// Implements the CQRS query handler pattern for retrieving a specific account status by its ordinal position.
    /// </summary>
    public class GetAccountStatusByOrdinalPositionQueryHandler : IRequestHandler<GetAccountStatusByOrdinalPositionQuery, AccountStatusDetailsDto?>
    {
        private readonly IAccountStatusRepository _accountStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAccountStatusByOrdinalPositionQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAccountStatusByOrdinalPositionQueryHandler"/> class.
        /// </summary>
        /// <param name="accountStatusRepository">The account status repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        public GetAccountStatusByOrdinalPositionQueryHandler(
            IAccountStatusRepository accountStatusRepository,
            IMapper mapper,
            ILogger<GetAccountStatusByOrdinalPositionQueryHandler> logger)
        {
            _accountStatusRepository = accountStatusRepository ?? throw new ArgumentNullException(nameof(accountStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAccountStatusByOrdinalPositionQuery by retrieving a specific account status from the database by its ordinal position.
        /// </summary>
        /// <param name="request">The GetAccountStatusByOrdinalPositionQuery containing the ordinal position of the account status to retrieve.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>An AccountStatusDetailsDto representing the requested account status, or null if not found.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<AccountStatusDetailsDto?> Handle(GetAccountStatusByOrdinalPositionQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Retrieving account status with ordinal position: {OrdinalPosition}", request.OrdinalPosition);

                var accountStatuses = await _accountStatusRepository.GetByOrdinalPositionAsync(cancellationToken);

                var accountStatus = accountStatuses
                    .FirstOrDefault(a => a.OrdinalPosition == request.OrdinalPosition);

                if (accountStatus == null)
                {
                    _logger.LogWarning("Account status with ordinal position {OrdinalPosition} not found", request.OrdinalPosition);
                    return null;
                }

                var accountStatusDto = _mapper.Map<AccountStatusDetailsDto>(accountStatus);
                accountStatusDto.CompanyCount = 0;

                _logger.LogInformation("Successfully retrieved account status with ordinal position: {OrdinalPosition}", request.OrdinalPosition);

                return accountStatusDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving account status with ordinal position {OrdinalPosition}: {ErrorMessage}",
                    request.OrdinalPosition, ex.Message);
                throw;
            }
        }
    }
}