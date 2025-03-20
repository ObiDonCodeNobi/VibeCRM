using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.EmailAddressType.Commands.UpdateEmailAddressType
{
    /// <summary>
    /// Handler for processing UpdateEmailAddressTypeCommand requests.
    /// Updates an existing email address type in the repository.
    /// </summary>
    public class UpdateEmailAddressTypeCommandHandler : IRequestHandler<UpdateEmailAddressTypeCommand, bool>
    {
        private readonly IEmailAddressTypeRepository _emailAddressTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateEmailAddressTypeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateEmailAddressTypeCommandHandler"/> class.
        /// </summary>
        /// <param name="emailAddressTypeRepository">The email address type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters is null.</exception>
        public UpdateEmailAddressTypeCommandHandler(
            IEmailAddressTypeRepository emailAddressTypeRepository,
            IMapper mapper,
            ILogger<UpdateEmailAddressTypeCommandHandler> logger)
        {
            _emailAddressTypeRepository = emailAddressTypeRepository ?? throw new ArgumentNullException(nameof(emailAddressTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdateEmailAddressTypeCommand request.
        /// </summary>
        /// <param name="request">The request to update an email address type.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if the email address type was updated successfully; otherwise, false.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while updating the email address type.</exception>
        public async Task<bool> Handle(UpdateEmailAddressTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Updating email address type with ID: {EmailAddressTypeId}", request.Id);

                // Check if the email address type exists
                var existingEmailAddressType = await _emailAddressTypeRepository.GetByIdAsync(request.Id, cancellationToken);

                if (existingEmailAddressType == null)
                {
                    _logger.LogWarning("Email address type with ID {EmailAddressTypeId} not found", request.Id);
                    return false;
                }

                // Update properties
                existingEmailAddressType.Type = request.Type;
                existingEmailAddressType.Description = request.Description;
                existingEmailAddressType.OrdinalPosition = request.OrdinalPosition;
                existingEmailAddressType.ModifiedDate = DateTime.UtcNow;

                // Update in repository
                await _emailAddressTypeRepository.UpdateAsync(existingEmailAddressType, cancellationToken);

                _logger.LogInformation("Successfully updated email address type with ID: {EmailAddressTypeId}", request.Id);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating email address type with ID: {EmailAddressTypeId}", request.Id);
                throw;
            }
        }
    }
}