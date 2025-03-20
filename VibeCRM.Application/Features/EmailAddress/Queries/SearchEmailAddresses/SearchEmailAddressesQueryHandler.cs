using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.EmailAddress.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.EmailAddress.Queries.SearchEmailAddresses
{
    /// <summary>
    /// Handler for processing SearchEmailAddressesQuery requests.
    /// Implements IRequestHandler to handle the searching of email addresses by a search term.
    /// </summary>
    public class SearchEmailAddressesQueryHandler : IRequestHandler<SearchEmailAddressesQuery, IEnumerable<EmailAddressListDto>>
    {
        private readonly IEmailAddressRepository _emailAddressRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<SearchEmailAddressesQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchEmailAddressesQueryHandler"/> class.
        /// </summary>
        /// <param name="emailAddressRepository">The email address repository for data access operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for logging events and errors.</param>
        public SearchEmailAddressesQueryHandler(
            IEmailAddressRepository emailAddressRepository,
            IMapper mapper,
            ILogger<SearchEmailAddressesQueryHandler> logger)
        {
            _emailAddressRepository = emailAddressRepository ?? throw new ArgumentNullException(nameof(emailAddressRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the SearchEmailAddressesQuery request.
        /// </summary>
        /// <param name="request">The SearchEmailAddressesQuery request containing the search term.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of EmailAddressListDto objects that match the search term.</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
        public async Task<IEnumerable<EmailAddressListDto>> Handle(SearchEmailAddressesQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            _logger.LogInformation("Searching email addresses with term '{SearchTerm}'", request.SearchTerm);

            // If search term is empty, return all email addresses
            if (string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var allEmailAddresses = await _emailAddressRepository.GetAllAsync(cancellationToken);
                return _mapper.Map<IEnumerable<EmailAddressListDto>>(allEmailAddresses);
            }

            // Check if the search term is a complete email address
            var exactMatch = await _emailAddressRepository.GetByEmailAsync(request.SearchTerm, cancellationToken);
            if (exactMatch != null)
            {
                return new[] { _mapper.Map<EmailAddressListDto>(exactMatch) };
            }

            // Check if the search term is a domain
            if (request.SearchTerm.Contains("@"))
            {
                // Extract domain part if it's an email-like pattern
                var domainPart = request.SearchTerm.Split('@').Last();
                var domainResults = await _emailAddressRepository.GetByDomainAsync(domainPart, cancellationToken);
                return _mapper.Map<IEnumerable<EmailAddressListDto>>(domainResults);
            }
            else if (!request.SearchTerm.Contains("@") && request.SearchTerm.Contains("."))
            {
                // Treat as domain directly
                var domainResults = await _emailAddressRepository.GetByDomainAsync(request.SearchTerm, cancellationToken);
                return _mapper.Map<IEnumerable<EmailAddressListDto>>(domainResults);
            }

            // For other cases, get all and filter in memory
            // This is not ideal for large datasets but works as a fallback
            var allEmails = await _emailAddressRepository.GetAllAsync(cancellationToken);
            var filteredEmails = allEmails.Where(e =>
                e.Address.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase));

            var emailAddressDtos = _mapper.Map<IEnumerable<EmailAddressListDto>>(filteredEmails);

            _logger.LogInformation("Found {Count} email addresses matching search term '{SearchTerm}'",
                emailAddressDtos.Count(), request.SearchTerm);

            return emailAddressDtos;
        }
    }
}