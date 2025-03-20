using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Phone.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Phone.Commands.CreatePhone
{
    /// <summary>
    /// Handler for processing CreatePhoneCommand requests
    /// </summary>
    public class CreatePhoneCommandHandler : IRequestHandler<CreatePhoneCommand, PhoneDto>
    {
        private readonly IPhoneRepository _phoneRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreatePhoneCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePhoneCommandHandler"/> class
        /// </summary>
        /// <param name="phoneRepository">The phone repository for database operations</param>
        /// <param name="mapper">The AutoMapper instance for object mapping</param>
        /// <param name="logger">The logger for recording diagnostic information</param>
        public CreatePhoneCommandHandler(
            IPhoneRepository phoneRepository,
            IMapper mapper,
            ILogger<CreatePhoneCommandHandler> logger)
        {
            _phoneRepository = phoneRepository ?? throw new ArgumentNullException(nameof(phoneRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreatePhoneCommand request
        /// </summary>
        /// <param name="request">The command containing the phone details to create</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A DTO representing the newly created phone</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
        /// <exception cref="InvalidOperationException">Thrown when the phone could not be created</exception>
        public async Task<PhoneDto> Handle(CreatePhoneCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Creating new phone with area code {AreaCode}, prefix {Prefix}, and line number {LineNumber}",
                    request.AreaCode, request.Prefix, request.LineNumber);

                var phoneEntity = _mapper.Map<Domain.Entities.BusinessEntities.Phone>(request);

                // Set audit fields
                phoneEntity.CreatedDate = DateTime.UtcNow;
                phoneEntity.ModifiedDate = DateTime.UtcNow;

                var createdPhone = await _phoneRepository.AddAsync(phoneEntity, cancellationToken);

                _logger.LogInformation("Successfully created phone with ID {PhoneId}", createdPhone.Id);

                return _mapper.Map<PhoneDto>(createdPhone);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating phone with area code {AreaCode}, prefix {Prefix}, and line number {LineNumber}",
                    request.AreaCode, request.Prefix, request.LineNumber);
                throw new InvalidOperationException($"Failed to create phone: {ex.Message}", ex);
            }
        }
    }
}