using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.AccountStatus.Commands.DeleteAccountStatus
{
    /// <summary>
    /// Handler for processing DeleteAccountStatusCommand requests.
    /// Implements the CQRS command handler pattern for soft-deleting account status entities.
    /// </summary>
    public class DeleteAccountStatusCommandHandler : IRequestHandler<DeleteAccountStatusCommand, bool>
    {
        private readonly IAccountStatusRepository _accountStatusRepository;
        private readonly ILogger<DeleteAccountStatusCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteAccountStatusCommandHandler"/> class.
        /// </summary>
        /// <param name="accountStatusRepository">The account status repository for database operations.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        public DeleteAccountStatusCommandHandler(
            IAccountStatusRepository accountStatusRepository,
            ILogger<DeleteAccountStatusCommandHandler> logger)
        {
            _accountStatusRepository = accountStatusRepository ?? throw new ArgumentNullException(nameof(accountStatusRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeleteAccountStatusCommand by soft-deleting an account status entity in the database.
        /// Sets the Active property to false rather than physically removing the record.
        /// </summary>
        /// <param name="request">The DeleteAccountStatusCommand containing the ID of the account status to delete.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the account status to delete is not found.</exception>
        public async Task<bool> Handle(DeleteAccountStatusCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Soft-deleting account status with ID: {AccountStatusId}", request.Id);

                // Get the existing entity
                var existingAccountStatus = await _accountStatusRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingAccountStatus == null)
                {
                    _logger.LogWarning("Account status with ID {AccountStatusId} not found for deletion", request.Id);
                    throw new InvalidOperationException($"Account status with ID {request.Id} not found.");
                }

                // Update the modified by information before deletion
                existingAccountStatus.ModifiedBy = request.ModifiedBy;
                existingAccountStatus.ModifiedDate = DateTime.UtcNow;

                // Perform soft delete (sets Active = false)
                var result = await _accountStatusRepository.DeleteAsync(existingAccountStatus.Id, cancellationToken);

                if (result)
                {
                    _logger.LogInformation("Successfully soft-deleted account status with ID: {AccountStatusId}", request.Id);
                }
                else
                {
                    _logger.LogWarning("Failed to soft-delete account status with ID: {AccountStatusId}", request.Id);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while soft-deleting account status with ID {AccountStatusId}: {ErrorMessage}",
                    request.Id, ex.Message);
                throw;
            }
        }
    }
}