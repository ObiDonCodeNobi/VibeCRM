using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.Attachment;

namespace VibeCRM.Application.Features.Attachment.Queries.GetAttachmentById
{
    /// <summary>
    /// Handler for processing GetAttachmentByIdQuery requests.
    /// Implements the CQRS query handler pattern for retrieving a specific attachment by ID.
    /// </summary>
    public class GetAttachmentByIdQueryHandler : IRequestHandler<GetAttachmentByIdQuery, AttachmentDetailsDto?>
    {
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAttachmentByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAttachmentByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="attachmentRepository">The attachment repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public GetAttachmentByIdQueryHandler(
            IAttachmentRepository attachmentRepository,
            IMapper mapper,
            ILogger<GetAttachmentByIdQueryHandler> logger)
        {
            _attachmentRepository = attachmentRepository ?? throw new ArgumentNullException(nameof(attachmentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAttachmentByIdQuery by retrieving a specific attachment from the database.
        /// </summary>
        /// <param name="request">The GetAttachmentByIdQuery containing the attachment ID to retrieve.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>An AttachmentDetailsDto representing the requested attachment, or null if not found.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the attachment ID is empty.</exception>
        public async Task<AttachmentDetailsDto?> Handle(GetAttachmentByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.Id == Guid.Empty) throw new ArgumentException("Attachment ID cannot be empty", nameof(request.Id));

            try
            {
                // Get the attachment by ID (Active=1 filter is applied in the repository)
                var attachment = await _attachmentRepository.GetByIdAsync(request.Id, cancellationToken);

                if (attachment == null)
                {
                    _logger.LogWarning("Attachment with ID {AttachmentId} not found or is inactive", request.Id);
                    return null;
                }

                _logger.LogInformation("Retrieved attachment with ID: {AttachmentId}", request.Id);

                // Map entity to DTO
                return _mapper.Map<AttachmentDetailsDto>(attachment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving attachment with ID {AttachmentId}: {ErrorMessage}",
                    request.Id, ex.Message);
                throw;
            }
        }
    }
}