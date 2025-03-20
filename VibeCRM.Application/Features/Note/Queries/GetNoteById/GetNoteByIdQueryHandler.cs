using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Note.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Note.Queries.GetNoteById
{
    /// <summary>
    /// Handler for processing GetNoteByIdQuery requests.
    /// Implements the CQRS query handler pattern for retrieving a specific note by ID.
    /// </summary>
    public class GetNoteByIdQueryHandler : IRequestHandler<GetNoteByIdQuery, NoteDetailsDto?>
    {
        private readonly INoteRepository _noteRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetNoteByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetNoteByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="noteRepository">The note repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public GetNoteByIdQueryHandler(
            INoteRepository noteRepository,
            IMapper mapper,
            ILogger<GetNoteByIdQueryHandler> logger)
        {
            _noteRepository = noteRepository ?? throw new ArgumentNullException(nameof(noteRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetNoteByIdQuery by retrieving a specific note from the database.
        /// </summary>
        /// <param name="request">The GetNoteByIdQuery containing the note ID to retrieve.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A NoteDetailsDto representing the requested note, or null if not found.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the note ID is empty.</exception>
        public async Task<NoteDetailsDto?> Handle(GetNoteByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.Id == Guid.Empty) throw new ArgumentException("Note ID cannot be empty", nameof(request.Id));

            try
            {
                // Get the note by ID (Active=1 filter is applied in the repository)
                var note = await _noteRepository.GetByIdAsync(request.Id, cancellationToken);

                if (note == null)
                {
                    _logger.LogWarning("Note with ID {NoteId} not found or is inactive", request.Id);
                    return null;
                }

                _logger.LogInformation("Retrieved note with ID: {NoteId}", request.Id);

                // Map entity to DTO
                return _mapper.Map<NoteDetailsDto>(note);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving note with ID {NoteId}: {ErrorMessage}", request.Id, ex.Message);
                throw;
            }
        }
    }
}