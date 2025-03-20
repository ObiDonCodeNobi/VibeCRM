using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.EmailAddress.Commands.CreateEmailAddress
{
    /// <summary>
    /// Handler for processing CreateEmailAddressCommand requests.
    /// Implements IRequestHandler to handle the creation of email addresses.
    /// </summary>
    public class CreateEmailAddressCommandHandler : IRequestHandler<CreateEmailAddressCommand, Guid>
    {
        private readonly IEmailAddressRepository _emailAddressRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateEmailAddressCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateEmailAddressCommandHandler"/> class.
        /// </summary>
        /// <param name="emailAddressRepository">The email address repository for data access operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for logging events and errors.</param>
        public CreateEmailAddressCommandHandler(
            IEmailAddressRepository emailAddressRepository,
            IMapper mapper,
            ILogger<CreateEmailAddressCommandHandler> logger)
        {
            _emailAddressRepository = emailAddressRepository ?? throw new ArgumentNullException(nameof(emailAddressRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreateEmailAddressCommand request.
        /// </summary>
        /// <param name="request">The CreateEmailAddressCommand request containing email address data.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>The ID of the created email address.</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the email address already exists.</exception>
        public async Task<Guid> Handle(CreateEmailAddressCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            _logger.LogInformation("Creating new email address with address {EmailAddress}", request.Address);

            // Check if email address already exists
            var isUnique = await _emailAddressRepository.IsEmailUniqueAsync(request.Address, cancellationToken);
            if (!isUnique)
            {
                _logger.LogWarning("Email address {EmailAddress} already exists", request.Address);
                throw new InvalidOperationException($"Email address '{request.Address}' already exists.");
            }

            // Map command to entity
            var emailAddressEntity = _mapper.Map<Domain.Entities.BusinessEntities.EmailAddress>(request);

            // Add to repository
            var result = await _emailAddressRepository.AddAsync(emailAddressEntity, cancellationToken);

            _logger.LogInformation("Successfully created email address with ID {EmailAddressId}", result.EmailAddressId);

            return result.EmailAddressId;
        }
    }
}