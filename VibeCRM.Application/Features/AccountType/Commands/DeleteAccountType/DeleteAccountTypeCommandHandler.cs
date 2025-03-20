using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.AccountType.Commands.DeleteAccountType
{
    /// <summary>
    /// Handler for the DeleteAccountTypeCommand.
    /// Performs a soft delete of an account type in the system.
    /// </summary>
    public class DeleteAccountTypeCommandHandler : IRequestHandler<DeleteAccountTypeCommand, bool>
    {
        private readonly IAccountTypeRepository _accountTypeRepository;
        private readonly ILogger<DeleteAccountTypeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteAccountTypeCommandHandler"/> class.
        /// </summary>
        /// <param name="accountTypeRepository">The account type repository.</param>
        /// <param name="logger">The logger.</param>
        public DeleteAccountTypeCommandHandler(
            IAccountTypeRepository accountTypeRepository,
            ILogger<DeleteAccountTypeCommandHandler> logger)
        {
            _accountTypeRepository = accountTypeRepository ?? throw new ArgumentNullException(nameof(accountTypeRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeleteAccountTypeCommand by soft deleting an account type.
        /// </summary>
        /// <param name="request">The command containing the ID of the account type to delete.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if the account type was deleted successfully; otherwise, false.</returns>
        public async Task<bool> Handle(DeleteAccountTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Soft deleting account type with ID: {AccountTypeId}", request.Id);

                // Check if account type exists
                var exists = await _accountTypeRepository.ExistsAsync(request.Id, cancellationToken);
                if (!exists)
                {
                    _logger.LogWarning("Account type with ID {AccountTypeId} not found", request.Id);
                    return false;
                }

                // Perform soft delete (sets Active = 0)
                var result = await _accountTypeRepository.DeleteAsync(request.Id, cancellationToken);

                if (result)
                {
                    _logger.LogInformation("Successfully soft deleted account type with ID: {AccountTypeId}", request.Id);
                }
                else
                {
                    _logger.LogWarning("Failed to soft delete account type with ID: {AccountTypeId}", request.Id);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error soft deleting account type with ID {AccountTypeId}: {ErrorMessage}", request.Id, ex.Message);
                throw;
            }
        }
    }
}