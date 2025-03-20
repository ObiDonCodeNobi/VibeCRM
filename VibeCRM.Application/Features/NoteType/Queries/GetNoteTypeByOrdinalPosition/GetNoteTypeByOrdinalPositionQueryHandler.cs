using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.NoteType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.NoteType.Queries.GetNoteTypeByOrdinalPosition
{
    /// <summary>
    /// Handler for processing GetNoteTypeByOrdinalPositionQuery requests
    /// </summary>
    public class GetNoteTypeByOrdinalPositionQueryHandler : IRequestHandler<GetNoteTypeByOrdinalPositionQuery, IEnumerable<NoteTypeDto>>
    {
        private readonly INoteTypeRepository _noteTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetNoteTypeByOrdinalPositionQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the GetNoteTypeByOrdinalPositionQueryHandler class
        /// </summary>
        /// <param name="noteTypeRepository">The note type repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public GetNoteTypeByOrdinalPositionQueryHandler(
            INoteTypeRepository noteTypeRepository,
            IMapper mapper,
            ILogger<GetNoteTypeByOrdinalPositionQueryHandler> logger)
        {
            _noteTypeRepository = noteTypeRepository ?? throw new ArgumentNullException(nameof(noteTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetNoteTypeByOrdinalPositionQuery request
        /// </summary>
        /// <param name="request">The get note types by ordinal position query</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>A collection of note type DTOs ordered by ordinal position</returns>
        /// <exception cref="ApplicationException">Thrown when an error occurs during retrieval of note types</exception>
        public async Task<IEnumerable<NoteTypeDto>> Handle(GetNoteTypeByOrdinalPositionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving note types ordered by ordinal position");

                var noteTypes = await _noteTypeRepository.GetByOrdinalPositionAsync(cancellationToken);
                var noteTypeDtos = _mapper.Map<IEnumerable<NoteTypeDto>>(noteTypes);

                _logger.LogInformation("Successfully retrieved note types ordered by ordinal position");
                return noteTypeDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving note types ordered by ordinal position");
                throw new ApplicationException("Error retrieving note types ordered by ordinal position", ex);
            }
        }
    }
}