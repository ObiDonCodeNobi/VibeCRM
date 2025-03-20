using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.PhoneType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.PhoneType.Commands.UpdatePhoneType
{
    /// <summary>
    /// Handler for the UpdatePhoneTypeCommand.
    /// Updates an existing phone type in the database.
    /// </summary>
    public class UpdatePhoneTypeCommandHandler : IRequestHandler<UpdatePhoneTypeCommand, PhoneTypeDto>
    {
        private readonly IPhoneTypeRepository _phoneTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdatePhoneTypeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatePhoneTypeCommandHandler"/> class.
        /// </summary>
        /// <param name="phoneTypeRepository">The phone type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public UpdatePhoneTypeCommandHandler(
            IPhoneTypeRepository phoneTypeRepository,
            IMapper mapper,
            ILogger<UpdatePhoneTypeCommandHandler> logger)
        {
            _phoneTypeRepository = phoneTypeRepository ?? throw new ArgumentNullException(nameof(phoneTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdatePhoneTypeCommand by updating an existing phone type.
        /// </summary>
        /// <param name="request">The command containing the phone type details to update.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The updated phone type DTO.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the phone type with the specified ID is not found.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the update process.</exception>
        public async Task<PhoneTypeDto> Handle(UpdatePhoneTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Updating phone type with ID: {PhoneTypeId}", request.Id);

                // Get existing phone type
                var existingPhoneType = await _phoneTypeRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingPhoneType == null)
                {
                    _logger.LogError("Phone type with ID {PhoneTypeId} not found", request.Id);
                    throw new KeyNotFoundException($"Phone type with ID {request.Id} not found");
                }

                // Map command to entity, preserving audit fields
                _mapper.Map(request, existingPhoneType);

                // Update audit fields
                existingPhoneType.ModifiedDate = DateTime.UtcNow;
                existingPhoneType.ModifiedBy = Guid.Parse("00000000-0000-0000-0000-000000000000"); // This should be replaced with the actual user ID

                // Update in repository
                var updatedPhoneType = await _phoneTypeRepository.UpdateAsync(existingPhoneType, cancellationToken);

                // Map to DTO
                var phoneTypeDto = _mapper.Map<PhoneTypeDto>(updatedPhoneType);

                _logger.LogInformation("Successfully updated phone type with ID: {PhoneTypeId}", phoneTypeDto.Id);

                return phoneTypeDto;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating phone type: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
