using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Note.Commands.DeleteNote
{
    /// <summary>
    /// Handler for processing DeleteNoteCommand requests.
    /// Implements the CQRS command handler pattern for soft-deleting note entities.
    /// </summary>
    public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand, bool>
    {
        private readonly INoteRepository _noteRepository;
        private readonly ILogger<DeleteNoteCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteNoteCommandHandler"/> class.
        /// </summary>
        /// <param name="noteRepository">The note repository for database operations.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public DeleteNoteCommandHandler(
            INoteRepository noteRepository,
            ILogger<DeleteNoteCommandHandler> logger)
        {
            _noteRepository = noteRepository ?? throw new ArgumentNullException(nameof(noteRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeleteNoteCommand by soft-deleting a note entity in the database.
        /// Sets the Active property to false rather than physically removing the record.
        /// </summary>
        /// <param name="request">The DeleteNoteCommand containing the note ID to delete.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>True if the note was successfully soft-deleted, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the note ID is empty.</exception>
        public async Task<bool> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.NoteId == Guid.Empty) throw new ArgumentException("Note ID cannot be empty", nameof(request.NoteId));
            if (request.ModifiedBy == Guid.Empty) throw new ArgumentException("Modified by user ID cannot be empty", nameof(request.ModifiedBy));

            try
            {
                // Get the existing note
                var existingNote = await _noteRepository.GetByIdAsync(request.NoteId, cancellationToken);
                if (existingNote == null)
                {
                    _logger.LogWarning("Note with ID {NoteId} not found or is already inactive", request.NoteId);
                    return false;
                }

                // Update the ModifiedBy property before deletion
                existingNote.ModifiedBy = request.ModifiedBy;
                existingNote.ModifiedDate = DateTime.UtcNow;

                // First update the note with the modified information
                await _noteRepository.UpdateAsync(existingNote, cancellationToken);

                // Then soft delete the note (sets Active = false)
                var result = await _noteRepository.DeleteAsync(request.NoteId, cancellationToken);

                if (result)
                {
                    _logger.LogInformation("Soft-deleted note with ID: {NoteId}", request.NoteId);
                }
                else
                {
                    _logger.LogWarning("Failed to soft-delete note with ID: {NoteId}", request.NoteId);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while soft-deleting note with ID: {NoteId}", request.NoteId);
                throw;
            }
        }
    }
}