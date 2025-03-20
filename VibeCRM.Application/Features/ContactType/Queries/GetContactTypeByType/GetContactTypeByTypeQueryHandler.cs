using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.ContactType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.ContactType.Queries.GetContactTypeByType
{
    /// <summary>
    /// Handler for the GetContactTypeByTypeQuery.
    /// Retrieves a contact type by its type name.
    /// </summary>
    public class GetContactTypeByTypeQueryHandler : IRequestHandler<GetContactTypeByTypeQuery, ContactTypeDetailsDto>
    {
        private readonly IContactTypeRepository _contactTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetContactTypeByTypeQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetContactTypeByTypeQueryHandler"/> class.
        /// </summary>
        /// <param name="contactTypeRepository">The contact type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetContactTypeByTypeQueryHandler(
            IContactTypeRepository contactTypeRepository,
            IMapper mapper,
            ILogger<GetContactTypeByTypeQueryHandler> logger)
        {
            _contactTypeRepository = contactTypeRepository ?? throw new ArgumentNullException(nameof(contactTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetContactTypeByTypeQuery by retrieving a contact type by its type name.
        /// </summary>
        /// <param name="request">The query containing the type name of the contact type to retrieve.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The contact type details DTO if found; otherwise, null.</returns>
        public async Task<ContactTypeDetailsDto> Handle(GetContactTypeByTypeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving contact type with type name: {ContactTypeName}", request.Type);

                // Get contact types from repository by type name
                var contactTypes = await _contactTypeRepository.GetByTypeAsync(request.Type, cancellationToken);
                var contactType = contactTypes.FirstOrDefault();

                if (contactType == null)
                {
                    _logger.LogWarning("Contact type with type name {ContactTypeName} not found", request.Type);
                    return new ContactTypeDetailsDto();
                }

                // Map to DTO
                var contactTypeDto = _mapper.Map<ContactTypeDetailsDto>(contactType);

                // Get contact count (this would typically be implemented in the repository)
                // For now, we'll set it to 0 as a placeholder
                contactTypeDto.ContactCount = 0;

                _logger.LogInformation("Successfully retrieved contact type with type name: {ContactTypeName}", request.Type);

                return contactTypeDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving contact type with type name {ContactTypeName}: {ErrorMessage}", request.Type, ex.Message);
                throw;
            }
        }
    }
}