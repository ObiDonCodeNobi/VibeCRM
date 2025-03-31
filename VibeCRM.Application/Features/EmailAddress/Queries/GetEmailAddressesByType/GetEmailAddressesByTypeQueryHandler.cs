using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.EmailAddress;

namespace VibeCRM.Application.Features.EmailAddress.Queries.GetEmailAddressesByType
{
    /// <summary>
    /// Handler for processing GetEmailAddressesByTypeQuery requests.
    /// Implements IRequestHandler to handle the retrieval of email addresses by their type.
    /// </summary>
    public class GetEmailAddressesByTypeQueryHandler : IRequestHandler<GetEmailAddressesByTypeQuery, IEnumerable<EmailAddressListDto>>
    {
        private readonly IEmailAddressRepository _emailAddressRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetEmailAddressesByTypeQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetEmailAddressesByTypeQueryHandler"/> class.
        /// </summary>
        /// <param name="emailAddressRepository">The email address repository for data access operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for logging events and errors.</param>
        public GetEmailAddressesByTypeQueryHandler(
            IEmailAddressRepository emailAddressRepository,
            IMapper mapper,
            ILogger<GetEmailAddressesByTypeQueryHandler> logger)
        {
            _emailAddressRepository = emailAddressRepository ?? throw new ArgumentNullException(nameof(emailAddressRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetEmailAddressesByTypeQuery request.
        /// </summary>
        /// <param name="request">The GetEmailAddressesByTypeQuery request containing the email address type ID.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of EmailAddressListDto objects with the specified type.</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
        public async Task<IEnumerable<EmailAddressListDto>> Handle(GetEmailAddressesByTypeQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            _logger.LogInformation("Retrieving email addresses with type ID {EmailAddressTypeId}", request.EmailAddressTypeId);

            // Get email addresses by type from repository
            var emailAddresses = await _emailAddressRepository.GetByEmailAddressTypeAsync(request.EmailAddressTypeId, cancellationToken);

            // Map to DTOs
            var emailAddressDtos = _mapper.Map<IEnumerable<EmailAddressListDto>>(emailAddresses);

            _logger.LogInformation("Found {Count} email addresses with type ID {EmailAddressTypeId}",
                emailAddressDtos, request.EmailAddressTypeId);

            return emailAddressDtos;
        }
    }
}