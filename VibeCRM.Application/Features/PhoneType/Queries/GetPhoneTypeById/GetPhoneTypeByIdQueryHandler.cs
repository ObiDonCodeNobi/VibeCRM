using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.PhoneType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.PhoneType.Queries.GetPhoneTypeById
{
    /// <summary>
    /// Handler for the GetPhoneTypeByIdQuery.
    /// Retrieves a phone type by its ID.
    /// </summary>
    public class GetPhoneTypeByIdQueryHandler : IRequestHandler<GetPhoneTypeByIdQuery, PhoneTypeDetailsDto>
    {
        private readonly IPhoneTypeRepository _phoneTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPhoneTypeByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPhoneTypeByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="phoneTypeRepository">The phone type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetPhoneTypeByIdQueryHandler(
            IPhoneTypeRepository phoneTypeRepository,
            IMapper mapper,
            ILogger<GetPhoneTypeByIdQueryHandler> logger)
        {
            _phoneTypeRepository = phoneTypeRepository ?? throw new ArgumentNullException(nameof(phoneTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetPhoneTypeByIdQuery by retrieving a phone type by its ID.
        /// </summary>
        /// <param name="request">The query containing the ID of the phone type to retrieve.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The phone type details DTO if found, otherwise null.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the phone type with the specified ID is not found.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the retrieval process.</exception>
        public async Task<PhoneTypeDetailsDto> Handle(GetPhoneTypeByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving phone type with ID: {PhoneTypeId}", request.Id);

                // Get phone type from repository
                var phoneType = await _phoneTypeRepository.GetByIdAsync(request.Id, cancellationToken);
                if (phoneType == null)
                {
                    _logger.LogError("Phone type with ID {PhoneTypeId} not found", request.Id);
                    throw new KeyNotFoundException($"Phone type with ID {request.Id} not found");
                }

                // Map to DTO
                var phoneTypeDto = _mapper.Map<PhoneTypeDetailsDto>(phoneType);

                // Set the PhoneCount property
                phoneTypeDto.PhoneCount = phoneType.Phones?.Count ?? 0;

                _logger.LogInformation("Successfully retrieved phone type with ID: {PhoneTypeId}", request.Id);

                return phoneTypeDto;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving phone type: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}