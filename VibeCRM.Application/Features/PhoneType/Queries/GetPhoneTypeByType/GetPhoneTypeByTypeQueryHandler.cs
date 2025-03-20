using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.PhoneType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.PhoneType.Queries.GetPhoneTypeByType
{
    /// <summary>
    /// Handler for the GetPhoneTypeByTypeQuery.
    /// Retrieves a phone type by its type name.
    /// </summary>
    public class GetPhoneTypeByTypeQueryHandler : IRequestHandler<GetPhoneTypeByTypeQuery, PhoneTypeDto>
    {
        private readonly IPhoneTypeRepository _phoneTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPhoneTypeByTypeQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPhoneTypeByTypeQueryHandler"/> class.
        /// </summary>
        /// <param name="phoneTypeRepository">The phone type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetPhoneTypeByTypeQueryHandler(
            IPhoneTypeRepository phoneTypeRepository,
            IMapper mapper,
            ILogger<GetPhoneTypeByTypeQueryHandler> logger)
        {
            _phoneTypeRepository = phoneTypeRepository ?? throw new ArgumentNullException(nameof(phoneTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetPhoneTypeByTypeQuery by retrieving a phone type by its type name.
        /// </summary>
        /// <param name="request">The query containing the type name of the phone type to retrieve.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The phone type DTO if found, otherwise null.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when no phone type with the specified type name is found.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the retrieval process.</exception>
        public async Task<PhoneTypeDto> Handle(GetPhoneTypeByTypeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving phone type with type name: {TypeName}", request.Type);

                // Get phone types from repository
                var phoneTypes = await _phoneTypeRepository.GetByTypeAsync(request.Type, cancellationToken);
                if (phoneTypes == null || !phoneTypes.Any())
                {
                    _logger.LogError("Phone type with type name {TypeName} not found", request.Type);
                    throw new KeyNotFoundException($"Phone type with type name '{request.Type}' not found");
                }

                // Get the first matching phone type
                var phoneType = phoneTypes.First();

                // Map to DTO
                var phoneTypeDto = _mapper.Map<PhoneTypeDto>(phoneType);

                _logger.LogInformation("Successfully retrieved phone type with type name: {TypeName}", request.Type);

                return phoneTypeDto;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving phone type by type name: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
