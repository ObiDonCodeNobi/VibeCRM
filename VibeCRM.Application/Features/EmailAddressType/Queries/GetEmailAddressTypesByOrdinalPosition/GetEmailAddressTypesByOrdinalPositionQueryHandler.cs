using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.EmailAddressType;

namespace VibeCRM.Application.Features.EmailAddressType.Queries.GetEmailAddressTypesByOrdinalPosition
{
    /// <summary>
    /// Handler for processing GetEmailAddressTypesByOrdinalPositionQuery requests.
    /// Retrieves email address types ordered by their ordinal position from the repository and maps them to EmailAddressTypeDto objects.
    /// </summary>
    public class GetEmailAddressTypesByOrdinalPositionQueryHandler : IRequestHandler<GetEmailAddressTypesByOrdinalPositionQuery, IEnumerable<EmailAddressTypeDto>>
    {
        private readonly IEmailAddressTypeRepository _emailAddressTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetEmailAddressTypesByOrdinalPositionQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetEmailAddressTypesByOrdinalPositionQueryHandler"/> class.
        /// </summary>
        /// <param name="emailAddressTypeRepository">The email address type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters is null.</exception>
        public GetEmailAddressTypesByOrdinalPositionQueryHandler(
            IEmailAddressTypeRepository emailAddressTypeRepository,
            IMapper mapper,
            ILogger<GetEmailAddressTypesByOrdinalPositionQueryHandler> logger)
        {
            _emailAddressTypeRepository = emailAddressTypeRepository ?? throw new ArgumentNullException(nameof(emailAddressTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetEmailAddressTypesByOrdinalPositionQuery request.
        /// </summary>
        /// <param name="request">The request to get email address types ordered by ordinal position.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A collection of EmailAddressTypeDto objects ordered by ordinal position.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving email address types.</exception>
        public async Task<IEnumerable<EmailAddressTypeDto>> Handle(GetEmailAddressTypesByOrdinalPositionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving email address types ordered by ordinal position");

                var emailAddressTypes = await _emailAddressTypeRepository.GetByOrdinalPositionAsync(cancellationToken);
                var emailAddressTypeDtos = _mapper.Map<IEnumerable<EmailAddressTypeDto>>(emailAddressTypes);

                _logger.LogInformation("Successfully retrieved {Count} email address types ordered by ordinal position",
                    emailAddressTypeDtos);

                return emailAddressTypeDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving email address types ordered by ordinal position");
                throw;
            }
        }
    }
}