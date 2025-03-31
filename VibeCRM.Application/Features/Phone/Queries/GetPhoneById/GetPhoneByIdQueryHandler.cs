using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.Phone;

namespace VibeCRM.Application.Features.Phone.Queries.GetPhoneById
{
    /// <summary>
    /// Handler for processing GetPhoneByIdQuery requests
    /// </summary>
    public class GetPhoneByIdQueryHandler : IRequestHandler<GetPhoneByIdQuery, PhoneDetailsDto>
    {
        private readonly IPhoneRepository _phoneRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPhoneByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPhoneByIdQueryHandler"/> class
        /// </summary>
        /// <param name="phoneRepository">The phone repository for database operations</param>
        /// <param name="mapper">The AutoMapper instance for object mapping</param>
        /// <param name="logger">The logger for recording diagnostic information</param>
        public GetPhoneByIdQueryHandler(
            IPhoneRepository phoneRepository,
            IMapper mapper,
            ILogger<GetPhoneByIdQueryHandler> logger)
        {
            _phoneRepository = phoneRepository ?? throw new ArgumentNullException(nameof(phoneRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetPhoneByIdQuery request
        /// </summary>
        /// <param name="request">The query containing the phone ID to retrieve</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The phone details DTO if found, otherwise null</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
        /// <exception cref="InvalidOperationException">Thrown when an error occurs during retrieval</exception>
        public async Task<PhoneDetailsDto> Handle(GetPhoneByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Getting phone with ID {PhoneId}", request.Id);

                var phone = await _phoneRepository.GetByIdWithDetailsAsync(request.Id, cancellationToken);

                if (phone == null)
                {
                    _logger.LogWarning("Phone with ID {PhoneId} not found", request.Id);
                    return new PhoneDetailsDto();
                }

                var phoneDto = _mapper.Map<PhoneDetailsDto>(phone);

                _logger.LogInformation("Successfully retrieved phone with ID {PhoneId}", request.Id);

                return phoneDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving phone with ID {PhoneId}", request.Id);
                throw new InvalidOperationException($"Failed to retrieve phone: {ex.Message}", ex);
            }
        }
    }
}