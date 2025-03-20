using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.EmailAddress.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.EmailAddress.Queries.GetEmailAddressById
{
    /// <summary>
    /// Handler for processing GetEmailAddressByIdQuery requests.
    /// Implements IRequestHandler to handle the retrieval of an email address by its ID.
    /// </summary>
    public class GetEmailAddressByIdQueryHandler : IRequestHandler<GetEmailAddressByIdQuery, EmailAddressDetailsDto>
    {
        private readonly IEmailAddressRepository _emailAddressRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetEmailAddressByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetEmailAddressByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="emailAddressRepository">The email address repository for data access operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for logging events and errors.</param>
        public GetEmailAddressByIdQueryHandler(
            IEmailAddressRepository emailAddressRepository,
            IMapper mapper,
            ILogger<GetEmailAddressByIdQueryHandler> logger)
        {
            _emailAddressRepository = emailAddressRepository ?? throw new ArgumentNullException(nameof(emailAddressRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetEmailAddressByIdQuery request.
        /// </summary>
        /// <param name="request">The GetEmailAddressByIdQuery request containing the ID of the email address to retrieve.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>An EmailAddressDetailsDto object representing the requested email address, or null if not found.</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
        public async Task<EmailAddressDetailsDto> Handle(GetEmailAddressByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            _logger.LogInformation("Retrieving email address with ID {EmailAddressId}", request.Id);

            // Get email address from repository
            var emailAddress = await _emailAddressRepository.GetByIdAsync(request.Id, cancellationToken);

            if (emailAddress == null)
            {
                _logger.LogWarning("Email address with ID {EmailAddressId} not found", request.Id);
                return new EmailAddressDetailsDto();
            }

            // Map to DTO
            var emailAddressDto = _mapper.Map<EmailAddressDetailsDto>(emailAddress);

            _logger.LogInformation("Successfully retrieved email address with ID {EmailAddressId}", request.Id);

            return emailAddressDto;
        }
    }
}