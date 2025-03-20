using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Note.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Note.Commands.UpdateNote
{
    /// <summary>
    /// Handler for processing UpdateNoteCommand requests.
    /// Implements the CQRS command handler pattern for updating existing note entities.
    /// </summary>
    public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, NoteDetailsDto>
    {
        private readonly INoteRepository _noteRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateNoteCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateNoteCommandHandler"/> class.
        /// </summary>
        /// <param name="noteRepository">The note repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public UpdateNoteCommandHandler(
            INoteRepository noteRepository,
            IMapper mapper,
            ILogger<UpdateNoteCommandHandler> logger)
        {
            _noteRepository = noteRepository ?? throw new ArgumentNullException(nameof(noteRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdateNoteCommand by updating an existing note entity in the database.
        /// </summary>
        /// <param name="request">The UpdateNoteCommand containing the updated note details.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A NoteDetailsDto representing the updated note.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the note ID is empty.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the note to update is not found.</exception>
        public async Task<NoteDetailsDto> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.NoteId == Guid.Empty) throw new ArgumentException("Note ID cannot be empty", nameof(request.NoteId));

            try
            {
                // Get the existing note
                var existingNote = await _noteRepository.GetByIdAsync(request.NoteId, cancellationToken);
                if (existingNote == null)
                {
                    _logger.LogWarning("Note with ID {NoteId} not found or is inactive", request.NoteId);
                    throw new InvalidOperationException($"Note with ID {request.NoteId} not found or is inactive");
                }

                // Map updated values to the existing entity
                _mapper.Map(request, existingNote);

                // Update the note in the repository
                var updatedNote = await _noteRepository.UpdateAsync(existingNote, cancellationToken);
                _logger.LogInformation("Updated note with ID: {NoteId}", updatedNote.NoteId);

                // Return the mapped DTO
                return _mapper.Map<NoteDetailsDto>(updatedNote);
            }
            catch (Exception ex) when (!(ex is ArgumentNullException || ex is ArgumentException || ex is InvalidOperationException))
            {
                _logger.LogError(ex, "Error occurred while updating note with ID: {NoteId}", request.NoteId);
                throw;
            }
        }
    }
}