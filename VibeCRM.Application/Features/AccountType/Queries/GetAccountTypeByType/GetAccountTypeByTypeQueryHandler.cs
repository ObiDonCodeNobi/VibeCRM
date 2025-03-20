using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.AccountType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.AccountType.Queries.GetAccountTypeByType
{
    /// <summary>
    /// Handler for the GetAccountTypeByTypeQuery.
    /// Retrieves account types by their type name.
    /// </summary>
    public class GetAccountTypeByTypeQueryHandler : IRequestHandler<GetAccountTypeByTypeQuery, IEnumerable<AccountTypeListDto>>
    {
        private readonly IAccountTypeRepository _accountTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAccountTypeByTypeQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAccountTypeByTypeQueryHandler"/> class.
        /// </summary>
        /// <param name="accountTypeRepository">The account type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetAccountTypeByTypeQueryHandler(
            IAccountTypeRepository accountTypeRepository,
            IMapper mapper,
            ILogger<GetAccountTypeByTypeQueryHandler> logger)
        {
            _accountTypeRepository = accountTypeRepository ?? throw new ArgumentNullException(nameof(accountTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAccountTypeByTypeQuery by retrieving account types by their type name.
        /// </summary>
        /// <param name="request">The query containing the type name to search for.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A collection of account type list DTOs matching the type name.</returns>
        public async Task<IEnumerable<AccountTypeListDto>> Handle(GetAccountTypeByTypeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving account types with type name: {TypeName}", request.Type);

                // Get account types from repository
                var accountTypes = await _accountTypeRepository.GetByTypeAsync(request.Type, cancellationToken);

                // Map to DTOs
                var accountTypeDtos = _mapper.Map<IEnumerable<AccountTypeListDto>>(accountTypes);

                // Set company count to 0 for now
                // In a real implementation, you would retrieve the actual company counts
                foreach (var dto in accountTypeDtos)
                {
                    dto.CompanyCount = 0;
                }

                _logger.LogInformation("Successfully retrieved account types with type name: {TypeName}", request.Type);

                return accountTypeDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving account types with type name {TypeName}: {ErrorMessage}", request.Type, ex.Message);
                throw;
            }
        }
    }
}