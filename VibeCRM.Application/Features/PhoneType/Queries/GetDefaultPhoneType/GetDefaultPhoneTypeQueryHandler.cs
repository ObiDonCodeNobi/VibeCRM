using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.PhoneType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.PhoneType.Queries.GetDefaultPhoneType
{
    /// <summary>
    /// Handler for the GetDefaultPhoneTypeQuery.
    /// Retrieves the default phone type, which is the one with the lowest ordinal position.
    /// </summary>
    public class GetDefaultPhoneTypeQueryHandler : IRequestHandler<GetDefaultPhoneTypeQuery, PhoneTypeDto>
    {
        private readonly IPhoneTypeRepository _phoneTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDefaultPhoneTypeQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetDefaultPhoneTypeQueryHandler"/> class.
        /// </summary>
        /// <param name="phoneTypeRepository">The phone type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetDefaultPhoneTypeQueryHandler(
            IPhoneTypeRepository phoneTypeRepository,
            IMapper mapper,
            ILogger<GetDefaultPhoneTypeQueryHandler> logger)
        {
            _phoneTypeRepository = phoneTypeRepository ?? throw new ArgumentNullException(nameof(phoneTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetDefaultPhoneTypeQuery by retrieving the default phone type.
        /// </summary>
        /// <param name="request">The query to retrieve the default phone type.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The default phone type DTO if found, otherwise null.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when no phone types are found in the system.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the retrieval process.</exception>
        public async Task<PhoneTypeDto> Handle(GetDefaultPhoneTypeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving default phone type");

                // Get default phone type from repository
                var defaultPhoneType = await _phoneTypeRepository.GetDefaultAsync(cancellationToken);
                
                if (defaultPhoneType == null)
                {
                    _logger.LogError("No default phone type found in the system");
                    throw new KeyNotFoundException("No default phone type found in the system");
                }

                // Map to DTO
                var phoneTypeDto = _mapper.Map<PhoneTypeDto>(defaultPhoneType);

                _logger.LogInformation("Successfully retrieved default phone type with ID: {PhoneTypeId}", phoneTypeDto.Id);

                return phoneTypeDto;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving default phone type: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
