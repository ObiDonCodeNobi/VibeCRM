using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.EmailAddressType;

namespace VibeCRM.Application.Features.EmailAddressType.Queries.GetEmailAddressTypeByType
{
    /// <summary>
    /// Handler for processing GetEmailAddressTypeByTypeQuery requests.
    /// Retrieves email address types by their type name from the repository and maps them to EmailAddressTypeDto objects.
    /// </summary>
    public class GetEmailAddressTypeByTypeQueryHandler : IRequestHandler<GetEmailAddressTypeByTypeQuery, IEnumerable<EmailAddressTypeDto>>
    {
        private readonly IEmailAddressTypeRepository _emailAddressTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetEmailAddressTypeByTypeQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetEmailAddressTypeByTypeQueryHandler"/> class.
        /// </summary>
        /// <param name="emailAddressTypeRepository">The email address type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters is null.</exception>
        public GetEmailAddressTypeByTypeQueryHandler(
            IEmailAddressTypeRepository emailAddressTypeRepository,
            IMapper mapper,
            ILogger<GetEmailAddressTypeByTypeQueryHandler> logger)
        {
            _emailAddressTypeRepository = emailAddressTypeRepository ?? throw new ArgumentNullException(nameof(emailAddressTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetEmailAddressTypeByTypeQuery request.
        /// </summary>
        /// <param name="request">The request to get email address types by type name.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A collection of EmailAddressTypeDto objects that match the type name.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving email address types.</exception>
        public async Task<IEnumerable<EmailAddressTypeDto>> Handle(GetEmailAddressTypeByTypeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving email address types with type name: {TypeName}", request.Type);

                var emailAddressTypes = await _emailAddressTypeRepository.GetByTypeAsync(request.Type, cancellationToken);
                var emailAddressTypeDtos = _mapper.Map<IEnumerable<EmailAddressTypeDto>>(emailAddressTypes);

                _logger.LogInformation("Successfully retrieved {Count} email address types with type name: {TypeName}",
                    emailAddressTypeDtos, request.Type);

                return emailAddressTypeDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving email address types with type name: {TypeName}", request.Type);
                throw;
            }
        }
    }
}