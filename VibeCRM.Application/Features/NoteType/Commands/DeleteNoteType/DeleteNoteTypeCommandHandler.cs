using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.NoteType.Commands.DeleteNoteType
{
    /// <summary>
    /// Handler for processing DeleteNoteTypeCommand requests
    /// </summary>
    public class DeleteNoteTypeCommandHandler : IRequestHandler<DeleteNoteTypeCommand, bool>
    {
        private readonly INoteTypeRepository _noteTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteNoteTypeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the DeleteNoteTypeCommandHandler class
        /// </summary>
        /// <param name="noteTypeRepository">The note type repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public DeleteNoteTypeCommandHandler(
            INoteTypeRepository noteTypeRepository,
            IMapper mapper,
            ILogger<DeleteNoteTypeCommandHandler> logger)
        {
            _noteTypeRepository = noteTypeRepository ?? throw new ArgumentNullException(nameof(noteTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeleteNoteTypeCommand request
        /// </summary>
        /// <param name="request">The delete note type command</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>True if the delete was successful, otherwise false</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the note type with the specified ID is not found</exception>
        /// <exception cref="ApplicationException">Thrown when an error occurs during note type deletion</exception>
        public async Task<bool> Handle(DeleteNoteTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Soft deleting note type with ID: {Id}", request.Id);

                var existingEntity = await _noteTypeRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingEntity == null)
                {
                    _logger.LogWarning("Note type with ID: {Id} not found", request.Id);
                    throw new KeyNotFoundException($"Note type with ID: {request.Id} not found");
                }

                // Update the ModifiedBy before deletion
                existingEntity.ModifiedBy = Guid.Parse(request.ModifiedBy);
                await _noteTypeRepository.UpdateAsync(existingEntity, cancellationToken);

                // Perform soft delete
                await _noteTypeRepository.DeleteAsync(request.Id, cancellationToken);
                _logger.LogInformation("Successfully soft deleted note type with ID: {Id}", request.Id);

                return true;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error soft deleting note type with ID: {Id}", request.Id);
                throw new ApplicationException($"Error soft deleting note type with ID: {request.Id}", ex);
            }
        }
    }
}