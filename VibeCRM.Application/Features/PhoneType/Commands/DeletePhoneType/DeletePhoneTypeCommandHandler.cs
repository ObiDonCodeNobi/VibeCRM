using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.PhoneType.Commands.DeletePhoneType
{
    /// <summary>
    /// Handler for the DeletePhoneTypeCommand.
    /// Deletes an existing phone type from the database.
    /// </summary>
    public class DeletePhoneTypeCommandHandler : IRequestHandler<DeletePhoneTypeCommand, bool>
    {
        private readonly IPhoneTypeRepository _phoneTypeRepository;
        private readonly ILogger<DeletePhoneTypeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeletePhoneTypeCommandHandler"/> class.
        /// </summary>
        /// <param name="phoneTypeRepository">The phone type repository.</param>
        /// <param name="logger">The logger.</param>
        public DeletePhoneTypeCommandHandler(
            IPhoneTypeRepository phoneTypeRepository,
            ILogger<DeletePhoneTypeCommandHandler> logger)
        {
            _phoneTypeRepository = phoneTypeRepository ?? throw new ArgumentNullException(nameof(phoneTypeRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeletePhoneTypeCommand by deleting an existing phone type.
        /// </summary>
        /// <param name="request">The command containing the ID of the phone type to delete.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if the phone type was successfully deleted, otherwise false.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the phone type with the specified ID is not found.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the deletion process.</exception>
        public async Task<bool> Handle(DeletePhoneTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Deleting phone type with ID: {PhoneTypeId}", request.Id);

                // Get existing phone type
                var existingPhoneType = await _phoneTypeRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingPhoneType == null)
                {
                    _logger.LogError("Phone type with ID {PhoneTypeId} not found", request.Id);
                    throw new KeyNotFoundException($"Phone type with ID {request.Id} not found");
                }

                // Check if the phone type is in use
                if (existingPhoneType.Phones.Any())
                {
                    _logger.LogWarning("Cannot delete phone type with ID {PhoneTypeId} because it is in use", request.Id);
                    return false;
                }

                // Delete from repository (soft delete)
                await _phoneTypeRepository.DeleteAsync(request.Id, cancellationToken);

                _logger.LogInformation("Successfully deleted phone type with ID: {PhoneTypeId}", request.Id);

                return true;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting phone type: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}