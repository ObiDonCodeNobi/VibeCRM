using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Phone.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.Phone.Queries.GetPhonesByPhoneType
{
    /// <summary>
    /// Handler for processing GetPhonesByPhoneTypeQuery requests
    /// </summary>
    public class GetPhonesByPhoneTypeQueryHandler : IRequestHandler<GetPhonesByPhoneTypeQuery, IEnumerable<PhoneListDto>>
    {
        private readonly IPhoneRepository _phoneRepository;
        private readonly IPhoneTypeRepository _phoneTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPhonesByPhoneTypeQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPhonesByPhoneTypeQueryHandler"/> class
        /// </summary>
        /// <param name="phoneRepository">The phone repository for database operations</param>
        /// <param name="phoneTypeRepository">The phone type repository for verifying phone type existence</param>
        /// <param name="mapper">The AutoMapper instance for object mapping</param>
        /// <param name="logger">The logger for recording diagnostic information</param>
        public GetPhonesByPhoneTypeQueryHandler(
            IPhoneRepository phoneRepository,
            IPhoneTypeRepository phoneTypeRepository,
            IMapper mapper,
            ILogger<GetPhonesByPhoneTypeQueryHandler> logger)
        {
            _phoneRepository = phoneRepository ?? throw new ArgumentNullException(nameof(phoneRepository));
            _phoneTypeRepository = phoneTypeRepository ?? throw new ArgumentNullException(nameof(phoneTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetPhonesByPhoneTypeQuery request
        /// </summary>
        /// <param name="request">The query containing the phone type ID to retrieve phones for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of phone list DTOs of the specified phone type</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
        /// <exception cref="InvalidOperationException">Thrown when the specified phone type does not exist</exception>
        public async Task<IEnumerable<PhoneListDto>> Handle(GetPhonesByPhoneTypeQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Retrieving phones for phone type with ID {PhoneTypeId}", request.PhoneTypeId);

                // Verify the phone type exists
                if (!await _phoneTypeRepository.ExistsAsync(request.PhoneTypeId, cancellationToken))
                {
                    _logger.LogWarning("Phone type with ID {PhoneTypeId} not found", request.PhoneTypeId);
                    throw new InvalidOperationException($"Phone type with ID {request.PhoneTypeId} not found");
                }

                var phones = await _phoneRepository.GetByPhoneTypeAsync(request.PhoneTypeId, cancellationToken);

                _logger.LogInformation("Successfully retrieved phones for phone type with ID {PhoneTypeId}", request.PhoneTypeId);

                return _mapper.Map<IEnumerable<PhoneListDto>>(phones);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving phones for phone type with ID {PhoneTypeId}", request.PhoneTypeId);
                throw;
            }
        }
    }
}