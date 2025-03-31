using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.EmailAddressType;

namespace VibeCRM.Application.Features.EmailAddressType.Queries.GetDefaultEmailAddressType
{
    /// <summary>
    /// Handler for processing GetDefaultEmailAddressTypeQuery requests.
    /// Retrieves the default email address type from the repository and maps it to an EmailAddressTypeDto object.
    /// </summary>
    public class GetDefaultEmailAddressTypeQueryHandler : IRequestHandler<GetDefaultEmailAddressTypeQuery, EmailAddressTypeDto>
    {
        private readonly IEmailAddressTypeRepository _emailAddressTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDefaultEmailAddressTypeQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetDefaultEmailAddressTypeQueryHandler"/> class.
        /// </summary>
        /// <param name="emailAddressTypeRepository">The email address type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters is null.</exception>
        public GetDefaultEmailAddressTypeQueryHandler(
            IEmailAddressTypeRepository emailAddressTypeRepository,
            IMapper mapper,
            ILogger<GetDefaultEmailAddressTypeQueryHandler> logger)
        {
            _emailAddressTypeRepository = emailAddressTypeRepository ?? throw new ArgumentNullException(nameof(emailAddressTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetDefaultEmailAddressTypeQuery request.
        /// </summary>
        /// <param name="request">The request to get the default email address type.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>An EmailAddressTypeDto object representing the default email address type if found; otherwise, null.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving the default email address type.</exception>
        public async Task<EmailAddressTypeDto> Handle(GetDefaultEmailAddressTypeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving default email address type");

                var defaultEmailAddressType = await _emailAddressTypeRepository.GetDefaultAsync(cancellationToken);

                if (defaultEmailAddressType == null)
                {
                    _logger.LogWarning("No default email address type found");
                    return new EmailAddressTypeDto();
                }

                var emailAddressTypeDto = _mapper.Map<EmailAddressTypeDto>(defaultEmailAddressType);

                _logger.LogInformation("Successfully retrieved default email address type with ID: {EmailAddressTypeId}",
                    emailAddressTypeDto.Id);

                return emailAddressTypeDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving default email address type");
                throw;
            }
        }
    }
}