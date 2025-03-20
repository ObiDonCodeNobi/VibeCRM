using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.EmailAddressType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.EmailAddressType.Queries.GetAllEmailAddressTypes
{
    /// <summary>
    /// Handler for processing GetAllEmailAddressTypesQuery requests.
    /// Retrieves all active email address types from the repository and maps them to EmailAddressTypeListDto objects.
    /// </summary>
    public class GetAllEmailAddressTypesQueryHandler : IRequestHandler<GetAllEmailAddressTypesQuery, IEnumerable<EmailAddressTypeListDto>>
    {
        private readonly IEmailAddressTypeRepository _emailAddressTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllEmailAddressTypesQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllEmailAddressTypesQueryHandler"/> class.
        /// </summary>
        /// <param name="emailAddressTypeRepository">The email address type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters is null.</exception>
        public GetAllEmailAddressTypesQueryHandler(
            IEmailAddressTypeRepository emailAddressTypeRepository,
            IMapper mapper,
            ILogger<GetAllEmailAddressTypesQueryHandler> logger)
        {
            _emailAddressTypeRepository = emailAddressTypeRepository ?? throw new ArgumentNullException(nameof(emailAddressTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllEmailAddressTypesQuery request.
        /// </summary>
        /// <param name="request">The request to get all email address types.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A collection of EmailAddressTypeListDto objects.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving email address types.</exception>
        public async Task<IEnumerable<EmailAddressTypeListDto>> Handle(GetAllEmailAddressTypesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving all email address types");

                var emailAddressTypes = await _emailAddressTypeRepository.GetAllAsync(cancellationToken);
                var emailAddressTypeDtos = _mapper.Map<IEnumerable<EmailAddressTypeListDto>>(emailAddressTypes);

                _logger.LogInformation("Successfully retrieved {Count} email address types", emailAddressTypeDtos);

                return emailAddressTypeDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all email address types");
                throw;
            }
        }
    }
}