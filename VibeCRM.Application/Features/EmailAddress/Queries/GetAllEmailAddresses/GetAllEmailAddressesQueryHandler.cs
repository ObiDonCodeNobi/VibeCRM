using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.EmailAddress;

namespace VibeCRM.Application.Features.EmailAddress.Queries.GetAllEmailAddresses
{
    /// <summary>
    /// Handler for processing GetAllEmailAddressesQuery requests.
    /// Implements IRequestHandler to handle the retrieval of all email addresses.
    /// </summary>
    public class GetAllEmailAddressesQueryHandler : IRequestHandler<GetAllEmailAddressesQuery, IEnumerable<EmailAddressListDto>>
    {
        private readonly IEmailAddressRepository _emailAddressRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllEmailAddressesQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllEmailAddressesQueryHandler"/> class.
        /// </summary>
        /// <param name="emailAddressRepository">The email address repository for data access operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for logging events and errors.</param>
        public GetAllEmailAddressesQueryHandler(
            IEmailAddressRepository emailAddressRepository,
            IMapper mapper,
            ILogger<GetAllEmailAddressesQueryHandler> logger)
        {
            _emailAddressRepository = emailAddressRepository ?? throw new ArgumentNullException(nameof(emailAddressRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllEmailAddressesQuery request.
        /// </summary>
        /// <param name="request">The GetAllEmailAddressesQuery request.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of EmailAddressListDto objects representing all email addresses.</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
        public async Task<IEnumerable<EmailAddressListDto>> Handle(GetAllEmailAddressesQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            _logger.LogInformation("Retrieving all email addresses");

            // Get all email addresses from repository
            var emailAddresses = await _emailAddressRepository.GetAllAsync(cancellationToken);

            // Map to DTOs
            var emailAddressDtos = _mapper.Map<IEnumerable<EmailAddressListDto>>(emailAddresses);

            _logger.LogInformation("Successfully retrieved {Count} email addresses", emailAddressDtos);

            return emailAddressDtos;
        }
    }
}