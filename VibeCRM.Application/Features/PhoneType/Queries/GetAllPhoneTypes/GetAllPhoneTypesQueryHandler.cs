using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.PhoneType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.PhoneType.Queries.GetAllPhoneTypes
{
    /// <summary>
    /// Handler for the GetAllPhoneTypesQuery.
    /// Retrieves all phone types.
    /// </summary>
    public class GetAllPhoneTypesQueryHandler : IRequestHandler<GetAllPhoneTypesQuery, IEnumerable<PhoneTypeListDto>>
    {
        private readonly IPhoneTypeRepository _phoneTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllPhoneTypesQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllPhoneTypesQueryHandler"/> class.
        /// </summary>
        /// <param name="phoneTypeRepository">The phone type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetAllPhoneTypesQueryHandler(
            IPhoneTypeRepository phoneTypeRepository,
            IMapper mapper,
            ILogger<GetAllPhoneTypesQueryHandler> logger)
        {
            _phoneTypeRepository = phoneTypeRepository ?? throw new ArgumentNullException(nameof(phoneTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllPhoneTypesQuery by retrieving all phone types.
        /// </summary>
        /// <param name="request">The query to retrieve all phone types.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A collection of phone type list DTOs.</returns>
        /// <exception cref="Exception">Thrown when an error occurs during the retrieval process.</exception>
        public async Task<IEnumerable<PhoneTypeListDto>> Handle(GetAllPhoneTypesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving all phone types");

                // Get all phone types from repository
                var phoneTypes = await _phoneTypeRepository.GetAllAsync(cancellationToken);

                // Map to DTOs
                var phoneTypeDtos = _mapper.Map<IEnumerable<PhoneTypeListDto>>(phoneTypes);

                // For each DTO, we would typically set the PhoneCount property
                // This would be implemented in the repository or a separate service
                // For now, we'll set it to the count of phones in the entity

                foreach (var dto in phoneTypeDtos)
                {
                    var entity = phoneTypes.FirstOrDefault(pt => pt.Id == dto.Id);
                    if (entity != null)
                    {
                        dto.PhoneCount = entity.Phones?.Count ?? 0;
                    }
                }

                _logger.LogInformation("Successfully retrieved all phone types");

                return phoneTypeDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all phone types: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}