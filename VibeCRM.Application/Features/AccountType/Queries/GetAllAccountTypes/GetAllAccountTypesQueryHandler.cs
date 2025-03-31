using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.AccountType;

namespace VibeCRM.Application.Features.AccountType.Queries.GetAllAccountTypes
{
    /// <summary>
    /// Handler for the GetAllAccountTypesQuery.
    /// Retrieves all account types in the system.
    /// </summary>
    public class GetAllAccountTypesQueryHandler : IRequestHandler<GetAllAccountTypesQuery, IEnumerable<AccountTypeListDto>>
    {
        private readonly IAccountTypeRepository _accountTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllAccountTypesQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllAccountTypesQueryHandler"/> class.
        /// </summary>
        /// <param name="accountTypeRepository">The account type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetAllAccountTypesQueryHandler(
            IAccountTypeRepository accountTypeRepository,
            IMapper mapper,
            ILogger<GetAllAccountTypesQueryHandler> logger)
        {
            _accountTypeRepository = accountTypeRepository ?? throw new ArgumentNullException(nameof(accountTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllAccountTypesQuery by retrieving all account types.
        /// </summary>
        /// <param name="request">The query to retrieve all account types.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A collection of account type list DTOs.</returns>
        public async Task<IEnumerable<AccountTypeListDto>> Handle(GetAllAccountTypesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving all account types");

                // Get all account types from repository
                var accountTypes = await _accountTypeRepository.GetAllAsync(cancellationToken);

                // Map to DTOs
                var accountTypeDtos = _mapper.Map<IEnumerable<AccountTypeListDto>>(accountTypes);

                // Set company count to 0 for now
                // In a real implementation, you would retrieve the actual company counts
                foreach (var dto in accountTypeDtos)
                {
                    dto.CompanyCount = 0;
                }

                _logger.LogInformation("Successfully retrieved all account types");

                return accountTypeDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all account types: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}