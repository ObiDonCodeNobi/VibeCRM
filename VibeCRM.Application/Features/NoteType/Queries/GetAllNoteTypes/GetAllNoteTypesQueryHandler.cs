using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.NoteType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.NoteType.Queries.GetAllNoteTypes
{
    /// <summary>
    /// Handler for processing GetAllNoteTypesQuery requests
    /// </summary>
    public class GetAllNoteTypesQueryHandler : IRequestHandler<GetAllNoteTypesQuery, IEnumerable<NoteTypeDto>>
    {
        private readonly INoteTypeRepository _noteTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllNoteTypesQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the GetAllNoteTypesQueryHandler class
        /// </summary>
        /// <param name="noteTypeRepository">The note type repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public GetAllNoteTypesQueryHandler(
            INoteTypeRepository noteTypeRepository,
            IMapper mapper,
            ILogger<GetAllNoteTypesQueryHandler> logger)
        {
            _noteTypeRepository = noteTypeRepository ?? throw new ArgumentNullException(nameof(noteTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllNoteTypesQuery request
        /// </summary>
        /// <param name="request">The get all note types query</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>A collection of note type DTOs</returns>
        /// <exception cref="ApplicationException">Thrown when an error occurs during retrieval of note types</exception>
        public async Task<IEnumerable<NoteTypeDto>> Handle(GetAllNoteTypesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving all note types");

                var noteTypes = await _noteTypeRepository.GetAllAsync(cancellationToken);
                var notTypeDtos = _mapper.Map<IEnumerable<NoteTypeDto>>(noteTypes);

                _logger.LogInformation("Successfully retrieved all note types");
                return notTypeDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all note types");
                throw new ApplicationException("Error retrieving all note types", ex);
            }
        }
    }
}