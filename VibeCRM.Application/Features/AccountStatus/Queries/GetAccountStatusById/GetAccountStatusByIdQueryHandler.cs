using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.AccountStatus;

namespace VibeCRM.Application.Features.AccountStatus.Queries.GetAccountStatusById
{
    /// <summary>
    /// Handler for processing GetAccountStatusByIdQuery requests.
    /// Implements the CQRS query handler pattern for retrieving a specific account status by its ID.
    /// </summary>
    public class GetAccountStatusByIdQueryHandler : IRequestHandler<GetAccountStatusByIdQuery, AccountStatusDetailsDto?>
    {
        private readonly IAccountStatusRepository _accountStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAccountStatusByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAccountStatusByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="accountStatusRepository">The account status repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        public GetAccountStatusByIdQueryHandler(
            IAccountStatusRepository accountStatusRepository,
            IMapper mapper,
            ILogger<GetAccountStatusByIdQueryHandler> logger)
        {
            _accountStatusRepository = accountStatusRepository ?? throw new ArgumentNullException(nameof(accountStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAccountStatusByIdQuery by retrieving a specific account status from the database.
        /// </summary>
        /// <param name="request">The GetAccountStatusByIdQuery containing the ID of the account status to retrieve.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>An AccountStatusDetailsDto representing the requested account status, or null if not found.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<AccountStatusDetailsDto?> Handle(GetAccountStatusByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Retrieving account status with ID: {AccountStatusId}", request.Id);

                var accountStatus = await _accountStatusRepository.GetByIdAsync(request.Id, cancellationToken);

                if (accountStatus == null)
                {
                    _logger.LogWarning("Account status with ID {AccountStatusId} not found", request.Id);
                    return null;
                }

                var accountStatusDto = _mapper.Map<AccountStatusDetailsDto>(accountStatus);
                accountStatusDto.CompanyCount = 0;

                _logger.LogInformation("Successfully retrieved account status with ID: {AccountStatusId}", request.Id);

                return accountStatusDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving account status with ID {AccountStatusId}: {ErrorMessage}",
                    request.Id, ex.Message);
                throw;
            }
        }
    }
}