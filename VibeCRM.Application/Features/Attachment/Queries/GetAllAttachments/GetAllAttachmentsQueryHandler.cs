using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.Attachment;

namespace VibeCRM.Application.Features.Attachment.Queries.GetAllAttachments
{
    /// <summary>
    /// Handler for processing GetAllAttachmentsQuery requests.
    /// Implements the CQRS query handler pattern for retrieving all active attachments.
    /// </summary>
    public class GetAllAttachmentsQueryHandler : IRequestHandler<GetAllAttachmentsQuery, IEnumerable<AttachmentListDto>>
    {
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllAttachmentsQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllAttachmentsQueryHandler"/> class.
        /// </summary>
        /// <param name="attachmentRepository">The attachment repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public GetAllAttachmentsQueryHandler(
            IAttachmentRepository attachmentRepository,
            IMapper mapper,
            ILogger<GetAllAttachmentsQueryHandler> logger)
        {
            _attachmentRepository = attachmentRepository ?? throw new ArgumentNullException(nameof(attachmentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllAttachmentsQuery by retrieving all active attachments from the database.
        /// </summary>
        /// <param name="request">The GetAllAttachmentsQuery request.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of AttachmentListDto representing all active attachments.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<IEnumerable<AttachmentListDto>> Handle(GetAllAttachmentsQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                // Get all active attachments (Active=1 filter is applied in the repository)
                var attachments = await _attachmentRepository.GetAllAsync(cancellationToken);

                _logger.LogInformation("Retrieved all active attachments");

                // Map entities to DTOs
                return _mapper.Map<IEnumerable<AttachmentListDto>>(attachments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all attachments: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}