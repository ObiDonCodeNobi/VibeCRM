using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.NoteType.Commands.UpdateNoteType
{
    /// <summary>
    /// Handler for processing UpdateNoteTypeCommand requests
    /// </summary>
    public class UpdateNoteTypeCommandHandler : IRequestHandler<UpdateNoteTypeCommand, bool>
    {
        private readonly INoteTypeRepository _noteTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateNoteTypeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the UpdateNoteTypeCommandHandler class
        /// </summary>
        /// <param name="noteTypeRepository">The note type repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public UpdateNoteTypeCommandHandler(
            INoteTypeRepository noteTypeRepository,
            IMapper mapper,
            ILogger<UpdateNoteTypeCommandHandler> logger)
        {
            _noteTypeRepository = noteTypeRepository ?? throw new ArgumentNullException(nameof(noteTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdateNoteTypeCommand request
        /// </summary>
        /// <param name="request">The update note type command</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>True if the update was successful, otherwise false</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the note type with the specified ID is not found</exception>
        /// <exception cref="ApplicationException">Thrown when an error occurs during note type update</exception>
        public async Task<bool> Handle(UpdateNoteTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Updating note type with ID: {Id}", request.Id);

                var existingEntity = await _noteTypeRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingEntity == null)
                {
                    _logger.LogWarning("Note type with ID: {Id} not found", request.Id);
                    throw new KeyNotFoundException($"Note type with ID: {request.Id} not found");
                }

                // Update the entity properties
                existingEntity.Type = request.Type;
                existingEntity.Description = request.Description;
                existingEntity.OrdinalPosition = request.OrdinalPosition;
                existingEntity.ModifiedBy = Guid.Parse(request.ModifiedBy);

                await _noteTypeRepository.UpdateAsync(existingEntity, cancellationToken);
                _logger.LogInformation("Successfully updated note type with ID: {Id}", request.Id);

                return true;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating note type with ID: {Id}", request.Id);
                throw new ApplicationException($"Error updating note type with ID: {request.Id}", ex);
            }
        }
    }
}