using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.ContactType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.ContactType.Queries.GetAllContactTypes
{
    /// <summary>
    /// Handler for the GetAllContactTypesQuery.
    /// Retrieves all contact types.
    /// </summary>
    public class GetAllContactTypesQueryHandler : IRequestHandler<GetAllContactTypesQuery, IEnumerable<ContactTypeListDto>>
    {
        private readonly IContactTypeRepository _contactTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllContactTypesQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllContactTypesQueryHandler"/> class.
        /// </summary>
        /// <param name="contactTypeRepository">The contact type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetAllContactTypesQueryHandler(
            IContactTypeRepository contactTypeRepository,
            IMapper mapper,
            ILogger<GetAllContactTypesQueryHandler> logger)
        {
            _contactTypeRepository = contactTypeRepository ?? throw new ArgumentNullException(nameof(contactTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllContactTypesQuery by retrieving all contact types.
        /// </summary>
        /// <param name="request">The query to retrieve all contact types.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A collection of contact type list DTOs.</returns>
        public async Task<IEnumerable<ContactTypeListDto>> Handle(GetAllContactTypesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving all contact types");

                // Get all contact types from repository
                var contactTypes = await _contactTypeRepository.GetAllAsync(cancellationToken);

                // Map to DTOs
                var contactTypeDtos = _mapper.Map<IEnumerable<ContactTypeListDto>>(contactTypes);

                // For each DTO, we would typically set the ContactCount property
                // This would be implemented in the repository or a separate service
                // For now, we'll set it to 0 as a placeholder

                _logger.LogInformation("Successfully retrieved all contact types");

                return contactTypeDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all contact types: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}