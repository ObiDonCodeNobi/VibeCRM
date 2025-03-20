using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.AccountType.Commands.CreateAccountType
{
    /// <summary>
    /// Handler for the CreateAccountTypeCommand.
    /// Creates a new account type in the system.
    /// </summary>
    public class CreateAccountTypeCommandHandler : IRequestHandler<CreateAccountTypeCommand, Guid>
    {
        private readonly IAccountTypeRepository _accountTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateAccountTypeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAccountTypeCommandHandler"/> class.
        /// </summary>
        /// <param name="accountTypeRepository">The account type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public CreateAccountTypeCommandHandler(
            IAccountTypeRepository accountTypeRepository,
            IMapper mapper,
            ILogger<CreateAccountTypeCommandHandler> logger)
        {
            _accountTypeRepository = accountTypeRepository ?? throw new ArgumentNullException(nameof(accountTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreateAccountTypeCommand by creating a new account type.
        /// </summary>
        /// <param name="request">The command containing the account type details.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The ID of the newly created account type.</returns>
        public async Task<Guid> Handle(CreateAccountTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Creating new account type with type name: {TypeName}", request.Type);

                // Map command to entity
                var accountType = _mapper.Map<VibeCRM.Domain.Entities.TypeStatusEntities.AccountType>(request);

                // Set audit fields
                accountType.CreatedDate = DateTime.UtcNow;
                accountType.ModifiedDate = accountType.CreatedDate;

                // Ensure Active is set to true (soft delete pattern)
                accountType.Active = true;

                // Save to repository
                var createdAccountType = await _accountTypeRepository.AddAsync(accountType, cancellationToken);

                _logger.LogInformation("Successfully created account type with ID: {AccountTypeId}", createdAccountType.Id);

                return createdAccountType.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating account type: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}