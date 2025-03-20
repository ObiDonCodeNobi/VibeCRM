using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.EmailAddressType.Commands.DeleteEmailAddressType
{
    /// <summary>
    /// Handler for processing DeleteEmailAddressTypeCommand requests.
    /// Soft deletes an email address type by setting its Active property to false.
    /// </summary>
    public class DeleteEmailAddressTypeCommandHandler : IRequestHandler<DeleteEmailAddressTypeCommand, bool>
    {
        private readonly IEmailAddressTypeRepository _emailAddressTypeRepository;
        private readonly ILogger<DeleteEmailAddressTypeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteEmailAddressTypeCommandHandler"/> class.
        /// </summary>
        /// <param name="emailAddressTypeRepository">The email address type repository.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters is null.</exception>
        public DeleteEmailAddressTypeCommandHandler(
            IEmailAddressTypeRepository emailAddressTypeRepository,
            ILogger<DeleteEmailAddressTypeCommandHandler> logger)
        {
            _emailAddressTypeRepository = emailAddressTypeRepository ?? throw new ArgumentNullException(nameof(emailAddressTypeRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeleteEmailAddressTypeCommand request.
        /// </summary>
        /// <param name="request">The request to delete an email address type.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if the email address type was deleted successfully; otherwise, false.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while deleting the email address type.</exception>
        public async Task<bool> Handle(DeleteEmailAddressTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Deleting email address type with ID: {EmailAddressTypeId}", request.Id);

                // Check if the email address type exists
                var exists = await _emailAddressTypeRepository.ExistsAsync(request.Id, cancellationToken);

                if (!exists)
                {
                    _logger.LogWarning("Email address type with ID {EmailAddressTypeId} not found", request.Id);
                    return false;
                }

                // Soft delete in repository (sets Active = false)
                var result = await _emailAddressTypeRepository.DeleteAsync(request.Id, cancellationToken);

                if (result)
                {
                    _logger.LogInformation("Successfully deleted email address type with ID: {EmailAddressTypeId}", request.Id);
                }
                else
                {
                    _logger.LogWarning("Failed to delete email address type with ID: {EmailAddressTypeId}", request.Id);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting email address type with ID: {EmailAddressTypeId}", request.Id);
                throw;
            }
        }
    }
}