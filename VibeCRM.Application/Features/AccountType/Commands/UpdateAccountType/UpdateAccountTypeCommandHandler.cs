using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.AccountType.Commands.UpdateAccountType
{
    /// <summary>
    /// Handler for the UpdateAccountTypeCommand.
    /// Updates an existing account type in the system.
    /// </summary>
    public class UpdateAccountTypeCommandHandler : IRequestHandler<UpdateAccountTypeCommand, bool>
    {
        private readonly IAccountTypeRepository _accountTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateAccountTypeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAccountTypeCommandHandler"/> class.
        /// </summary>
        /// <param name="accountTypeRepository">The account type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public UpdateAccountTypeCommandHandler(
            IAccountTypeRepository accountTypeRepository,
            IMapper mapper,
            ILogger<UpdateAccountTypeCommandHandler> logger)
        {
            _accountTypeRepository = accountTypeRepository ?? throw new ArgumentNullException(nameof(accountTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdateAccountTypeCommand by updating an existing account type.
        /// </summary>
        /// <param name="request">The command containing the updated account type details.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if the account type was updated successfully; otherwise, false.</returns>
        public async Task<bool> Handle(UpdateAccountTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Updating account type with ID: {AccountTypeId}", request.Id);

                // Check if account type exists
                var existingAccountType = await _accountTypeRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingAccountType == null)
                {
                    _logger.LogWarning("Account type with ID {AccountTypeId} not found", request.Id);
                    return false;
                }

                // Update properties
                existingAccountType.Type = request.Type;
                existingAccountType.Description = request.Description;
                existingAccountType.OrdinalPosition = request.OrdinalPosition;
                existingAccountType.ModifiedDate = DateTime.UtcNow;

                // Save to repository
                await _accountTypeRepository.UpdateAsync(existingAccountType, cancellationToken);

                _logger.LogInformation("Successfully updated account type with ID: {AccountTypeId}", request.Id);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating account type with ID {AccountTypeId}: {ErrorMessage}", request.Id, ex.Message);
                throw;
            }
        }
    }
}