using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.NoteType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.NoteType.Queries.GetNoteTypeById
{
    /// <summary>
    /// Handler for processing GetNoteTypeByIdQuery requests
    /// </summary>
    public class GetNoteTypeByIdQueryHandler : IRequestHandler<GetNoteTypeByIdQuery, NoteTypeDetailsDto>
    {
        private readonly INoteTypeRepository _noteTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetNoteTypeByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the GetNoteTypeByIdQueryHandler class
        /// </summary>
        /// <param name="noteTypeRepository">The note type repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public GetNoteTypeByIdQueryHandler(
            INoteTypeRepository noteTypeRepository,
            IMapper mapper,
            ILogger<GetNoteTypeByIdQueryHandler> logger)
        {
            _noteTypeRepository = noteTypeRepository ?? throw new ArgumentNullException(nameof(noteTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetNoteTypeByIdQuery request
        /// </summary>
        /// <param name="request">The get note type by ID query</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The note type details DTO</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the note type with the specified ID is not found</exception>
        /// <exception cref="ApplicationException">Thrown when an error occurs during retrieval of the note type</exception>
        public async Task<NoteTypeDetailsDto> Handle(GetNoteTypeByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving note type with ID: {Id}", request.Id);

                var noteType = await _noteTypeRepository.GetByIdAsync(request.Id, cancellationToken);
                if (noteType == null)
                {
                    _logger.LogWarning("Note type with ID: {Id} not found", request.Id);
                    throw new KeyNotFoundException($"Note type with ID: {request.Id} not found");
                }

                var noteTypeDto = _mapper.Map<NoteTypeDetailsDto>(noteType);

                _logger.LogInformation("Successfully retrieved note type with ID: {Id}", request.Id);
                return noteTypeDto;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving note type with ID: {Id}", request.Id);
                throw new ApplicationException($"Error retrieving note type with ID: {request.Id}", ex);
            }
        }
    }
}