using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Common.Exceptions;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.AccountStatus;

namespace VibeCRM.Application.Features.AccountStatus.Queries.GetAccountStatusByStatus
{
    /// <summary>
    /// Handler for processing GetAccountStatusByStatusQuery requests.
    /// Implements the CQRS query handler pattern for retrieving a specific account status by its status name.
    /// </summary>
    public class GetAccountStatusByStatusQueryHandler : IRequestHandler<GetAccountStatusByStatusQuery, AccountStatusDetailsDto>
    {
        private readonly IAccountStatusRepository _accountStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAccountStatusByStatusQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAccountStatusByStatusQueryHandler"/> class.
        /// </summary>
        /// <param name="accountStatusRepository">The account status repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        public GetAccountStatusByStatusQueryHandler(
            IAccountStatusRepository accountStatusRepository,
            IMapper mapper,
            ILogger<GetAccountStatusByStatusQueryHandler> logger)
        {
            _accountStatusRepository = accountStatusRepository ?? throw new ArgumentNullException(nameof(accountStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAccountStatusByStatusQuery by retrieving a specific account status from the database.
        /// </summary>
        /// <param name="request">The GetAccountStatusByStatusQuery containing the status name to search for.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>An AccountStatusDetailsDto representing the requested account status.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<AccountStatusDetailsDto> Handle(GetAccountStatusByStatusQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Retrieving account status with status name: {Status}", request.Status);

                // Get the account status by status name
                var accountStatuses = await _accountStatusRepository.GetByStatusAsync(request.Status, cancellationToken);
                var accountStatus = accountStatuses.FirstOrDefault();

                if (accountStatus == null)
                {
                    _logger.LogWarning("Account status with status name {Status} not found", request.Status);
                    throw new NotFoundException($"Account status with status name {request.Status} not found.");
                }

                // Map to DTO
                var accountStatusDto = _mapper.Map<AccountStatusDetailsDto>(accountStatus);

                // Note: In a real implementation, we would need to add a method to the repository
                // to get company counts for this account status. For now, we'll set it to 0.
                accountStatusDto.CompanyCount = 0; // Default until repository supports company count

                _logger.LogInformation("Successfully retrieved account status with status name: {Status}", request.Status);

                return accountStatusDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving account status with status name {Status}: {ErrorMessage}",
                    request.Status, ex.Message);
                throw;
            }
        }
    }
}