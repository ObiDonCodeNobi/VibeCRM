using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.AccountType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.AccountType.Queries.GetAccountTypeByOrdinalPosition
{
    /// <summary>
    /// Handler for the GetAccountTypeByOrdinalPositionQuery.
    /// Retrieves account types ordered by their ordinal position.
    /// </summary>
    public class GetAccountTypeByOrdinalPositionQueryHandler : IRequestHandler<GetAccountTypeByOrdinalPositionQuery, IEnumerable<AccountTypeListDto>>
    {
        private readonly IAccountTypeRepository _accountTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAccountTypeByOrdinalPositionQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAccountTypeByOrdinalPositionQueryHandler"/> class.
        /// </summary>
        /// <param name="accountTypeRepository">The account type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetAccountTypeByOrdinalPositionQueryHandler(
            IAccountTypeRepository accountTypeRepository,
            IMapper mapper,
            ILogger<GetAccountTypeByOrdinalPositionQueryHandler> logger)
        {
            _accountTypeRepository = accountTypeRepository ?? throw new ArgumentNullException(nameof(accountTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAccountTypeByOrdinalPositionQuery by retrieving account types ordered by their ordinal position.
        /// </summary>
        /// <param name="request">The query to retrieve account types ordered by ordinal position.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A collection of account type list DTOs ordered by ordinal position.</returns>
        public async Task<IEnumerable<AccountTypeListDto>> Handle(GetAccountTypeByOrdinalPositionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving account types ordered by ordinal position");

                // Get account types from repository
                var accountTypes = await _accountTypeRepository.GetByOrdinalPositionAsync(cancellationToken);

                // Map to DTOs
                var accountTypeDtos = _mapper.Map<IEnumerable<AccountTypeListDto>>(accountTypes);

                // Set company count to 0 for now
                // In a real implementation, you would retrieve the actual company counts
                foreach (var dto in accountTypeDtos)
                {
                    dto.CompanyCount = 0;
                }

                _logger.LogInformation("Successfully retrieved account types ordered by ordinal position");

                return accountTypeDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving account types ordered by ordinal position: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}