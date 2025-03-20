using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.EmailAddress.Commands.DeleteEmailAddress
{
    /// <summary>
    /// Handler for processing DeleteEmailAddressCommand requests.
    /// Implements IRequestHandler to handle the soft deletion of email addresses.
    /// </summary>
    public class DeleteEmailAddressCommandHandler : IRequestHandler<DeleteEmailAddressCommand, bool>
    {
        private readonly IEmailAddressRepository _emailAddressRepository;
        private readonly ILogger<DeleteEmailAddressCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteEmailAddressCommandHandler"/> class.
        /// </summary>
        /// <param name="emailAddressRepository">The email address repository for data access operations.</param>
        /// <param name="logger">The logger for logging events and errors.</param>
        public DeleteEmailAddressCommandHandler(
            IEmailAddressRepository emailAddressRepository,
            ILogger<DeleteEmailAddressCommandHandler> logger)
        {
            _emailAddressRepository = emailAddressRepository ?? throw new ArgumentNullException(nameof(emailAddressRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeleteEmailAddressCommand request.
        /// </summary>
        /// <param name="request">The DeleteEmailAddressCommand request containing the ID of the email address to delete.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>True if the deletion was successful, otherwise false.</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the email address does not exist.</exception>
        public async Task<bool> Handle(DeleteEmailAddressCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            _logger.LogInformation("Deleting email address with ID {EmailAddressId}", request.Id);

            // Check if email address exists
            var exists = await _emailAddressRepository.ExistsAsync(request.Id, cancellationToken);
            if (!exists)
            {
                _logger.LogWarning("Email address with ID {EmailAddressId} not found", request.Id);
                throw new InvalidOperationException($"Email address with ID '{request.Id}' not found.");
            }

            // Delete from repository (soft delete)
            var result = await _emailAddressRepository.DeleteAsync(request.Id, cancellationToken);

            if (result)
            {
                _logger.LogInformation("Successfully deleted email address with ID {EmailAddressId}", request.Id);
            }
            else
            {
                _logger.LogWarning("Failed to delete email address with ID {EmailAddressId}", request.Id);
            }

            return result;
        }
    }
}