using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.Phone;

namespace VibeCRM.Application.Features.Phone.Queries.SearchPhonesByNumber
{
    /// <summary>
    /// Handler for processing SearchPhonesByNumberQuery requests
    /// </summary>
    public class SearchPhonesByNumberQueryHandler : IRequestHandler<SearchPhonesByNumberQuery, IEnumerable<PhoneListDto>>
    {
        private readonly IPhoneRepository _phoneRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<SearchPhonesByNumberQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchPhonesByNumberQueryHandler"/> class
        /// </summary>
        /// <param name="phoneRepository">The phone repository for database operations</param>
        /// <param name="mapper">The AutoMapper instance for object mapping</param>
        /// <param name="logger">The logger for recording diagnostic information</param>
        public SearchPhonesByNumberQueryHandler(
            IPhoneRepository phoneRepository,
            IMapper mapper,
            ILogger<SearchPhonesByNumberQueryHandler> logger)
        {
            _phoneRepository = phoneRepository ?? throw new ArgumentNullException(nameof(phoneRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the SearchPhonesByNumberQuery request
        /// </summary>
        /// <param name="request">The query containing the search term to match against phone numbers</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of phone list DTOs that match the search criteria</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
        public async Task<IEnumerable<PhoneListDto>> Handle(SearchPhonesByNumberQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Searching for phones with number containing '{SearchTerm}'", request.SearchTerm);

                if (string.IsNullOrWhiteSpace(request.SearchTerm))
                {
                    _logger.LogWarning("Search term is empty or whitespace");
                    return new List<PhoneListDto>();
                }

                var phones = await _phoneRepository.SearchByPhoneNumberAsync(request.SearchTerm, cancellationToken);

                _logger.LogInformation("Found {Count} phones matching search term '{SearchTerm}'",
                    phones is ICollection<Domain.Entities.BusinessEntities.Phone> collection ? collection.Count : 0,
                    request.SearchTerm);

                return _mapper.Map<IEnumerable<PhoneListDto>>(phones);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching for phones with number containing '{SearchTerm}'", request.SearchTerm);
                throw;
            }
        }
    }
}