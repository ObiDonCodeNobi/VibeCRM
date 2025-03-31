using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.AttachmentType;

namespace VibeCRM.Application.Features.AttachmentType.Queries.GetDefaultAttachmentType
{
    /// <summary>
    /// Handler for the GetDefaultAttachmentTypeQuery.
    /// Processes requests to retrieve the default attachment type.
    /// </summary>
    public class GetDefaultAttachmentTypeQueryHandler : IRequestHandler<GetDefaultAttachmentTypeQuery, AttachmentTypeDetailsDto?>
    {
        private readonly IAttachmentTypeRepository _attachmentTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDefaultAttachmentTypeQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetDefaultAttachmentTypeQueryHandler"/> class.
        /// </summary>
        /// <param name="attachmentTypeRepository">The attachment type repository for data access.</param>
        /// <param name="mapper">The AutoMapper instance for mapping entities to DTOs.</param>
        /// <param name="logger">The logger for logging information and errors.</param>
        /// <exception cref="ArgumentNullException">Thrown when any parameter is null.</exception>
        public GetDefaultAttachmentTypeQueryHandler(
            IAttachmentTypeRepository attachmentTypeRepository,
            IMapper mapper,
            ILogger<GetDefaultAttachmentTypeQueryHandler> logger)
        {
            _attachmentTypeRepository = attachmentTypeRepository ?? throw new ArgumentNullException(nameof(attachmentTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetDefaultAttachmentTypeQuery by retrieving the default attachment type from the database.
        /// </summary>
        /// <param name="request">The query to retrieve the default attachment type.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>The default attachment type details DTO if found, otherwise null.</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
        public async Task<AttachmentTypeDetailsDto?> Handle(GetDefaultAttachmentTypeQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            _logger.LogInformation("Retrieving default attachment type");

            var defaultAttachmentType = await _attachmentTypeRepository.GetDefaultAsync(cancellationToken);
            if (defaultAttachmentType == null || !defaultAttachmentType.Active)
            {
                _logger.LogWarning("Default attachment type not found or inactive");
                return null;
            }

            _logger.LogInformation("Successfully retrieved default attachment type with ID: {Id}", defaultAttachmentType.Id);

            return _mapper.Map<AttachmentTypeDetailsDto>(defaultAttachmentType);
        }
    }
}