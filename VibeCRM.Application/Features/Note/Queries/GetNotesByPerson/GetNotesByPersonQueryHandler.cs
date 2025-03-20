using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Note.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Note.Queries.GetNotesByPerson
{
    /// <summary>
    /// Handler for processing GetNotesByPersonQuery requests.
    /// Implements the CQRS query handler pattern for retrieving notes associated with a specific person.
    /// </summary>
    public class GetNotesByPersonQueryHandler : IRequestHandler<GetNotesByPersonQuery, IEnumerable<NoteListDto>>
    {
        private readonly INoteRepository _noteRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetNotesByPersonQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetNotesByPersonQueryHandler"/> class.
        /// </summary>
        /// <param name="noteRepository">The note repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public GetNotesByPersonQueryHandler(
            INoteRepository noteRepository,
            IMapper mapper,
            ILogger<GetNotesByPersonQueryHandler> logger)
        {
            _noteRepository = noteRepository ?? throw new ArgumentNullException(nameof(noteRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetNotesByPersonQuery by retrieving all notes associated with a specific person.
        /// </summary>
        /// <param name="request">The GetNotesByPersonQuery containing the person ID to retrieve notes for.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of NoteListDto objects representing the notes associated with the person.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the person ID is empty.</exception>
        public async Task<IEnumerable<NoteListDto>> Handle(GetNotesByPersonQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.PersonId == Guid.Empty) throw new ArgumentException("Person ID cannot be empty", nameof(request.PersonId));

            try
            {
                // Get notes by person ID (Active=1 filter is applied in the repository)
                var notes = await _noteRepository.GetByPersonAsync(request.PersonId, cancellationToken);

                _logger.LogInformation("Retrieved {Count} notes for person with ID: {PersonId}",
                    notes is ICollection<Domain.Entities.BusinessEntities.Note> collection ? collection.Count : "multiple",
                    request.PersonId);

                // Map entities to DTOs
                return _mapper.Map<IEnumerable<NoteListDto>>(notes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving notes for person with ID {PersonId}: {ErrorMessage}",
                    request.PersonId, ex.Message);
                throw;
            }
        }
    }
}