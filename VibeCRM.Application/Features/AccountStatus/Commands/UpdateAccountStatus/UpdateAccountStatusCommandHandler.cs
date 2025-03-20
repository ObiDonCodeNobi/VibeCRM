using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.AccountStatus.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.AccountStatus.Commands.UpdateAccountStatus
{
    /// <summary>
    /// Handler for processing UpdateAccountStatusCommand requests.
    /// Implements the CQRS command handler pattern for updating account status entities.
    /// </summary>
    public class UpdateAccountStatusCommandHandler : IRequestHandler<UpdateAccountStatusCommand, AccountStatusDto>
    {
        private readonly IAccountStatusRepository _accountStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateAccountStatusCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAccountStatusCommandHandler"/> class.
        /// </summary>
        /// <param name="accountStatusRepository">The account status repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        public UpdateAccountStatusCommandHandler(
            IAccountStatusRepository accountStatusRepository,
            IMapper mapper,
            ILogger<UpdateAccountStatusCommandHandler> logger)
        {
            _accountStatusRepository = accountStatusRepository ?? throw new ArgumentNullException(nameof(accountStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdateAccountStatusCommand by updating an existing account status entity in the database.
        /// </summary>
        /// <param name="request">The UpdateAccountStatusCommand containing the account status data to update.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>An AccountStatusDto representing the updated account status.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the account status to update is not found.</exception>
        public async Task<AccountStatusDto> Handle(UpdateAccountStatusCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Updating account status with ID: {AccountStatusId}", request.Id);

                // Get the existing entity
                var existingAccountStatus = await _accountStatusRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingAccountStatus == null)
                {
                    _logger.LogWarning("Account status with ID {AccountStatusId} not found for update", request.Id);
                    throw new InvalidOperationException($"Account status with ID {request.Id} not found.");
                }

                // Update the entity properties
                existingAccountStatus.Status = request.Status;
                existingAccountStatus.Description = request.Description;
                existingAccountStatus.OrdinalPosition = request.OrdinalPosition;
                existingAccountStatus.ModifiedBy = request.ModifiedBy;
                existingAccountStatus.ModifiedDate = DateTime.UtcNow;

                // Save the updated entity
                var updatedAccountStatus = await _accountStatusRepository.UpdateAsync(existingAccountStatus, cancellationToken);

                _logger.LogInformation("Successfully updated account status with ID: {AccountStatusId}", updatedAccountStatus.AccountStatusId);

                // Map the entity to a DTO for the response
                return _mapper.Map<AccountStatusDto>(updatedAccountStatus);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating account status with ID {AccountStatusId}: {ErrorMessage}",
                    request.Id, ex.Message);
                throw;
            }
        }
    }
}