using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Phone.Commands.DeletePhone
{
    /// <summary>
    /// Handler for processing DeletePhoneCommand requests
    /// </summary>
    public class DeletePhoneCommandHandler : IRequestHandler<DeletePhoneCommand, bool>
    {
        private readonly IPhoneRepository _phoneRepository;
        private readonly ILogger<DeletePhoneCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeletePhoneCommandHandler"/> class
        /// </summary>
        /// <param name="phoneRepository">The phone repository for database operations</param>
        /// <param name="logger">The logger for recording diagnostic information</param>
        public DeletePhoneCommandHandler(
            IPhoneRepository phoneRepository,
            ILogger<DeletePhoneCommandHandler> logger)
        {
            _phoneRepository = phoneRepository ?? throw new ArgumentNullException(nameof(phoneRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeletePhoneCommand request
        /// </summary>
        /// <param name="request">The command containing the ID of the phone to delete</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the phone was successfully deleted, otherwise false</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
        /// <exception cref="InvalidOperationException">Thrown when the phone could not be deleted</exception>
        public async Task<bool> Handle(DeletePhoneCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Deleting phone with ID {PhoneId}", request.Id);

                // Verify the phone exists
                if (!await _phoneRepository.ExistsAsync(request.Id, cancellationToken))
                {
                    _logger.LogWarning("Phone with ID {PhoneId} not found for deletion", request.Id);
                    return false;
                }

                // Delete the phone (soft delete - sets Active = 0)
                bool result = await _phoneRepository.DeleteAsync(request.Id, cancellationToken);

                if (result)
                {
                    _logger.LogInformation("Successfully deleted phone with ID {PhoneId}", request.Id);
                }
                else
                {
                    _logger.LogWarning("Failed to delete phone with ID {PhoneId}", request.Id);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting phone with ID {PhoneId}", request.Id);
                throw new InvalidOperationException($"Failed to delete phone: {ex.Message}", ex);
            }
        }
    }
}