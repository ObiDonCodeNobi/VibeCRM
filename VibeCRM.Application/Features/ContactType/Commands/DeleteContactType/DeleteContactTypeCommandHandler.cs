using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.ContactType.Commands.DeleteContactType
{
    /// <summary>
    /// Handler for the DeleteContactTypeCommand.
    /// Performs a soft delete on a contact type by setting Active = false.
    /// </summary>
    public class DeleteContactTypeCommandHandler : IRequestHandler<DeleteContactTypeCommand, bool>
    {
        private readonly IContactTypeRepository _contactTypeRepository;
        private readonly ILogger<DeleteContactTypeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteContactTypeCommandHandler"/> class.
        /// </summary>
        /// <param name="contactTypeRepository">The contact type repository.</param>
        /// <param name="logger">The logger.</param>
        public DeleteContactTypeCommandHandler(
            IContactTypeRepository contactTypeRepository,
            ILogger<DeleteContactTypeCommandHandler> logger)
        {
            _contactTypeRepository = contactTypeRepository ?? throw new ArgumentNullException(nameof(contactTypeRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeleteContactTypeCommand by performing a soft delete on a contact type.
        /// </summary>
        /// <param name="request">The command containing the ID of the contact type to delete.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if the contact type was successfully deleted; otherwise, false.</returns>
        public async Task<bool> Handle(DeleteContactTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Soft deleting contact type with ID: {ContactTypeId}", request.Id);

                // Check if contact type exists
                var contactType = await _contactTypeRepository.GetByIdAsync(request.Id, cancellationToken);
                if (contactType == null)
                {
                    _logger.LogWarning("Contact type with ID {ContactTypeId} not found", request.Id);
                    return false;
                }

                // Perform soft delete by setting Active = false
                var result = await _contactTypeRepository.DeleteAsync(request.Id, cancellationToken);

                if (result)
                {
                    _logger.LogInformation("Successfully soft deleted contact type with ID: {ContactTypeId}", request.Id);
                }
                else
                {
                    _logger.LogWarning("Failed to soft delete contact type with ID: {ContactTypeId}", request.Id);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error soft deleting contact type with ID {ContactTypeId}: {ErrorMessage}", request.Id, ex.Message);
                throw;
            }
        }
    }
}