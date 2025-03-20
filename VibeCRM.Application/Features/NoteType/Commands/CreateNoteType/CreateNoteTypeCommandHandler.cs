using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.NoteType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.NoteType.Commands.CreateNoteType
{
    /// <summary>
    /// Handler for processing CreateNoteTypeCommand requests
    /// </summary>
    public class CreateNoteTypeCommandHandler : IRequestHandler<CreateNoteTypeCommand, NoteTypeDetailsDto>
    {
        private readonly INoteTypeRepository _noteTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateNoteTypeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the CreateNoteTypeCommandHandler class
        /// </summary>
        /// <param name="noteTypeRepository">The note type repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public CreateNoteTypeCommandHandler(
            INoteTypeRepository noteTypeRepository,
            IMapper mapper,
            ILogger<CreateNoteTypeCommandHandler> logger)
        {
            _noteTypeRepository = noteTypeRepository ?? throw new ArgumentNullException(nameof(noteTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreateNoteTypeCommand request
        /// </summary>
        /// <param name="request">The create note type command</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The created note type details</returns>
        /// <exception cref="ApplicationException">Thrown when an error occurs during note type creation</exception>
        public async Task<NoteTypeDetailsDto> Handle(CreateNoteTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Creating new note type: {Type}", request.Type);

                var entity = new Domain.Entities.TypeStatusEntities.NoteType
                {
                    Type = request.Type,
                    Description = request.Description,
                    OrdinalPosition = request.OrdinalPosition,
                    CreatedBy = Guid.Parse(request.CreatedBy),
                    ModifiedBy = Guid.Parse(request.CreatedBy),
                    Active = true
                };

                var createdEntity = await _noteTypeRepository.AddAsync(entity, cancellationToken);
                _logger.LogInformation("Successfully created note type with ID: {Id}", createdEntity.Id);

                return _mapper.Map<NoteTypeDetailsDto>(createdEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating note type: {Type}", request.Type);
                throw new ApplicationException($"Error creating note type: {ex.Message}", ex);
            }
        }
    }
}