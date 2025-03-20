using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Phone.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Phone.Commands.UpdatePhone
{
    /// <summary>
    /// Handler for processing UpdatePhoneCommand requests
    /// </summary>
    public class UpdatePhoneCommandHandler : IRequestHandler<UpdatePhoneCommand, PhoneDto>
    {
        private readonly IPhoneRepository _phoneRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdatePhoneCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatePhoneCommandHandler"/> class
        /// </summary>
        /// <param name="phoneRepository">The phone repository for database operations</param>
        /// <param name="mapper">The AutoMapper instance for object mapping</param>
        /// <param name="logger">The logger for recording diagnostic information</param>
        public UpdatePhoneCommandHandler(
            IPhoneRepository phoneRepository,
            IMapper mapper,
            ILogger<UpdatePhoneCommandHandler> logger)
        {
            _phoneRepository = phoneRepository ?? throw new ArgumentNullException(nameof(phoneRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdatePhoneCommand request
        /// </summary>
        /// <param name="request">The command containing the updated phone details</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A DTO representing the updated phone</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
        /// <exception cref="InvalidOperationException">Thrown when the phone could not be updated</exception>
        public async Task<PhoneDto> Handle(UpdatePhoneCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Updating phone with ID {PhoneId}", request.Id);

                // Get the existing phone
                var existingPhone = await _phoneRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingPhone == null)
                {
                    throw new InvalidOperationException($"Phone with ID {request.Id} not found");
                }

                // Update the phone properties
                _mapper.Map(request, existingPhone);

                // Update audit fields
                existingPhone.ModifiedDate = DateTime.UtcNow;

                var updatedPhone = await _phoneRepository.UpdateAsync(existingPhone, cancellationToken);

                _logger.LogInformation("Successfully updated phone with ID {PhoneId}", updatedPhone.Id);

                return _mapper.Map<PhoneDto>(updatedPhone);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating phone with ID {PhoneId}", request.Id);
                throw new InvalidOperationException($"Failed to update phone: {ex.Message}", ex);
            }
        }
    }
}