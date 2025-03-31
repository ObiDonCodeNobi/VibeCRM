using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.PhoneType;

namespace VibeCRM.Application.Features.PhoneType.Queries.GetPhoneTypeByOrdinalPosition
{
    /// <summary>
    /// Handler for the GetPhoneTypeByOrdinalPositionQuery.
    /// Retrieves a phone type by its ordinal position.
    /// </summary>
    public class GetPhoneTypeByOrdinalPositionQueryHandler : IRequestHandler<GetPhoneTypeByOrdinalPositionQuery, PhoneTypeDto>
    {
        private readonly IPhoneTypeRepository _phoneTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPhoneTypeByOrdinalPositionQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPhoneTypeByOrdinalPositionQueryHandler"/> class.
        /// </summary>
        /// <param name="phoneTypeRepository">The phone type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetPhoneTypeByOrdinalPositionQueryHandler(
            IPhoneTypeRepository phoneTypeRepository,
            IMapper mapper,
            ILogger<GetPhoneTypeByOrdinalPositionQueryHandler> logger)
        {
            _phoneTypeRepository = phoneTypeRepository ?? throw new ArgumentNullException(nameof(phoneTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetPhoneTypeByOrdinalPositionQuery by retrieving a phone type by its ordinal position.
        /// </summary>
        /// <param name="request">The query containing the ordinal position of the phone type to retrieve.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The phone type DTO if found, otherwise null.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when no phone type with the specified ordinal position is found.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the retrieval process.</exception>
        public async Task<PhoneTypeDto> Handle(GetPhoneTypeByOrdinalPositionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving phone type with ordinal position: {OrdinalPosition}", request.OrdinalPosition);

                // Get all phone types ordered by ordinal position
                var phoneTypes = await _phoneTypeRepository.GetByOrdinalPositionAsync(cancellationToken);

                // Find the phone type with the requested ordinal position
                var phoneType = phoneTypes.FirstOrDefault(pt => pt.OrdinalPosition == request.OrdinalPosition);

                if (phoneType == null)
                {
                    _logger.LogError("Phone type with ordinal position {OrdinalPosition} not found", request.OrdinalPosition);
                    throw new KeyNotFoundException($"Phone type with ordinal position {request.OrdinalPosition} not found");
                }

                // Map to DTO
                var phoneTypeDto = _mapper.Map<PhoneTypeDto>(phoneType);

                _logger.LogInformation("Successfully retrieved phone type with ordinal position: {OrdinalPosition}", request.OrdinalPosition);

                return phoneTypeDto;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving phone type by ordinal position: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}