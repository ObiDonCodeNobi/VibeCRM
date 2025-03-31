using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.EmailAddressType;

namespace VibeCRM.Application.Features.EmailAddressType.Queries.GetEmailAddressTypeById
{
    /// <summary>
    /// Handler for processing GetEmailAddressTypeByIdQuery requests.
    /// Retrieves an email address type by its ID from the repository and maps it to an EmailAddressTypeDetailsDto object.
    /// </summary>
    public class GetEmailAddressTypeByIdQueryHandler : IRequestHandler<GetEmailAddressTypeByIdQuery, EmailAddressTypeDetailsDto>
    {
        private readonly IEmailAddressTypeRepository _emailAddressTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetEmailAddressTypeByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetEmailAddressTypeByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="emailAddressTypeRepository">The email address type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters is null.</exception>
        public GetEmailAddressTypeByIdQueryHandler(
            IEmailAddressTypeRepository emailAddressTypeRepository,
            IMapper mapper,
            ILogger<GetEmailAddressTypeByIdQueryHandler> logger)
        {
            _emailAddressTypeRepository = emailAddressTypeRepository ?? throw new ArgumentNullException(nameof(emailAddressTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetEmailAddressTypeByIdQuery request.
        /// </summary>
        /// <param name="request">The request to get an email address type by ID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>An EmailAddressTypeDetailsDto object if found; otherwise, null.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving the email address type.</exception>
        public async Task<EmailAddressTypeDetailsDto> Handle(GetEmailAddressTypeByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving email address type with ID: {EmailAddressTypeId}", request.Id);

                var emailAddressType = await _emailAddressTypeRepository.GetByIdAsync(request.Id, cancellationToken);

                if (emailAddressType == null)
                {
                    _logger.LogWarning("Email address type with ID {EmailAddressTypeId} not found", request.Id);
                    return new EmailAddressTypeDetailsDto();
                }

                var emailAddressTypeDto = _mapper.Map<EmailAddressTypeDetailsDto>(emailAddressType);

                // Get count of email addresses for this type (this would need to be implemented in the repository)
                // emailAddressTypeDto.EmailAddressCount = await _emailAddressTypeRepository.GetEmailAddressCountAsync(request.Id, cancellationToken);

                _logger.LogInformation("Successfully retrieved email address type with ID: {EmailAddressTypeId}", request.Id);

                return emailAddressTypeDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving email address type with ID: {EmailAddressTypeId}", request.Id);
                throw;
            }
        }
    }
}