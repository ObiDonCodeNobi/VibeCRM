using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.ContactType;

namespace VibeCRM.Application.Features.ContactType.Queries.GetContactTypeById
{
    /// <summary>
    /// Handler for the GetContactTypeByIdQuery.
    /// Retrieves a contact type by its unique identifier.
    /// </summary>
    public class GetContactTypeByIdQueryHandler : IRequestHandler<GetContactTypeByIdQuery, ContactTypeDetailsDto>
    {
        private readonly IContactTypeRepository _contactTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetContactTypeByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetContactTypeByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="contactTypeRepository">The contact type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetContactTypeByIdQueryHandler(
            IContactTypeRepository contactTypeRepository,
            IMapper mapper,
            ILogger<GetContactTypeByIdQueryHandler> logger)
        {
            _contactTypeRepository = contactTypeRepository ?? throw new ArgumentNullException(nameof(contactTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetContactTypeByIdQuery by retrieving a contact type by its ID.
        /// </summary>
        /// <param name="request">The query containing the ID of the contact type to retrieve.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The contact type details DTO if found; otherwise, null.</returns>
        public async Task<ContactTypeDetailsDto> Handle(GetContactTypeByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving contact type with ID: {ContactTypeId}", request.Id);

                // Get contact type from repository
                var contactType = await _contactTypeRepository.GetByIdAsync(request.Id, cancellationToken);
                if (contactType == null)
                {
                    _logger.LogWarning("Contact type with ID {ContactTypeId} not found", request.Id);
                    return new ContactTypeDetailsDto();
                }

                // Map to DTO
                var contactTypeDto = _mapper.Map<ContactTypeDetailsDto>(contactType);

                // Get contact count (this would typically be implemented in the repository)
                // For now, we'll set it to 0 as a placeholder
                contactTypeDto.ContactCount = 0;

                _logger.LogInformation("Successfully retrieved contact type with ID: {ContactTypeId}", request.Id);

                return contactTypeDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving contact type with ID {ContactTypeId}: {ErrorMessage}", request.Id, ex.Message);
                throw;
            }
        }
    }
}