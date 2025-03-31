using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.ContactType;

namespace VibeCRM.Application.Features.ContactType.Commands.UpdateContactType
{
    /// <summary>
    /// Handler for the UpdateContactTypeCommand.
    /// Updates an existing contact type in the database.
    /// </summary>
    public class UpdateContactTypeCommandHandler : IRequestHandler<UpdateContactTypeCommand, ContactTypeDto>
    {
        private readonly IContactTypeRepository _contactTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateContactTypeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateContactTypeCommandHandler"/> class.
        /// </summary>
        /// <param name="contactTypeRepository">The contact type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public UpdateContactTypeCommandHandler(
            IContactTypeRepository contactTypeRepository,
            IMapper mapper,
            ILogger<UpdateContactTypeCommandHandler> logger)
        {
            _contactTypeRepository = contactTypeRepository ?? throw new ArgumentNullException(nameof(contactTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdateContactTypeCommand by updating an existing contact type.
        /// </summary>
        /// <param name="request">The command containing the contact type details to update.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The updated contact type DTO.</returns>
        public async Task<ContactTypeDto> Handle(UpdateContactTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Updating contact type with ID: {ContactTypeId}", request.Id);

                // Get existing entity
                var existingContactType = await _contactTypeRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingContactType == null)
                {
                    _logger.LogWarning("Contact type with ID {ContactTypeId} not found", request.Id);
                    throw new Exception($"Contact type with ID {request.Id} not found");
                }

                // Update properties
                existingContactType.Type = request.Type;
                existingContactType.Description = request.Description;
                existingContactType.OrdinalPosition = request.OrdinalPosition;
                existingContactType.ModifiedDate = DateTime.UtcNow;
                existingContactType.ModifiedBy = Guid.Parse("00000000-0000-0000-0000-000000000000"); // This should be replaced with the actual user ID

                // Update in repository
                var updatedContactType = await _contactTypeRepository.UpdateAsync(existingContactType, cancellationToken);

                // Map to DTO
                var contactTypeDto = _mapper.Map<ContactTypeDto>(updatedContactType);

                _logger.LogInformation("Successfully updated contact type with ID: {ContactTypeId}", contactTypeDto.Id);

                return contactTypeDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating contact type with ID {ContactTypeId}: {ErrorMessage}", request.Id, ex.Message);
                throw;
            }
        }
    }
}