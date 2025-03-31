using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.ContactType;

namespace VibeCRM.Application.Features.ContactType.Queries.GetDefaultContactType
{
    /// <summary>
    /// Handler for the GetDefaultContactTypeQuery.
    /// Retrieves the default contact type.
    /// </summary>
    public class GetDefaultContactTypeQueryHandler : IRequestHandler<GetDefaultContactTypeQuery, ContactTypeDto>
    {
        private readonly IContactTypeRepository _contactTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDefaultContactTypeQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetDefaultContactTypeQueryHandler"/> class.
        /// </summary>
        /// <param name="contactTypeRepository">The contact type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetDefaultContactTypeQueryHandler(
            IContactTypeRepository contactTypeRepository,
            IMapper mapper,
            ILogger<GetDefaultContactTypeQueryHandler> logger)
        {
            _contactTypeRepository = contactTypeRepository ?? throw new ArgumentNullException(nameof(contactTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetDefaultContactTypeQuery by retrieving the default contact type.
        /// </summary>
        /// <param name="request">The query to retrieve the default contact type.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The default contact type DTO if found; otherwise, null.</returns>
        public async Task<ContactTypeDto> Handle(GetDefaultContactTypeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving default contact type");

                // Get default contact type from repository
                var defaultContactType = await _contactTypeRepository.GetDefaultAsync(cancellationToken);
                if (defaultContactType == null)
                {
                    _logger.LogWarning("Default contact type not found");
                    return new ContactTypeDto();
                }

                // Map to DTO
                var contactTypeDto = _mapper.Map<ContactTypeDto>(defaultContactType);

                _logger.LogInformation("Successfully retrieved default contact type with ID: {ContactTypeId}", contactTypeDto.Id);

                return contactTypeDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving default contact type: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}