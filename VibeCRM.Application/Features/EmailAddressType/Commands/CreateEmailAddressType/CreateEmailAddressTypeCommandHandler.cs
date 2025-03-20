using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.EmailAddressType.Commands.CreateEmailAddressType
{
    /// <summary>
    /// Handler for processing CreateEmailAddressTypeCommand requests.
    /// Creates a new email address type in the repository.
    /// </summary>
    public class CreateEmailAddressTypeCommandHandler : IRequestHandler<CreateEmailAddressTypeCommand, Guid>
    {
        private readonly IEmailAddressTypeRepository _emailAddressTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateEmailAddressTypeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateEmailAddressTypeCommandHandler"/> class.
        /// </summary>
        /// <param name="emailAddressTypeRepository">The email address type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters is null.</exception>
        public CreateEmailAddressTypeCommandHandler(
            IEmailAddressTypeRepository emailAddressTypeRepository,
            IMapper mapper,
            ILogger<CreateEmailAddressTypeCommandHandler> logger)
        {
            _emailAddressTypeRepository = emailAddressTypeRepository ?? throw new ArgumentNullException(nameof(emailAddressTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreateEmailAddressTypeCommand request.
        /// </summary>
        /// <param name="request">The request to create a new email address type.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The ID of the newly created email address type.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while creating the email address type.</exception>
        public async Task<Guid> Handle(CreateEmailAddressTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Creating new email address type with type name: {TypeName}", request.Type);

                // Map command to entity
                var emailAddressType = _mapper.Map<Domain.Entities.TypeStatusEntities.EmailAddressType>(request);

                // Set default values
                emailAddressType.Id = Guid.NewGuid();
                emailAddressType.CreatedDate = DateTime.UtcNow;
                emailAddressType.ModifiedDate = DateTime.UtcNow;
                emailAddressType.Active = true;

                // Add to repository
                var result = await _emailAddressTypeRepository.AddAsync(emailAddressType, cancellationToken);

                _logger.LogInformation("Successfully created email address type with ID: {EmailAddressTypeId}", result.Id);

                return result.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating email address type with type name: {TypeName}", request.Type);
                throw;
            }
        }
    }
}