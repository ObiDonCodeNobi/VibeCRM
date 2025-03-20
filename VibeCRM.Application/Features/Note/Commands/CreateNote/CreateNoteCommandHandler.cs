using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Note.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Note.Commands.CreateNote
{
    /// <summary>
    /// Handler for processing CreateNoteCommand requests.
    /// Implements the CQRS command handler pattern for creating new note entities.
    /// </summary>
    public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, NoteDetailsDto>
    {
        private readonly INoteRepository _noteRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateNoteCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateNoteCommandHandler"/> class.
        /// </summary>
        /// <param name="noteRepository">The note repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public CreateNoteCommandHandler(
            INoteRepository noteRepository,
            IMapper mapper,
            ILogger<CreateNoteCommandHandler> logger)
        {
            _noteRepository = noteRepository ?? throw new ArgumentNullException(nameof(noteRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreateNoteCommand by creating a new note entity in the database.
        /// </summary>
        /// <param name="request">The CreateNoteCommand containing the note details.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A NoteDetailsDto representing the newly created note.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<NoteDetailsDto> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            // Map command to entity
            var note = _mapper.Map<VibeCRM.Domain.Entities.BusinessEntities.Note>(request);

            try
            {
                // Add the note to the repository
                var createdNote = await _noteRepository.AddAsync(note, cancellationToken);
                _logger.LogInformation("Created new note with ID: {NoteId}", createdNote.NoteId);

                // Return the mapped DTO
                return _mapper.Map<NoteDetailsDto>(createdNote);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating note: {Subject}", request.Subject);
                throw;
            }
        }
    }
}