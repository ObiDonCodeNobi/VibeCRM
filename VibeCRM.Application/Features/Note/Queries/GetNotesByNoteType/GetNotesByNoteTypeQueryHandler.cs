using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Note.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Note.Queries.GetNotesByNoteType
{
    /// <summary>
    /// Handler for processing GetNotesByNoteTypeQuery requests.
    /// Implements the CQRS query handler pattern for retrieving notes of a specific note type.
    /// </summary>
    public class GetNotesByNoteTypeQueryHandler : IRequestHandler<GetNotesByNoteTypeQuery, IEnumerable<NoteListDto>>
    {
        private readonly INoteRepository _noteRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetNotesByNoteTypeQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetNotesByNoteTypeQueryHandler"/> class.
        /// </summary>
        /// <param name="noteRepository">The note repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public GetNotesByNoteTypeQueryHandler(
            INoteRepository noteRepository,
            IMapper mapper,
            ILogger<GetNotesByNoteTypeQueryHandler> logger)
        {
            _noteRepository = noteRepository ?? throw new ArgumentNullException(nameof(noteRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetNotesByNoteTypeQuery by retrieving all notes of a specific note type.
        /// </summary>
        /// <param name="request">The GetNotesByNoteTypeQuery containing the note type ID to retrieve notes for.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of NoteListDto objects representing the notes of the specified note type.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the note type ID is empty.</exception>
        public async Task<IEnumerable<NoteListDto>> Handle(GetNotesByNoteTypeQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.NoteTypeId == Guid.Empty) throw new ArgumentException("Note type ID cannot be empty", nameof(request.NoteTypeId));

            try
            {
                // Get notes by note type ID (Active=1 filter is applied in the repository)
                var notes = await _noteRepository.GetByNoteTypeAsync(request.NoteTypeId, cancellationToken);

                _logger.LogInformation("Retrieved {Count} notes for note type with ID: {NoteTypeId}",
                    notes is ICollection<Domain.Entities.BusinessEntities.Note> collection ? collection.Count : "multiple",
                    request.NoteTypeId);

                // Map entities to DTOs
                return _mapper.Map<IEnumerable<NoteListDto>>(notes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving notes for note type with ID {NoteTypeId}: {ErrorMessage}",
                    request.NoteTypeId, ex.Message);
                throw;
            }
        }
    }
}