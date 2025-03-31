using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Common.Exceptions;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.AccountType;

namespace VibeCRM.Application.Features.AccountType.Queries.GetAccountTypeById
{
    /// <summary>
    /// Handler for the GetAccountTypeByIdQuery.
    /// Retrieves an account type by its unique identifier.
    /// </summary>
    public class GetAccountTypeByIdQueryHandler : IRequestHandler<GetAccountTypeByIdQuery, AccountTypeListDto>
    {
        private readonly IAccountTypeRepository _accountTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAccountTypeByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAccountTypeByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="accountTypeRepository">The account type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetAccountTypeByIdQueryHandler(
            IAccountTypeRepository accountTypeRepository,
            IMapper mapper,
            ILogger<GetAccountTypeByIdQueryHandler> logger)
        {
            _accountTypeRepository = accountTypeRepository ?? throw new ArgumentNullException(nameof(accountTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAccountTypeByIdQuery by retrieving an account type by its ID.
        /// </summary>
        /// <param name="request">The query containing the ID of the account type to retrieve.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The account type list DTO or null if not found.</returns>
        public async Task<AccountTypeListDto> Handle(GetAccountTypeByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving account type with ID: {AccountTypeId}", request.Id);

                // Get account type from repository
                var accountType = await _accountTypeRepository.GetByIdAsync(request.Id, cancellationToken);
                if (accountType == null)
                {
                    _logger.LogWarning("Account type with ID {AccountTypeId} not found", request.Id);
                    throw new NotFoundException($"Account type with ID {request.Id} not found.");
                }

                // Map to DTO
                var accountTypeDto = _mapper.Map<AccountTypeListDto>(accountType);

                // Set company count to 0 for now
                // In a real implementation, you would retrieve the actual company count
                accountTypeDto.CompanyCount = 0;

                _logger.LogInformation("Successfully retrieved account type with ID: {AccountTypeId}", request.Id);

                return accountTypeDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving account type with ID {AccountTypeId}: {ErrorMessage}", request.Id, ex.Message);
                throw;
            }
        }
    }
}