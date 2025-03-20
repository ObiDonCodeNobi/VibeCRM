using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.EmailAddress.Commands.UpdateEmailAddress
{
    /// <summary>
    /// Handler for processing UpdateEmailAddressCommand requests.
    /// Implements IRequestHandler to handle the updating of email addresses.
    /// </summary>
    public class UpdateEmailAddressCommandHandler : IRequestHandler<UpdateEmailAddressCommand, bool>
    {
        private readonly IEmailAddressRepository _emailAddressRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateEmailAddressCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateEmailAddressCommandHandler"/> class.
        /// </summary>
        /// <param name="emailAddressRepository">The email address repository for data access operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for logging events and errors.</param>
        public UpdateEmailAddressCommandHandler(
            IEmailAddressRepository emailAddressRepository,
            IMapper mapper,
            ILogger<UpdateEmailAddressCommandHandler> logger)
        {
            _emailAddressRepository = emailAddressRepository ?? throw new ArgumentNullException(nameof(emailAddressRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdateEmailAddressCommand request.
        /// </summary>
        /// <param name="request">The UpdateEmailAddressCommand request containing updated email address data.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>True if the update was successful, otherwise false.</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the email address does not exist or when trying to update to an email that already exists.</exception>
        public async Task<bool> Handle(UpdateEmailAddressCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            _logger.LogInformation("Updating email address with ID {EmailAddressId}", request.Id);

            // Check if email address exists
            var existingEmailAddress = await _emailAddressRepository.GetByIdAsync(request.Id, cancellationToken);
            if (existingEmailAddress == null)
            {
                _logger.LogWarning("Email address with ID {EmailAddressId} not found", request.Id);
                throw new InvalidOperationException($"Email address with ID '{request.Id}' not found.");
            }

            // If email address is being changed, check if the new one is unique
            if (!string.Equals(existingEmailAddress.Address, request.Address, StringComparison.OrdinalIgnoreCase))
            {
                var isUnique = await _emailAddressRepository.IsEmailUniqueAsync(request.Address, cancellationToken);
                if (!isUnique)
                {
                    _logger.LogWarning("Email address {EmailAddress} already exists", request.Address);
                    throw new InvalidOperationException($"Email address '{request.Address}' already exists.");
                }
            }

            // Map command to entity, preserving creation information
            var emailAddressEntity = _mapper.Map<UpdateEmailAddressCommand, Domain.Entities.BusinessEntities.EmailAddress>(request);
            emailAddressEntity.CreatedBy = existingEmailAddress.CreatedBy;
            emailAddressEntity.CreatedDate = existingEmailAddress.CreatedDate;

            // Update in repository
            var result = await _emailAddressRepository.UpdateAsync(emailAddressEntity, cancellationToken);

            _logger.LogInformation("Successfully updated email address with ID {EmailAddressId}", result.EmailAddressId);

            return true;
        }
    }
}